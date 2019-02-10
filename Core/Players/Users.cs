using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LevelUP.Core.Players
{
    public class Users : ModuleBase<SocketCommandContext>
    {
        [Command("CurrentLevel"), Alias("cl", "CL")]
        public async Task Level(IUser User)
        {
            int level = Data.Data.GetLevel(User.Id);
            Console.WriteLine(level);
            await Context.Channel.SendMessageAsync($"{User.Mention} your level is {level}");
        }
    }
}
