using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libnvtnand
{
    public enum ImageType : ushort
    {
        Execute = 1,
        ROFS = 2,
        System = 3,
        Logo = 4,
        Data = 5 // Probably
    }
}
