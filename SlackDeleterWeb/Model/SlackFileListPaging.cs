using Newtonsoft.Json;

namespace SlackDeleterWeb.Model {
	public sealed class SlackFileListPaging {

		[JsonProperty("count")]
		public uint Count { get; set; }

		[JsonProperty("total")]
		public uint Total { get; set; }

		[JsonProperty("page")]
		public uint Page { get; set; }

		[JsonProperty("pages")]
		public uint Pages { get; set; }

	}
}
