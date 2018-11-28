using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SlackDeleterWeb.Model;

namespace SlackDeleterWeb.Utilities {
	public sealed class SlackHelper : ISlackHelper {

		private readonly HttpClient http;

		public SlackHelper() => http = new HttpClient();


		public async Task<SlackFileDeleteResponse> DeleteFile(string fileId, string token) => await ExecuteSlackRequest<SlackFileDeleteResponse>("files.delete", new Dictionary<string, string> { { "token", token }, { "file", fileId } });
		public async Task<SlackFileListResponse> RetrieveFilesOlderThan(uint days, string token) => await ExecuteSlackRequest<SlackFileListResponse>("files.list", new Dictionary<string, string> { { "token", token }, { "ts_to", CalculateTimeSince(days).ToString() }, { "count", "1000" } });



		private static int CalculateTimeSince(uint days) => (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - days * 24 * 60 * 60);



		private async Task<T> ExecuteSlackRequest<T>(string function, Dictionary<string, string> @params) {
			var uri = new Uri($@"https://slack.com/api/{function}");
			var response = await http.PostAsync(uri, new FormUrlEncodedContent(@params));
			var response_body = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(response_body);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		private void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					http.Dispose();
				}
				disposedValue = true;
			}
		}

		public void Dispose() => Dispose(true);

		#endregion
	}
}
