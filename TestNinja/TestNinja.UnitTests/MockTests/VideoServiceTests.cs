using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.MockTests
{
    [TestFixture]
    public class VideoServiceTests
    {
        private Mock<IFileReader> fileReader;
        private Mock<IVideoRepository> repository;
        private VideoService videoService;


        [SetUp]
        public void Setup()
        {
            fileReader = new Mock<IFileReader>();
            repository = new Mock<IVideoRepository>();
            videoService = new VideoService(fileReader.Object, repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideoAsCsv_AllVideosAreProcessed_ReturnAsEmptyString()
        {
            repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideoAsCsv_AFewUnprocessedVideos_ReturnAsStringWithIdOfUnprocessed()
        {
            repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>()
            {
                new Video {Id = 1},
                new Video {Id = 2},
                new Video {Id = 3}
            });

            var result = videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
