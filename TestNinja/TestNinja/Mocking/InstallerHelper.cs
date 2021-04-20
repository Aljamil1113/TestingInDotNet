using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private readonly IFileDownloader fileDownloader;
        private string _setupDestinationFile;
        public InstallerHelper(IFileDownloader _fileDownloader)
        {
           fileDownloader = _fileDownloader;
        }
        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
               fileDownloader.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}