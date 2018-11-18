using RaboOmniKassaApi.Net.Models.Request;
using RaboOmniKassaApi.Net.Models.Response;

namespace RaboOmniKassaApi.Net.Connectors
{
    /// <summary>
    /// Summary description for IConnector
    /// </summary>
    public interface IConnector
    {
        string AnnounceMerchantOrder(MerchantOrderRequest order);
        string GetAnnouncementData(AnnouncementResponse announcement);
    }
}