using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Sara.Modules
{
    public class BasicModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Pings the bot to get a response")]
        public async Task PingAsync()
            => await ReplyAsync("Pong!");

        [Command("info")]
        [Summary("Gets information from a specific user")]
        [RequireGuildPrecondition]
        public async Task InfoAsync([Summary("The user to get information from")] SocketUser? user = null)
        {
            var target = (user as SocketGuildUser) ?? (Context.User as SocketGuildUser);
            if (target == null)
            {
                await ReplyAsync("Invalid user");
                return;
            }

            var eb = new EmbedBuilder()
            .AddField("Id", target.Id)
            .AddField("Nickname", target.Nickname ?? "Not set")
            .AddField("Created at", target.CreatedAt)
            .AddField("Joined at", target.JoinedAt, true)
            .WithAuthor($"{target.Username}#{target.Discriminator}", iconUrl: target.GetAvatarUrl());

            await Context.Channel.SendMessageAsync(embed: eb.Build());
        }

        [Command("channelinfo")]
        [Summary("Gets information from a specific channel")]
        [RequireGuildPrecondition]
        public async Task ChannelAsync([Summary("The channel to get information from")] SocketGuildChannel channel)
        {
            var eb = new EmbedBuilder()
            .AddField("Id", channel.Id)
            .AddField("Created at", channel.CreatedAt)
            .AddField("User count", channel.Users.Count)
            .AddField("Name", channel.Name, true)
            .WithAuthor(channel.Name, iconUrl: Context.Guild.IconUrl);

            await Context.Channel.SendMessageAsync(embed: eb.Build());
        }

        [Command("avatar")]
        [Alias("avi")]
        [Summary("Get avatar from a specific user")]
        public async Task AvatarAsync(SocketUser? user = null)
            => await ReplyAsync((user ?? Context.User).GetAvatarUrl(size: 2048));
    }
}