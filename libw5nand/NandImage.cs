using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace libnvtnand
{
    public class NandImage
    {
        public BootHeader Header;
        private List<ImageEntry> Images;

        public const uint BytesPerBlock = 0x20000;
        public const uint NandSize = 0x800000;

        public NandImage()
        {
            Header = new BootHeader();
            Images = new List<ImageEntry>();
        }

        public bool GetImage(ushort ID, out ImageEntry Entry)
        {
            if(!ImageExists(ID)) {
                Entry = new ImageEntry();
                return false;
            }

            for(int i = 0; i < Images.Count; i++) {
                if(Images[i].ImageID == ID) {
                    Entry = Images[i];
                    return true;
                }
            }

            Entry = new ImageEntry();
            return false;
        }

        public List<ImageEntry> GetImages()
        {
            return Images;
        }

        public bool AddImage(ImageEntry Entry)
        {
            if (ImageExists(Entry.ImageID))
                return false;
            if (!CheckValidEntry(Entry, true))
                return false;

            Images.Add(Entry);
            return true;
        }

        public uint CalculateUsage()
        {
            if (Images == null)
                Images = new List<ImageEntry>();

            uint size = 0;
            for (int i = 0; i < Images.Count; i++)
            {
                size += Images[i].FileSize;
            }

            return size;
        }

        private bool CheckValidEntry(ImageEntry Entry, bool ToAdd)
        {
            if (Images == null)
                Images = new List<ImageEntry>();

            if (Entry.ImageID == 0) {
                if (Entry.ImageType != ImageType.System || Entry.StartBlock != 0 || Entry.EndBlock != 3)
                    return false;
            }

            if (ToAdd && Entry.StartBlock >= Entry.EndBlock)
                return false;
            if (Entry.FileSize <= 0)
                return false;

            if (ToAdd)
            {
                foreach (ImageEntry image in Images)
                {
                    if (Entry.StartBlock >= image.StartBlock && Entry.EndBlock <= image.EndBlock)
                        return false;
                }
            }

            if (ToAdd && CalculateUsage() + Entry.FileSize > NandSize)
                return false;

            return true;
        }

        private bool HasValidNandLoader()
        {
            ImageEntry nandLoader;
            if (!GetImage(0, out nandLoader))
                return false;
            if (!nandLoader.Name.ToLower().Contains("nandloader"))
               return false;

            return CheckValidEntry(nandLoader, false);
        }

        public void RemoveImage(ushort ID)
        {
            if (ImageExists(ID))
            {
                for (int i = 0; i < Images.Count; i++)
                {
                    if (Images[i].ImageID == ID)
                        Images.RemoveAt(i);
                }
            }
        }

        public bool ReplaceImage(ImageEntry Entry)
        {
            RemoveImage(Entry.ImageID);
            return AddImage(Entry);
        }

        public bool ImageExists(ushort ID)
        {
            if (Images == null)
            {
                Images = new List<ImageEntry>();
                return false;
            }

            foreach (ImageEntry image in Images)
            {
                if (image.ImageID == ID)
                    return true;
            }

            return false;
        }

        public static bool FormatImage(Stream ImageStream)
        {
            if (ImageStream == null)
                return false;

            try
            {
                // Seek to the beginning of the stream
                ImageStream.Seek(0, SeekOrigin.Begin);

                // Now format the NAND image
                for (int i = 0; i < NandSize; i++)
                    ImageStream.WriteByte(0xFF);

                // And seek back
                ImageStream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool WriteImage(Stream ImageStream)
        {
            if (Images == null || ImageStream == null)
                return false;
            if (Images.Count == 0)
                return false;
            if (!HasValidNandLoader())
                return false;

            try
            {

                BinaryWriter writer = new BinaryWriter(ImageStream);

                // Write the boot header to various locations
                // 0x00000, 0x20000, 0x40000 and 0x60000
                for (int block = 0; block < 4; block++)
                {
                    writer.BaseStream.Seek(0x20000 * block, SeekOrigin.Begin);
                    if (!WriteHeader(writer, Header))
                        return false;
                }

                // When writing the bootloader, there is a copy at 0x1f000, 0x3f000, 0x5f000
                // and at 0x7f000
                // Copy the headers and data over
                for (int block = 0; block < 4; block++)
                {
                    // Seek to the end page of the block
                    writer.BaseStream.Seek((0x10000 + (0x20000 * block)) | 0xf000, SeekOrigin.Begin);

                    // Write the preamble
                    // First, seek to the last page of the block
                    try
                    {
                        // Now write the magic
                        writer.Write((uint)0x574255aa);
                        // Then write the number of images
                        writer.Write((uint)Images.Count);
                        // Now skip one 32 bit word
                        writer.BaseStream.Seek(sizeof(uint), SeekOrigin.Current);
                        // Finally write the last magic
                        writer.Write((uint)0x57425963);
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    // Write all the images
                    for (ushort imageID = 0; imageID < Images.Count; imageID++)
                    {
                        // Retrieve the image
                        ImageEntry img;
                        if (!GetImage(imageID, out img))
                            return false;

                        // Write the header
                        if (!WriteEntryHeader(writer, img))
                            return false;

                        // On the first write of every image, write the data
                        if (block == 0 || imageID == 0)
                        {
                            if (!WriteEntryData(writer, img,
                                (imageID == 0) ? (uint)((block * BytesPerBlock) + 0x20) : 0))
                                return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool ReadImage(Stream ImageStream)
        {
            if (ImageStream == null)
                return false;

            BinaryReader reader = new BinaryReader(ImageStream);
            uint numImages = 0;

            // Prepare everything
            Header = new BootHeader();
            if (Images != null)
                Images.Clear();
            Images = new List<ImageEntry>();

            // Our blocksize is 0x20000 bytes / block
            // Seek to the beginning of the file / memorystream for our header block
            // When writing, there is a copy of the entire NandLoader at 0x20000, 0x40000 and 0x60000
            reader.BaseStream.Seek(0x00000, SeekOrigin.Begin);
            if (!ReadHeader(reader, out Header))
                return false;

            // Now seek to block 0 of the last nand page - 1
            // When writing, there is a copy at 0x3f000, 0x5f000 and at 0x7f000
            reader.BaseStream.Seek(0x1f000, SeekOrigin.Begin);
            try
            {
                // Now check the magic
                if (reader.ReadUInt32() != 0x574255aa)
                    return false;
                // Then read the number of images
                numImages = reader.ReadUInt32();
                // Now skip one 32 bit word
                reader.BaseStream.Seek(sizeof(uint), SeekOrigin.Current);
                // Finally check the last magic
                if (reader.ReadUInt32() != 0x57425963)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            // Now we can read the image structs
            for (int entry = 0; entry < numImages; entry++)
            {
                ImageEntry ent;
                if (!ReadEntry(reader, out ent))
                    return false;

                Images.Add(ent);
            }

            return true;
        }

        private bool WriteHeader(BinaryWriter Writer, BootHeader Header)
        {
            if (Writer == null)
                return false;

            try
            {
                Writer.Write(Header.BootCodeMarker);
                Writer.Write(Header.ExecuteAddress);
                Writer.Write(Header.ImageSize);
                Writer.Write(Header.SkewMarker);
                Writer.Write(Header.DQSODS);
                Writer.Write(Header.CLKQSDS);
                Writer.Write(Header.DramMarker);
                Writer.Write(Header.DramSize);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool WriteEntryHeader(BinaryWriter Writer, ImageEntry Entry)
        {
            if (Writer == null)
                return false;

            // Check if the entry is valid before writing the data
            if (!CheckValidEntry(Entry, false))
                return false;

            try
            {
                Writer.Write(Entry.ImageID);
                Writer.Write((ushort)Entry.ImageType);
                Writer.Write(Entry.StartBlock);
                Writer.Write(Entry.EndBlock);
                Writer.Write(Entry.ExecuteAddress);
                Writer.Write(Entry.FileSize);
                if (!Utils.WriteNCString(Writer, Entry.Name, 0x20))
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool WriteEntryData(BinaryWriter Writer, ImageEntry Entry, uint DataOffset = 0x0)
        {
            if (Writer == null)
                return false;

            // Check if the entry is valid before writing the data
            if (!CheckValidEntry(Entry, false))
                return false;

            try
            {
                long oldPos = Writer.BaseStream.Position;

                // Seek to the start of data
                Writer.BaseStream.Seek(Entry.StartBlock * BytesPerBlock + DataOffset,
                    SeekOrigin.Begin);
                // Write all the data
                Writer.Write(Entry.Data);

                Writer.BaseStream.Position = oldPos;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool ReadHeader(BinaryReader Reader, out BootHeader Header)
        {
            Header = new BootHeader();

            if (Reader == null)
                return false;

            try
            {
                Header.BootCodeMarker = Reader.ReadUInt32();
                Header.ExecuteAddress = Reader.ReadUInt32();
                Header.ImageSize = Reader.ReadUInt32();
                Header.SkewMarker = Reader.ReadUInt32();
                Header.DQSODS = Reader.ReadUInt32();
                Header.CLKQSDS = Reader.ReadUInt32();
                Header.DramMarker = Reader.ReadUInt32();
                Header.DramSize = Reader.ReadUInt32();
            }
            catch (Exception)
            {
                return false;
            }

            // Magic check
            if (Header.BootCodeMarker != 0x57425AA5)
                return false;

            return true;
        }

        private bool ReadEntry(BinaryReader Reader, out ImageEntry Entry)
        {
            Entry = new ImageEntry();

            if (Reader == null)
                return false;

            try
            {
                Entry.ImageID = Reader.ReadUInt16();
                Entry.ImageType = (ImageType)Reader.ReadUInt16();
                Entry.StartBlock = Reader.ReadUInt16();
                Entry.EndBlock = Reader.ReadUInt16();
                Entry.ExecuteAddress = Reader.ReadUInt32();
                Entry.FileSize = Reader.ReadUInt32();
                Entry.Name = Utils.ReadNCString(Reader, 0x20);

                if (Entry.Name == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            // Check if the entry is valid before reading the data
            if (!CheckValidEntry(Entry, true))
                return false;

            try
            {
                long oldPosition = Reader.BaseStream.Position;

                // Check for nand loader
                // If we have got a NAND Loader entry, copy 1/2 a block (0x10000 bytes)
                if (Entry.ImageID == 0) // NAND Loader, start from 0x20
                    Reader.BaseStream.Seek(0x20, SeekOrigin.Begin);
                else // Seek to the normal data
                    Reader.BaseStream.Seek(Entry.StartBlock * BytesPerBlock, SeekOrigin.Begin);
                    
                // Now copy the entire image
                Entry.Data = Reader.ReadBytes((int)((Entry.ImageID == 0) ? (Entry.FileSize - 0x20) : Entry.FileSize));

                Reader.BaseStream.Position = oldPosition;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
