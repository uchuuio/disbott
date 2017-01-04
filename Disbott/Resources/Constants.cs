﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disbott
{
    public static class Constants
    {
        public static string quotePath = Path.GetDirectoryName(Path.GetDirectoryName
            (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))).Remove(0, 6) + "\\bin\\Debug\\quotes.db";

        public static string remindMePath = Path.GetDirectoryName(Path.GetDirectoryName
            (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))).Remove(0, 6) + "\\bin\\Debug\\remindme.db";

        public static string pollPath = Path.GetDirectoryName(Path.GetDirectoryName
            (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))).Remove(0, 6) + "\\bin\\Debug\\poll.db";
    }
}
