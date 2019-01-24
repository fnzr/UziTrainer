using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer
{
    static class Constants
    {
        public static String AppDir = Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.ApplicationData), "UziTrainer");
        public static String DebugDir = Path.Combine(AppDir, "Debug");

        public static int TraceDebug = 1;
    }
}
