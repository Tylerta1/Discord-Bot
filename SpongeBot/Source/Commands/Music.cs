using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SpongeBot.Source.Commands
{
    public class Music : ModuleBase<SocketCommandContext>
    {
        [Command("play")]
        public async Task Play(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if(channel == null)
            {
                await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument.");
                return;
            }

            var audioClient = await channel.ConnectAsync();
        }
    }
}
