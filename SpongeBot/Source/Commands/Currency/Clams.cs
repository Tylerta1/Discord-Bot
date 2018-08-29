using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using SpongeBot.Source.Data;
using SpongeBot.Resources.Database;
using System.Linq;
namespace SpongeBot.Source.Commands.Currency
{
    public class Clams : ModuleBase<SocketCommandContext>
    {
        [Group("Clams"), Alias("clams")]
        public class ClamsGroup : ModuleBase<SocketCommandContext>
        {
            [Command(""), Alias("me", "my")]
            public async Task Me(IUser User = null)
            {
                if (User == null)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User}, you have {Data.DataHandler.GetClams(Context.User.Id)} clams!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{User.Username}, you have {Data.DataHandler.GetClams(User.Id)} clams!");
                }
            }

            [Command("give"), Alias("gift")]
            public async Task Give(IUser User = null, int Amount = 0)
            {
                if (User == null)
                {
                    await Context.Channel.SendMessageAsync("give user command");
                    return;
                }

                if (User.IsBot)
                {
                    await Context.Channel.SendMessageAsync("no bot");
                    return;
                }

                if (Amount == 0)
                {
                    await Context.Channel.SendMessageAsync("no monies");
                    return;
                }

                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.Administrator)
                {
                    await Context.Channel.SendMessageAsync("You dont have admin permission to execute this command");
                    return;
                }

                await Context.Channel.SendMessageAsync($":tada: {User.Mention} you have received **{Amount}** clams from {Context.User.Username}");

                await Data.DataHandler.SaveClams(User.Id, Amount);
            }

            [Command("reset")]
            public async Task Reset(IUser User = null)
            {
                if (User == null)
                {
                    await Context.Channel.SendMessageAsync("Please specify who you want to reset");
                    return;
                }
                if (User.IsBot)
                {
                    return;
                }
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.Administrator)
                {
                    await Context.Channel.SendMessageAsync("You dont have admin permission to execute this command");
                    return;
                }
                await Context.Channel.SendMessageAsync($":skull: {User.Mention}, you have been reset by {Context.User.Username}! This means you have lost all your clams!");

                using (var DbContext = new SqliteDbContext())
                {
                    DbContext.Clams.RemoveRange(DbContext.Clams.Where(x => x.UserID == User.Id));
                    await DbContext.SaveChangesAsync();
                }
            }
        }
    }
}
