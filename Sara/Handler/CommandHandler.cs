using System;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Sara.Handler
{
    internal class CommandHandler
    {
        private CommandService _cmds;
        private DiscordSocketClient _client;

        public CommandHandler(DiscordSocketClient client, CommandService cmds)
        {
            _cmds = cmds;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            // hook for parsing text messages
            _client.MessageReceived += HandleCommandAsync;

            // add all modules into command handler
            await _cmds.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if ((msg == null) || msg.Author.IsBot) 
                return;

            int argPos = 0;
            if (!(msg.HasCharPrefix('!', ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos)))
                return;

            var ctx = new SocketCommandContext(_client, msg);
            await _cmds.ExecuteAsync(context: ctx, argPos: argPos, services: null);
        }
    }
}
