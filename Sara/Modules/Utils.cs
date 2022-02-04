using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using org.mariuszgromada.math.mxparser;

namespace Sara.Modules
{
    public class UtilsModule : ModuleBase<SocketCommandContext>
    {
        [Command("calc")]
        [Summary("Calculates an expression")]
        public async Task CalcAsync([Summary("The expression to calculate")] string expr)
        {
            var parser = new Expression(expr);
            if (!parser.checkSyntax())
            {
                await ReplyAsync($"The syntax of the expression is invalid: { parser.getErrorMessage() }");
                return;
            }

            await ReplyAsync(parser.calculate().ToString());
        }

        [Command("choose")]
        [Summary("Makes a choice for you")]
        public async Task ChooseAsync([Summary("Space-separated possible choices")] params string[] args)
            => await ReplyAsync($"I choose: { args[new Random().Next(0, args.Length)] }");
    }
}
