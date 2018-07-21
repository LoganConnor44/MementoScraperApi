using System.Collections.Generic;

namespace MementoScraperApi.Credentials {
    public interface ICredentials {
		Dictionary<string, string> Credentials {
			get;
		}
    }
}