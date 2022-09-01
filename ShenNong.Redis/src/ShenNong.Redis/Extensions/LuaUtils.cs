using System;
using System.Collections.Generic;
using System.Text;

namespace ShenNong.Redis.Extensions
{
    public class LuaUtils
    {
        public static string[] SingletonList(string str) {
            return new string[1] { str };
        }
    }
}
