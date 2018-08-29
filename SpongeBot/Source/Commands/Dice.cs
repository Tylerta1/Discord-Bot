using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;

namespace SpongeBot.Source.Commands
{
    public class Dice : ModuleBase<SocketCommandContext>
    {
        [Command("dice"), Alias("Dice", "roll", "Roll")]
        public async Task Roll()
        {
            string[] diceSides = { "Escalators", "Eel" };
            Random rnd = new Random();
            await Context.Channel.SendMessageAsync(diceSides[rnd.Next(2)]);
        }
    }
}
