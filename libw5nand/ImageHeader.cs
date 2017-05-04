using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libw5nand
{
    public struct ImageHeader
    {
        public ushort ImageID;
        public string ImageName; // Up to 31 chars
        public ImageType ImageType;
        public ushort StartBlock;
        public ushort EndBlock;
        public uint ExecuteAddress;
        public uint FileSize;
    }
}
