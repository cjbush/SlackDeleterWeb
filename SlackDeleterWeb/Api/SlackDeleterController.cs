using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlackDeleterWeb.Model;
using SlackDeleterWeb.Utilities;

namespace SlackDeleterWeb.Api {
	[Route("api/[controller]")]
	[ApiController]
	public sealed class SlackDeleterController : ControllerBase {

		private readonly ISlackHelper slack;

		public SlackDeleterController(ISlackHelper slack) => this.slack = slack;


		[HttpPost("{SlackApiToken}/{Id}")]
		public async Task<SlackFileDeleteResponse> Post(string SlackApiToken, string Id) => await slack.DeleteFile(Id, SlackApiToken);
	}
}