using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Newtonsoft.Json;
using SpongeBot.Resources.Datatypes;
using SpongeBot.Resources.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace SpongeBot
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            // Get bot settings to start up
            BotSettings myBotSettings = LoadJson();

            // Initialize Client
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            // Initialize CommandService
            commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Info
            });

            IServiceCollection serviceCollection = new ServiceCollection();
            //serviceCollection.AddSingleton()
            services = new ServiceCollection().BuildServiceProvider();

            client.MessageReceived += Client_commandHandler;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            client.Ready += Client_Ready;
            client.Log += Client_Log;

            await client.LoginAsync(TokenType.Bot, myBotSettings.token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage message)
        {
            Console.WriteLine($"{DateTime.Now}: {message.Message}");
            // Debugging through a text channel on the server, will fix after I fix static settings class
            //try
            //{
            //    SocketGuild Guild = Client.Guilds.Where(x => x.Id == myBotSettings.log[0]).FirstOrDefault();
            //    SocketTextChannel channel = Guild.Channels.Where(x => x.Id == ESettings.log[1]).FirstOrDefault() as SocketTextChannel;
            //    await channel.SendMessageAsync($"{DateTime.Now} at {arg.Source}] {arg.Message}");
            //}
            //catch
            //{

            //}

        }

        private async Task Client_Ready()
        {
            Console.WriteLine("Spongebot is ready!");
            await client.SetGameAsync("Krusty Krab Pizza", "https://www.google.com/", StreamType.NotStreaming);
        }

        private async Task Client_commandHandler(SocketMessage msgParam)
        {
            var message = msgParam as SocketUserMessage;
            var context = new SocketCommandContext(client, message);

            if (context.Message == null || context.Message.Content == "") return;
            if (context.User.IsBot) return;

            await MiscBotCommands(context, message);

            int strIndex = 0;
            if (!(message.HasStringPrefix("$", ref strIndex) || message.HasMentionPrefix(client.CurrentUser, ref strIndex))) return;

            var Result = await commands.ExecuteAsync(context, strIndex);
            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {context.Message.Content} | Error: {Result.ErrorReason}");
        }

        private async Task MiscBotCommands(SocketCommandContext context, SocketUserMessage message)
        {
            // Extra stuff I thought was funny, would work without command Prefix
            int strIndex = 0;
            if (message.HasStringPrefix("this bot is stupid", ref strIndex))
            {
                await context.Channel.SendMessageAsync("Well, it may be stupid, but it's also dumb");
            }
            if (message.HasStringPrefix("it could be worse", ref strIndex))
            {
                await context.Channel.SendMessageAsync("Yeah. You could be bald and have a big nose");
            }
        }

        private BotSettings LoadJson()
        {
            // Get settings for bot from JSON file
            string JSON = "";
            string SettingsLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.0", @"Data\Settings.json"));
            if (!File.Exists(SettingsLocation))
            {
                Console.WriteLine($"{DateTime.Now}: File for settings is not found");
                return null;
            }
            using (var Stream = new FileStream(SettingsLocation, FileMode.Open, FileAccess.Read))
            using (StreamReader readSettings = new StreamReader(Stream))
            {
                JSON = readSettings.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<BotSettings>(JSON);
        }
    }
}
