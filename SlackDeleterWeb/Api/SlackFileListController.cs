using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlackDeleterWeb.Model;
using SlackDeleterWeb.Utilities;

namespace SlackDeleterWeb.Api {
	[Route("api/[controller]")]
	[ApiController]
	public sealed class SlackFileListController : ControllerBase {

		private readonly ISlackHelper slack;

		public SlackFileListController(ISlackHelper slack) => this.slack = slack;


		[HttpGet("{token}")]
		[HttpGet("{token}/{days}")]
		public async Task<SlackFileListResponse> Get(string token, uint days = 30) => await slack.RetrieveFilesOlderThan(days, token);



	}
}