using NLog;
using System;
using Discord;
using Discord.WebSocket;

namespace Sara
{
    public class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static DiscordSocketClient? _client = null;

        public static async Task Main()
        {
            // setup bot client
            _client = new DiscordSocketClient();
            _client.Log += Log;

            // try to log into Discord
            var botToken = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();

            // start command handler
            var handler = new Handler.CommandHandler(_client, new Discord.Commands.CommandService());
            await handler.InstallCommandsAsync();

            // block task until the program is closed
            await Task.Delay(-1);
        }

        private static async Task Log(LogMessage msg)
        {
            _logger.Info(msg);
        }
    }
}