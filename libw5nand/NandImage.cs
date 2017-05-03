using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace libnvtnand
{
    public class NandImage
    {
        BootHeader Header;
        List<ImageEntry> Images;

        const uint BytesPerBlock = 0x20000;

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
            if (!CheckValidEntry(Entry))
                return false;

            Images.Add(Entry);
            return true;
        }

        private bool CheckValidEntry(ImageEntry Entry)
        {
            if (Images == null)
                Images = new List<ImageEntry>();

            if (Entry.ImageID == 0) {
                if (Entry.ImageType != ImageType.System || Entry.StartBlock != 0 || Entry.EndBlock != 3)
                    return false;
            }

            if (Entry.StartBlock >= Entry.EndBlock)
                return false;
            if (Entry.FileSize <= 0)
                return false;

            foreach (ImageEntry image in Images)
            {
                if (Entry.StartBlock >= image.StartBlock && Entry.EndBlock <= image.EndBlock)
                    return false;
            }

            return true;
        }

        private bool HasValidNandLoader()
        {
            ImageEntry nandLoader;
            if (!GetImage(0, out nandLoader))
                return false;
            if (!nandLoader.Name.ToLower().Contains("nandloader"))
               return false;

            return CheckValidEntry(nandLoader);
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

        public bool WriteImage(Stream ImageStream)
        {
            return true;
        }

        public bool ReadImage(Stream ImageStream)
        {
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

        private bool ReadHeader(BinaryReader Reader, out BootHeader Header)
        {
            Header = new BootHeader();

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

            return true;
        }

        private bool ReadEntry(BinaryReader Reader, out ImageEntry Entry)
        {
            Entry = new ImageEntry();

            try
            {
                Entry.ImageID = Reader.ReadUInt16();
                Entry.ImageType = (ImageType)Reader.ReadUInt16();
                Entry.StartBlock = Reader.ReadUInt16();
                Entry.EndBlock = Reader.ReadUInt16();
                Entry.ExecuteAddress = Reader.ReadUInt32();
                Entry.FileSize = Reader.ReadUInt32();
                Entry.Name = Utils.ReadNCString(Reader, 32);

                if (Entry.Name == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            // Check if the entry is valid before reading the data
            if (!CheckValidEntry(Entry))
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
