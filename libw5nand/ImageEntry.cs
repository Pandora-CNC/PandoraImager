using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libnvtnand
{
    public struct ImageEntry
    {
        public ushort ImageID;
        public ImageType ImageType;
        public ushort StartBlock;
        public ushort EndBlock;
        public uint ExecuteAddress;
        public uint FileSize;
        public string Name; // Up to 31 chars
        public byte[] Data;

        public ImageEntry(ushort ImageID, ImageType ImageType, ushort StartBlock, ushort EndBlock,
            uint ExecuteAddress, uint FileSize, string Name)
        {
            this.ImageID = ImageID;
            this.ImageType = ImageType;
            this.StartBlock = StartBlock;
            this.EndBlock = EndBlock;
            this.ExecuteAddress = ExecuteAddress;
            this.FileSize = FileSize;
            this.Name = Name;
            this.Data = null;
        }

        public ImageEntry(ushort ImageID, ImageType ImageType, ushort StartBlock, uint ExecuteAddress,
            string Name, byte[] Data)
        {
            this.ImageID = ImageID;
            this.ImageType = ImageType;
            this.StartBlock = StartBlock;
            this.ExecuteAddress = ExecuteAddress;
            this.FileSize = (uint)Data.Length;
            this.Name = Name;
            this.Data = Data;

            if (FileSize % NandImage.BytesPerBlock > 0)
                this.EndBlock = (ushort)(((FileSize - (FileSize % NandImage.BytesPerBlock))
                    / NandImage.BytesPerBlock) + 1);
            else
                this.EndBlock = (ushort)(FileSize / NandImage.BytesPerBlock);
            this.EndBlock += this.StartBlock;
        }
    }
}
