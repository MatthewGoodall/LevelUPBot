using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LevelUP.Core.Data;

namespace LevelUP
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commandService;


        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            commandService = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });


            client.MessageReceived += MessageReceived;
            await commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                        services: null);
            client.Ready += Ready;
            client.Log += Log;

            string token = "";
            using (var Stream = new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Replace(@"bin\Debug\netcoreapp2.1", "Data/token.txt"), FileMode.Open, FileAccess.Read))
            using (var ReadToken = new StreamReader(Stream))
            {
                token = ReadToken.ReadToEnd();
            }
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Ready()
        {
            await client.SetGameAsync("Dying Horribly");
        }

        private async Task Log(LogMessage log)
        {

            Console.WriteLine($"[{DateTime.Now} at {log.Source}] {log.Message}");
        }

        private async Task MessageReceived(SocketMessage message)
        {
            var arg = message as SocketUserMessage;
            var Context = new SocketCommandContext(client, arg);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;

            if (!(arg.HasStringPrefix("!!", ref ArgPos)) || arg.HasMentionPrefix(client.CurrentUser, ref ArgPos)) {
                await Data.SaveExperience(Context.User.Id, 10);
                Console.WriteLine($"{DateTime.Now} at Commands] {Context.User} has increased there experience by 10. new experience level is {Data.GetExperience(Context.User.Id)}");
                return;
            }

            var result = await commandService.ExecuteAsync(Context, ArgPos, null);
            if (!result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {result.ErrorReason}");
            }
        }

    }
}
