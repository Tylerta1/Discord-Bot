using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;

using Newtonsoft.Json;

using SpongeBot.Resources.Settings;
using SpongeBot.Resources.Datatypes;

namespace SpongeBot.Source.Moderation
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        [Command("reload")]
        public async Task Reload()
        {
            if (Context.User.Id != ESettings.owner)
            {
                // not owner
                return;
            }

            string SettingsLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.0", @"Data\Settings.json"));
            if(!File.Exists(SettingsLocation))
            {
                return;
            }

            string JSON = "";
            using (var Stream = new FileStream(SettingsLocation, FileMode.Open, FileAccess.Read))
            using (var ReadSettings = new StreamReader(Stream))
            {
                JSON = ReadSettings.ReadToEnd();
            }
            Setting Settings = JsonConvert.DeserializeObject<Setting>(JSON);
        }
    }
}
