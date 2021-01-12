using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace DCG_Wiki_Discord_Bot
{
    class Program
    {
        //Constants
        ulong _guildID = 0;
        //Set Up Variables
        DiscordClient _client;
        DiscordGuild _guild;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {

            _client = new DiscordClient(new DiscordConfiguration()
            {
                Token = ConfigurationManager.AppSettings.Get("token"),
                TokenType = TokenType.Bot,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Critical
            });

            _guild = await _client.GetGuildAsync(_guildID);
        }
    }
}
