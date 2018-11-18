namespace RaboOmniKassaApi.Net.Connectors.Http
{
    /// <summary>
    /// Summary description for IRestTemplate
    /// </summary>
    public interface IRestTemplate
    {
        void SetToken(string token);
        string Get(string path, string[] parameters = null);
        string Post(string path, string body = null);
    }
}