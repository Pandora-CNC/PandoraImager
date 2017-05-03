using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libnvtnand
{
    public struct BootHeader
    {
        public uint BootCodeMarker;
        public uint ExecuteAddress;
        public uint ImageSize;
        public uint SkewMarker;
        public uint DQSODS;
        public uint CLKQSDS;
        public uint DramMarker;
        public uint DramSize;
    }
}
