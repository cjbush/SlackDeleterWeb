using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SlackDeleterWeb.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class SlackDeleterController : ControllerBase {
		[HttpGet("{token}")]
		public async Task Get(string token) {
			using (var client = new HttpClient()) {
				async Task<dynamic> list_files() {
					var ts_to = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - 30 * 24 * 60 * 60);
					var @params = new Dictionary<string, string> {
						{ "token", token },
						{ "ts_to", ts_to.ToString() },
						{ "count", "1000" }
					};
					var uri = new Uri($@"https://slack.com/api/files.list");
					var response = await client.PostAsync(uri, new FormUrlEncodedContent(@params));
					var response_body = await response.Content.ReadAsStringAsync();
					dynamic result = JsonConvert.DeserializeObject(response_body);
					var resultFiles = result.files;
					return resultFiles;
				}

				async Task delete_files(IEnumerable<dynamic> files_to_delete) {
					using (var semaphore = new SemaphoreSlim(10)) {
						var tasks = new List<Task>();
						foreach (var file in files_to_delete) {
							await semaphore.WaitAsync();
							var task = Task.Run(async () => {
								try {
									var file_id = file.id.Value;
									var @params = new Dictionary<string, string> {
										{ "token", token },
										{ "file", file_id }
									};
									var uri = new Uri("https://slack.com/api/files.delete");
									var response = await client.PostAsync(uri, new FormUrlEncodedContent(@params));
									var response_body = await response.Content.ReadAsStringAsync();
									Console.WriteLine($@"{file_id}: {((dynamic)JsonConvert.DeserializeObject(response_body)).ok.Value}");
									await Task.Delay(100); //Get past Slack API request throttling
								} finally {
									semaphore.Release();
								}
							});
							tasks.Add(task);
						}

						await Task.WhenAll(tasks);
					}
				}

				Console.WriteLine("Deleting files...");
				var files = await list_files();
				await delete_files(files);
				Console.WriteLine("Done!");
			}
		}
	}
}