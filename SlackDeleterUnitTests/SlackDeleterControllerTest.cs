using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SlackDeleterWeb.Api;
using SlackDeleterWeb.Model;
using SlackDeleterWeb.Utilities;

namespace SlackDeleterUnitTests {
	[TestFixture]
	public sealed class SlackDeleterControllerTest {
		private SlackDeleterController subject;
		private Mock<ISlackHelper> slackHelper;

		[SetUp]
		public void SetUp() {
			slackHelper = new Mock<ISlackHelper>();
			subject = new SlackDeleterController(slackHelper.Object);
		}

		[Test]
		public async Task Post_ShouldDeleteFile() {
			slackHelper.Setup(h => h.DeleteFile("bar", "foo")).Returns(Task.FromResult(new SlackFileDeleteResponse {
				OK = true
			})).Verifiable();
			await subject.Post("foo", "bar");
			slackHelper.VerifyAll();
		}

	}
}
