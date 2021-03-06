﻿namespace iPodcastSearch.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class iTunesSearchClientTests
    {
        private IPodcastSearchClient client;

        [TestInitialize]
        public void InitializeTest()
        {
            this.client = new iTunesSearchClient();
        }

        [TestMethod]
        public async Task GetPodcastsAsync_ValidSearchQuery_ReturnsPodcasts()
        {
            //  Arrange
            var query = "Just in time";

            //  Act
            var items = await this.client.SearchPodcastsAsync(query);

            //  Assert
            Assert.IsTrue(items.Any());
        }


        [TestMethod]
        public async Task GetPodcastsAsync_ValidSearchQuery_LimitResults_ReturnsPodcasts()
        {
            //  Arrange
            var query = "Just in time";

            //  Act
            var items = await this.client.SearchPodcastsAsync(query, 1);


            //  Assert
            Assert.IsTrue(items.Count == 1);
        }

        [TestMethod]
        public async Task GetPodcastsAsync_ValidSearchQuery_LimitByCountry_ReturnsPodcasts()
        {
            //  Arrange
            var query = "Just in time";
            var languageInput = "es";
            var languageOutput = "ESP";

            //  Act
            var items = await this.client.SearchPodcastsAsync(query, 100, languageInput);
            var actual = items.FirstOrDefault();

            //  Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(languageOutput, actual.Language);
        }

        [TestMethod]
        public async Task GetPodcastById_ValidId_ReturnsPodcast()
        {
            //  Arrange
            long podcastId = 1150376726;

            //  Act
            var podcast = await this.client.GetPodcastByIdAsync(podcastId, false);

            //  Assert
            Assert.IsNotNull(podcast);
            Assert.AreEqual("Just in Time Podcast", podcast.Name);
            Assert.AreEqual("http://jitpodcast.com/feed/podcast", podcast.FeedUrl);
        }

        [TestMethod]
        public async Task GetPodcastById_ValidId_WithEpisodes_ReturnsPodcastWithEpisodes()
        {
            //  Arrange
            long podcastId = 1150376726;

            //  Act
            var podcast = await this.client.GetPodcastByIdAsync(podcastId, true);

            //  Assert
            Assert.IsNotNull(podcast);
            Assert.AreEqual("Just in Time Podcast", podcast.Name);
            Assert.AreEqual("http://jitpodcast.com/feed/podcast", podcast.FeedUrl);
            Assert.IsTrue(podcast.Episodes.Any());
            Assert.AreEqual(podcast.EpisodeCount, podcast.Episodes.Count);
        }

        [TestMethod]
        public async Task GetPodcastEpisodes_ValidFeed_ReturnsEpisodes()
        {
            //  Arrange
            var feedUrl = "http://jitpodcast.com/feed/podcast";

            //  Act
            var episodes = await this.client.GetPodcastEpisodesAsync(feedUrl);

            //  Assert
            Assert.IsTrue(episodes.Any());
        }


        [TestMethod]
        public async Task GetPodcastEpisodes_ValidFeedFromSimplePodcastWordpress_ReturnsData()
        {
            //  Arrange
            var feedUrl = "http://jitpodcast.com/feed/podcast";

            //  Act
            var podcast = await this.client.GetPodcastFromFeedUrlAsyc(feedUrl);

            //  Assert
            Assert.IsNotNull(podcast);
            Assert.IsTrue(podcast.Episodes.Any());
        }

        [TestMethod]
        public async Task GetPodcastEpisodes_ValidFeedFromSoundcloud_ReturnsData()
        {
            //  Arrange
            var feedUrl = "http://feeds.soundcloud.com/users/soundcloud:users:156542883/sounds.rss";

            //  Act
            var podcast = await this.client.GetPodcastFromFeedUrlAsyc(feedUrl);

            //  Assert
            Assert.IsNotNull(podcast);
            Assert.IsTrue(podcast.Episodes.Any());
        }


      
    }
}