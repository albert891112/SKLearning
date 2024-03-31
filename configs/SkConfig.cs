namespace Configs
{
    public class SkConfig
    {
        private string _endpoint;
        public string endpoint => _endpoint;
        private string _apiKey;
        public string apiKey => _apiKey;

        private HttpClient? _httpClient;
        public HttpClient? httpClient => _httpClient;

        private string _deploymentName;
        public string deploymentName => _deploymentName;

        private string? _modelId;
        public string? modelId => _modelId;

        public SkConfig(string endpoint, string apiKey, string deploymentName,HttpClient? httpClient = null ,  string? modelId = null)
        {
            _endpoint = endpoint;
            _apiKey = apiKey;
            _httpClient = httpClient;
            _deploymentName = deploymentName;
            _modelId = modelId;
        }
    }

}


