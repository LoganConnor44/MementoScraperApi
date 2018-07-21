using System;
using Xunit;
using MementoScraperApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MementoScraperApi.Tests
{
    public class MementoTests
    {

        /// <summary>
        /// Retrieves tweets that contain media from a defined hashtag.
        /// </summary>
        /// <param name="hashtag"></param>
        /// <returns></returns>
        public IEnumerable<Tweetinvi.Models.ITweet> SetupCreateMedia(string hashtag)
        {
            var twitter = new Twitter();
            var tweets = twitter.GetSearchFor(hashtag);
            return twitter.GetTweetsWithMedia(tweets);
        }

        /// <summary>
        /// Verify that mementos are being created correctly.
        /// 
        /// Setup:
        /// * Create a list of Memento
        /// * Get tweets that contain media
        /// 
        /// Test:
        /// * Iterate through the media and create a memento for each
        /// * The list created is of type Memento
        /// * The list has memento objects
        /// </summary>
        [Fact]
        public void TestCreateMemento()
        {
            List<Memento> mementos = new List<Memento>();
            var mediaOnly = this.SetupCreateMedia("#InfinityWar");

            foreach (Tweetinvi.Models.ITweet tweet in mediaOnly)
            {
                var memento = new Memento(tweet);
                mementos.Add(memento);
            }
            Assert.Equal(typeof(List<Memento>), mementos.GetType());
            Assert.True(mementos.Count() > 0);
        }

        [Fact]
        ///not really a test - just development code
        public void TestIdentifyMediaTypes()
        {
            var twitter = new Twitter();
            var results = twitter.GetSearchFor("#InfinityWar");
            var mediaOnly = twitter.GetTweetsWithMedia(results);
            twitter.CreateMementos(mediaOnly);

            List<string> typesOfMedia = new List<string>();
            List<string> mediaUrl = new List<string>();
            foreach (var memory in mediaOnly)
            {
                foreach (var med in memory.Media)
                {
                    if (med.MediaType.ToLower() != "photo" && !typesOfMedia.Contains(med.MediaType))
                    {
                        typesOfMedia.Add(med.MediaType);
                        mediaUrl.Add(med.MediaURL);
                    }
                }
            }

            List<string> typesOfMedia2 = new List<string>();
            List<string> mediaUrl2 = new List<string>();
            foreach (var memento in twitter.Mementos)
            {
                foreach (var mem in memento.Memories)
                {
                    if (mem.MediaType.ToLower() != "photo" && !typesOfMedia2.Contains(mem.MediaType))
                    {
                        typesOfMedia2.Add(mem.MediaType);
                        mediaUrl2.Add(mem.Url);
                    }
                }
            }

            foreach (var item in typesOfMedia)
            {
                Assert.Contains(
                    item,
                    typesOfMedia2
                );
            }
            foreach (var item in mediaUrl2)
            {
                Assert.DoesNotContain(
                    item,
                    mediaUrl
                );
            }
            Assert.Equal(
                typesOfMedia.Count(),
                typesOfMedia2.Count()
            );
            Assert.Equal(
                mediaUrl.Count(),
                mediaUrl2.Count()
            );
        }
    }
}
