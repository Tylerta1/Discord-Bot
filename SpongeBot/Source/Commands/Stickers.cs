using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using SpongeBot.Source.Data;
using SpongeBot.Resources.Datatypes;

namespace SpongeBot.Source.Commands
{
    public class Stickers : ModuleBase<SocketCommandContext>
    {
        [Command("getsticker"), Summary("user gets sticker")]
        public async Task GetSticker()
        {

            Sticker Generated = Data.DataHandler.GetSticker();
            if(Generated == null)
            {
                return;
            }
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor($"Here is your sticker - {Generated.name}");
            embed.WithImageUrl(Generated.file);
            embed.WithFooter(Generated.descript);

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }
    }
}
