using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackDeleterWeb.Model {
	public sealed class SlackFileListResponse {

		[JsonProperty("ok")]
		public bool OK { get; set; }

		[JsonProperty("files")]
		public IEnumerable<SlackFile> Files { get; set; }

		[JsonProperty("paging")]
		public SlackFileListPaging Paging { get; set; }

	}
}
