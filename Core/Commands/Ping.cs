using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace LevelUP.Core.Commands
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping"), Summary("Pong!")]
        public async Task ping()
        {
            await Context.Channel.SendMessageAsync("Pong!");
        }
    }
}
