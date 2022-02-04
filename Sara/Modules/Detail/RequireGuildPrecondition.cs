using Discord.Commands;
using Discord.WebSocket;

namespace Sara.Modules
{
    public class RequireGuildPrecondition : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (context.User is SocketGuildUser)
                return Task.FromResult(PreconditionResult.FromSuccess());

            return Task.FromResult(PreconditionResult.FromError("This command must be ran on a server."));
        }
    }
}
