using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libw5nand
{
    public struct ImageEntry
    {
        public string Name; // Up to 31 chars
        public ImageType Type;
        public uint ExecAddr;
        public byte[] Data;

        public ImageEntry(string Name, ImageType ImageType, uint ExecAddr)
        {
            this.Type = ImageType;
            this.ExecAddr = ExecAddr;
            this.Name = Name;
            this.Data = null;
        }

        public ImageEntry(string Name, ImageType ImageType, uint ExecAddr, byte[] Data)
        {
            this.Name = Name;
            this.Type = ImageType;
            this.ExecAddr = ExecAddr;
            this.Data = Data;
        }
    }
}
