using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace SpongeBot.Source.Commands
{
    public class EmbedExample : ModuleBase<SocketCommandContext>
    {

        [Command("embed"), Summary("embed")]
        public async Task Embed([Remainder]string Input = "None")
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Test embed", Context.User.GetAvatarUrl());
            Embed.WithColor(40, 200, 150);
            Embed.WithFooter("The footer of the embed", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithDescription("dumm");
            Embed.AddInlineField("UserInput: ", Input);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
