using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;


namespace SpongeBot.Source.Commands
{
    public class Quotes : ModuleBase<SocketCommandContext>
    {
        [Command("F"), Alias("f")]
        public async Task FisFor()
        {
            await ReplyAsync("F is for fire that burns down the whole town, U is for uranium..bombs! N is for no survival!");
            await Context.Channel.SendMessageAsync("F is for fire that burns down the whole town, U is for uranium..bombs! N is for no survival!");
        }

        [Command("Hash-slinging")]
        public async Task HashSlinging()
        {
            string[] variations = { "The bash-ringing!", "The hash slinging slasher!", "The slash-bringing hasher", "The sash wringing", "The trash thinging", "The mash flinging", "The flash springing", "The crash thinging" };
            Random rnd = new Random();
            await Context.Channel.SendMessageAsync(variations[rnd.Next(variations.Length)]);
        }

        [Command("drive")]
        public async Task drive()
        {
            await Context.Channel.SendMessageAsync("You don't need a license to drive a sandwich.");
        }

        [Command("What does claustrophobic mean?")]
        public async Task HoHoHo()
        {
            await Context.Channel.SendMessageAsync("It means you're afraid of Santa Claus!");
            await Context.Channel.SendMessageAsync("Ho ho ho!");
        }

        [Command("Why are you mad?")]
        public async Task forehead()
        {
            await Context.Channel.SendMessageAsync("Because I can't see my forehead >:(");
        }

        [Command("What am i?")]
        public async Task stupid()
        {
            await Context.Channel.SendMessageAsync("Uh, stupid?");
        }

        [Command("The best time to wear a striped sweater")]
        public async Task allTheTime()
        {
            await Context.Channel.SendMessageAsync("..is all the timeee!");
        }

        [Command("Story Time")]
        public async Task StoryTime()
        {
            await Context.Channel.SendMessageAsync("Once upon a time, there was an ugly barnacle. He was so ugly that everyone died. The end!");
        }


        [Command("mantra"), Alias("Mantra")]
        public async Task Ugly()
        { 
            await Context.Channel.SendMessageAsync("I'm ugly and I'm proud");
        }
    }
}
