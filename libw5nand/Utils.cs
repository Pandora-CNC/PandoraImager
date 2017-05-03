using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace libnvtnand
{
    public static class Utils
    {
        public static string ReadNCString(BinaryReader Reader, int Size)
        {
            long startPos = Reader.BaseStream.Position;
            string str = "";
            char chr;

            try
            {
                for (int i = 0; i < Size; i++ )
                {
                    if((chr = Reader.ReadChar()) != '\0')
                        str += chr;
                }
            }
            catch (Exception)
            {
                Reader.BaseStream.Position = startPos;
                return null;
            }

            return str;
        }

        public static string ReadCString(BinaryReader Reader)
        {
            long startPos = Reader.BaseStream.Position;
            string str = "";
            char chr;

            try
            {
                while ((chr = Reader.ReadChar()) != '\0')
                {
                    str += chr;
                }
            }
            catch (Exception)
            {
                Reader.BaseStream.Position = startPos;
                return null;
            }

            return str;
        }
    }
}
