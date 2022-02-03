using System;
using Discord.Commands;

namespace Sara.Modules
{
    public class BasicModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Pings the bot to get a response")]
        public async Task PingAsync()
            => await ReplyAsync("Pong!");
    }
}
