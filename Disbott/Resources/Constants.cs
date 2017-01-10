using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disbott
{
    public static class Constants
    {
#if DEBUG
            public static string quotePath = Path.GetDirectoryName(Path.GetDirectoryName
                (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))).Remove(0, 6) + "\\bin\\Debug\\quotestest.db";

            public static string remindMePath = Path.GetDirectoryName(Path.GetDirectoryName
                (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))).Remove(0, 6) + "\\bin\\Debug\\remindmetest.db";

            public static string pollPath = Path.GetDirectoryName(Path.GetDirectoryName
                (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))).Remove(0, 6) + "\\bin\\Debug\\polltest.db";

            public static string MessageCountPath = Path.GetDirectoryName(Path.GetDirectoryName
                (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))).Remove(0, 6) + "\\bin\\Debug\\MessageCounttest.db";
#else
        public static string quotePath = "Quote.db";
        public static string remindMePath = "remindMe.db";
        public static string pollPath = "poll.db";
        public static string MessageCountPath = "MessageCount.db";
#endif

    }
}
