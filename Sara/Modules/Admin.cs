using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Sara.Modules
{
    public class Admin : ModuleBase<SocketCommandContext>
    {
        [Command("ban")]
        [Summary("Bans an user from the server")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanAsync([Summary("User that should be banned")] SocketGuildUser user,
            [Summary("Reason for the ban")] string? reason = null,
            [Summary("If their previous messages should be pruned")] int pruneDays = 0)
        {
            if (user == Context.User)
            {
                await ReplyAsync("You can't ban yourself!");
                return;
            }

            await user.BanAsync(pruneDays, reason);
        }

        [Command("kick")]
        [Summary("Kicks an user from the server")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickAsync([Summary("User that should be kicked")] SocketGuildUser user,
                                    [Summary("Reason for the kick")] string? reason = null)
        {
            if (user == Context.User)
            {
                await ReplyAsync("You can't kick yourself!");
                return;
            }

            await user.KickAsync(reason);
        }

        [Command("purge")]
        [Summary("Cleans the last N messages from the current channel")]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task PurgeAsync([Summary("How many of the previous messages should be deleted")] int msgCount)
        {
            if (msgCount > 250)
            {
                await ReplyAsync("Message count exceeds maximum (250).");
                return;
            }

            var msg = (await Context.Channel.GetMessagesAsync(msgCount + 1).FlattenAsync());
            foreach (var m in msg)
            {
                await m.DeleteAsync();
            }
        }
    }
}
