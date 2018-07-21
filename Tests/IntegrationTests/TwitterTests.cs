using System;
using Xunit;
using MementoScraperApi.Models;
using System.Collections.Generic;
using System.Linq;


namespace MementoScraperApi.Tests {
    public class TwitterTests {
        [Fact]
        public void TestInstantiation() {
            var twitter = new Twitter();
            Assert.Equal(typeof(Twitter), twitter.GetType());
        }

        [Fact]
        public void TestReturnITweet() {
            var twitter = new Twitter();
            IEnumerable<Tweetinvi.Models.ITweet> results = twitter.GetSearchFor("#InfinityWar");
            //xunit casting enumerable to list
            Assert.Equal(typeof(List<Tweetinvi.Models.ITweet>), results.GetType());
        }

        [Fact]
        public void TestReturnsMediaOnlyTweets() {
            var twitter = new Twitter();
            var results = twitter.GetSearchFor("#InfinityWar");
            var mediaOnly = twitter.GetTweetsWithMedia(results);
            Assert.NotEmpty(
                mediaOnly.Where(item => item.Media.Count > 0)
            );
        }

        [Fact]
        public void TestCreateMementos() {
            var twitter = new Twitter();
            var results = twitter.GetSearchFor("#InfinityWar");
            var mediaOnly = twitter.GetTweetsWithMedia(results);
            twitter.CreateMementos(mediaOnly);
            Assert.Equal(typeof(List<Memento>), twitter.Mementos.GetType());
        }
    }
}