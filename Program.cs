using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace DCG_Wiki_Discord_Bot
{
    class Program
    {
        //Set Up Variables
        DiscordClient _client;
        DiscordGuild _guild;
        DiscordChannel _mainChannel;

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

            _guild = await _client.GetGuildAsync(Constants._guildID);
            _mainChannel = await _client.GetChannelAsync(Constants._channelID);

            _client.MessageCreated += MessageCreatedHandler;

            await _client.ConnectAsync();
            await Task.Delay(-1);
        }

        private async Task MessageCreatedHandler(DiscordClient s, MessageCreateEventArgs e)
        {
            var message = e.Message;

            if (!message.Content.StartsWith(Constants._prefix)) return;

            var messageContent = message.Content.Substring(Constants._prefix.Length);
            var parameters = messageContent.Split(' ');

            switch (parameters[0].ToLower())
            {
                case "ratingup":
                    //Only Priviledged Roles can use this Bot
                    if (!PriviledgedRole(e.Author as DiscordMember)) return;
                    
                    break;
                case "ratingdown":
                    //Only Priviledged Roles can use this Bot
                    if (!PriviledgedRole(e.Author as DiscordMember)) return;

                    break;
                case "rank":

                    break;
                case "queue":
                    break;
                case "help":
                    if (parameters.Length > 1 && parameters[1].ToLower() == "mod") await _client.SendMessageAsync(e.Channel, "A list of all commands for moderators.\n\n - ratingup\n - ratingdown", false);
                    else await _client.SendMessageAsync(e.Channel, "A list of all commands.\n\n - rank\n - queue", false);
                    break;
            }
        }

        private bool PriviledgedRole(DiscordMember member)
        {
            //Check for Allowed User Role for Priviledged Commands
            foreach (DiscordRole role in member.Roles)
            {
                foreach (ulong allowedId in Constants._usableRoles)
                {
                    if (role.Id == allowedId) return true;
                }
            }

            return false;
        }
    }
}
