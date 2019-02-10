using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LevelUP.Core.Data;

namespace LevelUP.Core.Currency
{
    public class Stones : ModuleBase<SocketCommandContext>
    {
        [Group("stone"), Alias("stones")]
        public class StonesGroup : ModuleBase<SocketCommandContext> {
            [Command(""), Alias("me", "my")]
            public async Task Me(IUser User = null) {
                if(User == null)
                    await Context.Channel.SendMessageAsync($"{Context.User}, you have {Data.Data.GetStones(Context.User.Id)} stones!");
                else
                    await Context.Channel.SendMessageAsync($"{User.Username}, has {Data.Data.GetStones(User.Id)} stones!");
            }

            [Command("give"), Alias("gift")]
            public async Task Give(IUser User = null, int Amount = 0) {
                if (User == null) {
                    await Context.Channel.SendMessageAsync(":x: You did not specify a user. Please use this syntax: !!stones give **<user>** <amount>");
                    return;
                }

                if (User.IsBot) {
                    await Context.Channel.SendMessageAsync(":x: Silly bot. Stones are for real children.....");
                    return;
                }

                if (Amount == 0) {
                    await Context.Channel.SendMessageAsync(":x: Come on. You need to specify a real amount!");
                    return;
                }

                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.Administrator) {
                    await Context.Channel.SendMessageAsync("You need to be a admin to send stones.");
                    return;
                }

                await Context.Channel.SendMessageAsync($":tada: {User.Mention} you have received **{Amount}** of stones from {Context.User.Username}!");

                await Data.Data.SaveStones(User.Id, Amount);
            }
        }

    }
}
