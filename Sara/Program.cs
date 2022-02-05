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
            // retrieve bot token
            var botToken = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
            if (botToken == null)
            {
                _logger.Error("Could not find Discord OAuth2 token in the environment variables");
                return;
            }

            // setup bot client
            _client = new DiscordSocketClient();
            _client.Log += Log;

            // try to log into Discord
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();

            // start command handler
            var handler = new Handler.CommandHandler(_client, new Discord.Commands.CommandService());
            await handler.InstallCommandsAsync();

            // block task until the program is closed
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            _logger.Info(msg);
            return Task.CompletedTask;
        }
    }
}