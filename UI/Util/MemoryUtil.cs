﻿using System.Linq;
using System;

namespace LiveSplit.UI.Util
{
    public enum MemType
    {
        FLOAT,
        INT
    }
    class MemReaderUtil
    { 
        public static string TrimAllWithInplaceCharArray(string str)
        {
            // Fastest for short string inputs

            var len = str.Length;
            var src = str.ToCharArray();
            int dstIdx = 0;

            for (int i = 0; i < len; i++)
            {
                var ch = src[i];

                switch (ch)
                {

                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':

                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':

                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':

                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':

                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        continue;

                    default:
                        src[dstIdx++] = ch;
                        break;
                }
            }
            return new string(src, 0, dstIdx);
        }

        public static string RemoveName(string str)
        {
            if (!str.Contains(':')) return str.Substring(str.IndexOf('.') + 1);
            return str;
        }

        public static int ConvertMemory(Byte[] memory, MemType type)
        {
            int x = -123;
            float f = -123;

            if (type == MemType.INT) x = BitConverter.ToInt32(memory, 0);
            else f = BitConverter.ToSingle(memory, 0);

            if (x != -123) return x;
            else if (f != -123) return (int)f;
            else return 0;

        }

    }
}