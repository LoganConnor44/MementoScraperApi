using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi.Models;
using Tweetinvi.Logic.TwitterEntities;

namespace MementoScraperApi.Models {
    /// <summary>
    /// A class to define a Memento.
    /// </summary>
    public class Memento
    {

        public int Id { get; set; }

        public int MementoId { get; set; }

        /// <summary>
        /// Any text associated with a Memento.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// A list of media that are associated with a Memento.
        /// </summary>
        public List<Memory> Memories { get; set; }

        /// <summary>
        /// The person a Memento belongs to.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// The social media a Memento was created from.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The date of the Memento.
        /// </summary>
        public DateTime Creation { get; set; }

        public Memento() {
            
        }

        /// <summary>
        /// The constructor for a Memento created by Twitter.
        /// 
        /// Sets:
        /// * the owner
        /// * the comment
        /// * the type
        /// * a list of memories
        /// </summary>
        /// <param name="tweet"></param>
        public Memento(ITweet tweet)
        {
            this.Owner = tweet.CreatedBy.Name;
            this.Comment = tweet.FullText;
            this.Type = "twitter";
            this.Memories = new List<Memory>();

            foreach (MediaEntity media in tweet.Media)
            {
                this.Memories.Add(this.GetMemory(media));
            }
        }

        /// <summary>
        /// Returns a memory and differentiates between videos/gifs and everything else.
        /// 
        /// Steps:
        /// * Creates a local memory and sets the id and media type
        /// * sets the media url
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        private Memory GetMemory(MediaEntity media)
        {
            var tempMemory = new Memory()
            {
                MemoryId = media.Id,
                MediaType = media.MediaType
            };
            if (tempMemory.MediaType.ToLower().Contains("video") ||
                tempMemory.MediaType.ToLower().Contains("gif"))
            {
                foreach (var vid in media.VideoDetails.Variants)
                {
                    if (vid.ContentType.ToLower().Contains("video"))
                    {
                        tempMemory.Url = vid.URL;
                        break;
                    }
                }
            }
            else
            {
                tempMemory.Url = media.MediaURLHttps;
            }
            return tempMemory;
        }
    }
}