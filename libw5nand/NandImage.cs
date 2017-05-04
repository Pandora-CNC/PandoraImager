using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace libw5nand
{
    public class NandImage
    {
        public BootHeader Header;
        public List<ImageEntry> Images;

        public int ImageCount
        {
            get
            {
                return Images.Count;
            }
        }

        public const uint BytesPerBlock = 0x20000;
        public const uint NandSize = 0x800000;

        public NandImage()
        {
            Header = new BootHeader();
            Images = new List<ImageEntry>();
        }

        public uint CalculateUsage()
        {
            uint size = 0;
            foreach (ImageEntry image in Images)
            {
                if(image.Data != null)
                    size += (uint)image.Data.Length;
            }

            return size;
        }

        public static ushort CalculateBlocks(int Bytes)
        {
            if (Bytes % NandImage.BytesPerBlock > 0)
                return (ushort)(((Bytes - (Bytes % NandImage.BytesPerBlock))
                    / NandImage.BytesPerBlock) + 1);
            else
                return (ushort)(Bytes / NandImage.BytesPerBlock);
        }

        public static bool CheckEntry(ImageEntry Entry)
        {
            if (Entry.Data == null)
                return false;
            if (Entry.Data.Length == 0)
                return false;
            if (Entry.Name.Length >= 0x30)
                return false;
            
            return true;
        }

        public bool CheckAddEntry(ImageEntry Entry)
        {
            if (Images.Count == 0 && Entry.Data.Length > NandSize / 2)
                return false;
            if (CalculateUsage() + Entry.Data.Length > NandSize)
                return false;

            return NandImage.CheckEntry(Entry);
        }

        public bool ReplaceImage(int ID, ImageEntry NewEntry)
        {
            if (Images.Count <= ID || ID <= 0)
                return false;
            Images[ID] = NewEntry;

            return true;
        }

        public bool SwapImages(int ID1, int ID2)
        {
            if (Images.Count <= ID1 || Images.Count <= ID2 || ID1 <= 0 || ID2 <= 0)
                return false;
            
            ImageEntry tmp;
            tmp = Images[ID1];
            Images[ID1] = Images[ID2];
            Images[ID2] = tmp;

            return true;
        }

        public bool MoveImageUp(int ID)
        {
            if (Images.Count <= ID || ID - 1 <= 0)
                return false;

            ImageEntry tmp;
            tmp = Images[ID];
            Images[ID] = Images[ID - 1];
            Images[ID - 1] = tmp;

            return true;
        }

        public bool MoveImageDown(int ID)
        {
            if (Images.Count <= ID + 1 || ID <= 0)
                return false;

            ImageEntry tmp;
            tmp = Images[ID];
            Images[ID] = Images[ID + 1];
            Images[ID + 1] = tmp;

            return true;
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
                    ImageStream.WriteByte((byte)0xFF);

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

            try
            {

                BinaryWriter writer = new BinaryWriter(ImageStream);

                // Write the boot header to various locations
                // 0x00000, 0x20000, 0x40000 and 0x60000
                for (int block = 0; block < 4; block++)
                {
                    writer.BaseStream.Seek(0x20000 * block, SeekOrigin.Begin);
                    if (!WriteBootHeader(writer, Header))
                        return false;
                }

                // When writing the bootloader, there is a copy at 0x1f000, 0x3f000, 0x5f000
                // and at 0x7f000
                // Copy the headers and data over
                for (int copyIter = 0; copyIter < 4; copyIter++)
                {
                    // Seek to the end page of the block
                    writer.BaseStream.Seek((0x10000 + (0x20000 * copyIter)) | 0xf000, SeekOrigin.Begin);

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
                    ushort block = 0;
                    for (ushort imageID = 0; imageID < Images.Count; imageID++)
                    {
                        // Retrieve the image
                        ImageEntry img = Images[imageID];
                        ImageHeader head = new ImageHeader();
                        ushort numBlocks;

                        if (!CheckEntry(img))
                            return false;

                        // Calculate the blocks
                        if (imageID == 0)
                            numBlocks = 3;
                        else
                            numBlocks = CalculateBlocks(img.Data.Length);

                        // Construct the header
                        head.ImageID = imageID;
                        head.ImageName = img.Name;
                        head.ImageType = img.Type;
                        head.StartBlock = block;
                        head.EndBlock = (ushort)(head.StartBlock + numBlocks);
                        head.ExecuteAddress = img.ExecAddr;
                        head.FileSize = (uint)img.Data.Length;

                        // On the bootloader image, adjust the size
                        if (imageID == 0)
                            head.FileSize += 0x20;

                        // Write the header
                        if (!WriteEntryHeader(writer, head))
                            return false;

                        // On the first write of every image, write the data
                        // For id 0, write it every time to a new location
                        if (copyIter == 0 || imageID == 0)
                        {
                            uint offset;
                            if (imageID == 0)
                                offset = (uint)((copyIter * BytesPerBlock) + 0x20);
                            else
                                offset = (uint)(block * BytesPerBlock);

                            if (!WriteEntryData(writer, img, offset))
                                return false;
                        }

                        block += numBlocks;
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
            if (!ReadBootHeader(reader, out Header))
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
            for (int image = 0; image < numImages; image++)
            {
                ImageHeader head;
                if (!ReadEntryHeader(reader, out head))
                    return false;

                ImageEntry entry = new ImageEntry(head.ImageName, head.ImageType,
                    head.ExecuteAddress);
                if (!ReadEntryData(reader, head, out entry.Data))
                    return false;

                if (!CheckAddEntry(entry))
                    return false;

                Images.Add(entry);
            }

            return true;
        }

        private bool WriteBootHeader(BinaryWriter Writer, BootHeader Header)
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

        private bool WriteEntryHeader(BinaryWriter Writer, ImageHeader Header)
        {
            if (Writer == null)
                return false;

            // Check if the entry is valid before writing the data
            if (!CheckEntryHeader(Header))
                return false;

            try
            {
                Writer.Write(Header.ImageID);
                Writer.Write((ushort)Header.ImageType);
                Writer.Write(Header.StartBlock);
                Writer.Write(Header.EndBlock);
                Writer.Write(Header.ExecuteAddress);
                Writer.Write(Header.FileSize);
                if (!Utils.WriteNCString(Writer, Header.ImageName, 0x20))
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool WriteEntryData(BinaryWriter Writer, ImageEntry Entry, uint DataOffset)
        {
            if (Writer == null)
                return false;

            // Check if the entry is valid before writing the data
            if (!CheckEntry(Entry))
                return false;

            try
            {
                long oldPos = Writer.BaseStream.Position;

                // Seek to the start of data
                Writer.BaseStream.Seek(DataOffset, SeekOrigin.Begin);
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

        private bool ReadBootHeader(BinaryReader Reader, out BootHeader Header)
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

        private bool ReadEntryHeader(BinaryReader Reader, out ImageHeader Header)
        {
            Header = new ImageHeader();

            if (Reader == null)
                return false;

            try
            {
                Header.ImageID = Reader.ReadUInt16();
                Header.ImageType = (ImageType)Reader.ReadUInt16();
                Header.StartBlock = Reader.ReadUInt16();
                Header.EndBlock = Reader.ReadUInt16();
                Header.ExecuteAddress = Reader.ReadUInt32();
                Header.FileSize = Reader.ReadUInt32();
                Header.ImageName = Utils.ReadNCString(Reader, 0x20);

                return CheckEntryHeader(Header);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CheckEntryHeader(ImageHeader Header)
        {
            if (Header.EndBlock <= Header.StartBlock || Header.FileSize == 0
                || (Header.ImageID == 0
                && (Header.ImageType != ImageType.System || Header.StartBlock != 0
                || Header.EndBlock != 3))
                || Header.ImageName == null)
                return false;
            if (Header.ImageName.Length >= 0x20)
                return false;
            return true;
        }

        private bool ReadEntryData(BinaryReader Reader, ImageHeader Header, out byte[] Data)
        {
            Data = null;

            if (!CheckEntryHeader(Header))
                return false;
  
            try
            {
                long oldPosition = Reader.BaseStream.Position;

                // Check for nand loader
                // If we have got a NAND Loader entry, copy 1/2 a block (0x10000 bytes)
                if (Header.ImageID == 0) // NAND Loader, start from 0x20
                    Reader.BaseStream.Seek(0x20, SeekOrigin.Begin);
                else // Seek to the normal data
                    Reader.BaseStream.Seek(Header.StartBlock * BytesPerBlock, SeekOrigin.Begin);

                // Now copy the entire image
                Data = Reader.ReadBytes((int)((Header.ImageID == 0) ? (Header.FileSize - 0x20)
                    : Header.FileSize));

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
