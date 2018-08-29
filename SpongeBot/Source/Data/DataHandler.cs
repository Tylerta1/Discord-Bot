using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SpongeBot.Resources.Database;
using SpongeBot.Resources.Datatypes;
using System.Reflection;
using System.IO;
using System.Xml;

namespace SpongeBot.Source.Data
{
    public static class DataHandler
    {
        public static int GetDubloons(ulong UserId)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Dubloons.Where(x => x.UserID == UserId).Count() < 1)
                {
                    return 0;
                }
                return DbContext.Dubloons.Where(x => x.UserID == UserId).Select(x => x.Amount).FirstOrDefault();
            }
        }
        public static int GetClams(ulong UserId)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if(DbContext.Clams.Where(x => x.UserID == UserId).Count() < 1)
                {
                    return 0;
                }
                return DbContext.Clams.Where(x => x.UserID == UserId).Select(x => x.Amount).FirstOrDefault();
            }
        }



        public static async Task SaveDubloons(ulong UserId, int amount)
        {
           
            using (var DbContext = new SqliteDbContext())
            {
                if(DbContext.Dubloons.Where(x => x.UserID == UserId).Count() < 1)
                {
                    DbContext.Dubloons.Add(new Dubloon
                    {
                        UserID = UserId,
                        Amount = amount
                    });
                }
                else
                {
                    Dubloon Current = DbContext.Dubloons.Where(x => x.UserID == UserId).FirstOrDefault();
                    Current.Amount += amount;
                    DbContext.Dubloons.Update(Current);
                }
                await DbContext.SaveChangesAsync();
            }
        }

        public static async Task SaveClams(ulong UserId, int amount)
        {

            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Clams.Where(x => x.UserID == UserId).Count() < 1)
                {
                    DbContext.Clams.Add(new Clam
                    {
                        UserID = UserId,
                        Amount = amount
                    });
                }
                else
                {
                    Clam Current = DbContext.Clams.Where(x => x.UserID == UserId).FirstOrDefault();
                    Current.Amount += amount;
                    DbContext.Clams.Update(Current);
                }
                await DbContext.SaveChangesAsync();
            }
        }

        public static Sticker GetSticker()
        {
            string StickerLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.0", @"Data\Stickers.xml"));
            if(!File.Exists(StickerLocation))
            {
                return null;
            }

            FileStream Stream = new FileStream(StickerLocation, FileMode.Open, FileAccess.Read);
            XmlDocument Doc = new XmlDocument();
            Doc.Load(Stream);
            Stream.Dispose();

            List<Sticker> Stickers = new List<Sticker>();
            foreach (XmlNode Node in Doc.DocumentElement)
            {
                Stickers.Add(new Sticker { name = Node.ChildNodes[0].InnerText, file = Node.ChildNodes[1].InnerText, descript = Node.ChildNodes[2].InnerText });
                // Console.WriteLine($"{Node.ChildNodes[0]}, File: {Node.ChildNodes[1]}, Description: {Node.ChildNodes[2]}");
            }
            Random rnd = new Random();
            int Num = rnd.Next(Stickers.Count());

            return Stickers[Num];
        }
    }
}
