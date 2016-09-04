using System;
using System.Threading.Tasks;
using Discord;

namespace Disbott.Modules
{
    class utils
    {
        internal static Task Ping(MessageEventArgs e, String message)
        {
            if (message == "ping")
            {
                return e.Channel.SendMessage("pong");
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
