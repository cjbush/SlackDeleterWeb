using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SlackDeleterWeb.Api;
using SlackDeleterWeb.Model;
using SlackDeleterWeb.Utilities;

namespace SlackDeleterUnitTests {
	[TestFixture]
	public sealed class SlackFileListControllerTest {

		private SlackFileListController subject;
		private Mock<ISlackHelper> slackHelper;

		[SetUp]
		public void SetUp() {
			slackHelper = new Mock<ISlackHelper>();
			subject = new SlackFileListController(slackHelper.Object);
		}

		[Test]
		public async Task Get_ShouldReturnValidResponse() {
			slackHelper.Setup(h => h.RetrieveFilesOlderThan(30, "foo")).Returns(Task.FromResult(new SlackFileListResponse {
				OK = true,
				Files = new List<SlackFile> { },
				Paging = new SlackFileListPaging { }
			})).Verifiable();
			await subject.Get("foo", 30);
			slackHelper.VerifyAll();
		}

	}
}
