using Microsoft.EntityFrameworkCore;
using InfoScreenPi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace InfoScreenPi.Infrastructure
{
    public static class DbInitializer
    {
        private static InfoScreenContext context;
        public static void Initialize(IServiceProvider serviceProvider, string imagesPath)
        {
            context = (InfoScreenContext)serviceProvider.GetService(typeof(InfoScreenContext));

            InitializeItems(imagesPath);
            InitializeUserRoles();

        }

        private static void InitializeItems(string imagesPath)
        {
            if (!context.Items.Any())
            {

                var _rssFeed1 = context.RssFeeds.Add(
                    new RssFeed
                    {
                        Title = "Het Belang van Limburg : Limburg",
                        Description = "Het Belang van Limburg : Provincie Limburg"
                    }
                ).Entity;
                
                var _itemKind1 = context.ItemKinds.Add(
                    new ItemKind
                    {
                        Description = "RSS",
                        Source = "HBVL"
                    }
                ).Entity;
                
                var _itemKind2 = context.ItemKinds.Add(
                    new ItemKind
                    {
                        Description = "CUSTOM",
                        Source = ""
                    }
                ).Entity;

                var _background1 = context.Backgrounds.Add(
                    new Background
                    {
                        Url = "http://s3.hbvlcdn.be/Assets/Images_Upload/2016/06/18/9061fe5a-356a-11e6-8c71-173369986151_web_scale_0.0607639_0.0607639__.jpg"
                    }
                ).Entity;

                var _background2 = context.Backgrounds.Add(
                    new Background
                    {
                        Url = "http://images.freepicturesweb.com/img1/05/02/26.jpg"
                    }
                ).Entity;

                var _background3 = context.Backgrounds.Add(
                    new Background
                    {
                        Url = "http://s4.hbvlcdn.be/Assets/Images_Upload/2016/06/18/c3299c8c-3559-11e6-8c71-173369986151_web_scale_0.3431373_0.3431373__.jpg"
                    }
                ).Entity;
                
                context.Items.Add(
                    new Item
                    {
                        Soort = _itemKind1,
                        RssFeed = _rssFeed1,
        	            Title = "Sint-Truiden trotseert gietende regen voor Rode Duivels",
        	            Content = "<p>In Sint-Truiden werd het een feestje om niet snel te vergeten. Ondanks de gietende regen zakten de fans massaal af om samen de overwinning van de Rode Duivels te vieren.</p>",
        	            Background = _background1,
        	            Active = true,
        	            Archieved = false
                    }
                );

                context.Items.Add(
                    new Item
                    {
                        Soort = _itemKind2,
                        Title = "Test item",
                        Content = "<center><h2><u>TEST</u> TEST</h2></center>",
                        Background = _background2,
                        Active = true,
                        Archieved = false
                    }
                );

                context.SaveChanges();
            }
        }

        private static void InitializeUserRoles()
        {
            if (!context.Roles.Any())
            {
                // create roles
                context.Roles.AddRange(new Role[]
                {
                new Role()
                {
                    Name="Admin"
                }
                });

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User()
                {
                    Email = "thomasvda@gmail.com",
                    Username = "TVDA",
                    FirstName = "Thomas",
                    LastName = "Vandenabeele",
                    HashedPassword = "9wsmLgYM5Gu4zA/BSpxK2GIBEWzqMPKs8wl2WDBzH/4=",
                    Salt = "GTtKxJA6xJuj3ifJtTXn9Q==",
                    IsLocked = false,
                    DateCreated = DateTime.Now,
                    LastLogin = DateTime.Now
                });

                // create user-admin for TVDA
                context.UserRoles.AddRange(new UserRole[] {
                new UserRole() {
                    RoleId = 1, // admin
                    UserId = 1  // TVDA
                }
            });
                context.SaveChanges();
            }
        }
    }
}