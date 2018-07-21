using System;
using System.Collections.Generic;
using System.Text;

namespace MementoScraperApi.Models
{
    public abstract class SocialMediaPlatform
    {
        protected string Name { get; set; }
        protected string Hashtag { get; set; }
        protected Dictionary<int, Memory> Media { get; set; }
        public List<Memento> Mementos { get; set; }
    }
}