using System.Threading.Tasks;
using Disbott.Controllers;
using NUnit.Framework;

namespace DisbottUnitTests
{
    /// <summary>
    /// Tests to confirm coinflips work as intended
    /// </summary>
    [TestFixture]
    public class GiphyOperations
    {
        /// <summary>
        /// Validate that giphy returns a gif
        /// </summary>
        [Test]
        public async Task Gif_is_a_gif()
        {
            var gif = await GiphyController.GetRandomGif("test");
            Assert.That(gif, Does.StartWith("http"));
            Assert.That(gif, Does.EndWith("/giphy.gif"));
        }
    }
}
