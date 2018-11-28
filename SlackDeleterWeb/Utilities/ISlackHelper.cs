using System;
using System.Threading.Tasks;
using SlackDeleterWeb.Model;

namespace SlackDeleterWeb.Utilities {
	public interface ISlackHelper : IDisposable {

		Task<SlackFileListResponse> RetrieveFilesOlderThan(uint days, string token);
		Task<SlackFileDeleteResponse> DeleteFile(string fileId, string token);

	}
}
