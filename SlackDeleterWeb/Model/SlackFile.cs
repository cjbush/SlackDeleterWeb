using Newtonsoft.Json;

namespace SlackDeleterWeb.Model {
	public sealed class SlackFile {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("created")]
		public uint Created { get; set; }

		[JsonProperty("timestamp")]
		public uint Timestamp { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("pretty_type")]
		public string FileType { get; set; }

		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("permalink")]
		public string Permalink { get; set; }
	}
}
