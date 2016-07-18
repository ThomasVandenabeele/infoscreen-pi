using System;
using InfoScreenPi.Entities;

namespace InfoScreenPi.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string KindDescription { get; set; }
        public string KindSource { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BackgroundUrl { get; set; }
        public Boolean Active { get; set; }
        public Boolean Archieved { get; set; }

        public ItemViewModel(Item i)
        {
            Id = i.Id;
            KindDescription = i.Soort.Description;
            KindSource = i.Soort.Source;
            Title = i.Title;
            Content = i.Content;
            BackgroundUrl = i.Background.Url;
            Active = i.Active;
            Archieved = i.Archieved;
        }

    }    

    

}