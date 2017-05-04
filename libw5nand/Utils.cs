using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace libw5nand
{
    public static class Utils
    {
        public static bool WriteNCString(BinaryWriter Writer, string String, int Size)
        {
            try
            {
                if (String.Length + 1 > Size)
                    String = String.Substring(0, Size - 1);

                byte[] strbytes = Encoding.ASCII.GetBytes(String);
                Writer.Write(strbytes);

                for (int i = 0; i < (Size - strbytes.Length); i++)
                    Writer.Write((byte)0x00);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

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
