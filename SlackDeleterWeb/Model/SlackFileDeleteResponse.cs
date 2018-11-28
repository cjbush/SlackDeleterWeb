using Newtonsoft.Json;

namespace SlackDeleterWeb.Model {
	public sealed class SlackFileDeleteResponse {
		[JsonProperty("ok")]
		public bool OK { get; set; }
	}
}
