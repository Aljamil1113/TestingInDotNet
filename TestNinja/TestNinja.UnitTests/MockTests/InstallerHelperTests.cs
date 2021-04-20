using Moq;
using NUnit.Framework;
using System;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.MockTests
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> fileDownloader;
        private InstallerHelper installerHelper;

        [SetUp]
        public void SetUp()
        {
            fileDownloader = new Mock<IFileDownloader>();
            installerHelper = new InstallerHelper(fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnTrue()
        {
            var result = installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }
    }
}
