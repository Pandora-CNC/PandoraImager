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
    }
}
