using System;
using System.IO;
using System.Web.Configuration;
using RaboOmniKassaApi.Net;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

public partial class Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string jsonData = new StreamReader(Request.InputStream).ReadToEnd();
        if (string.IsNullOrWhiteSpace(jsonData))
        {
            throw new Exception("Invalid notification call.");
        }

        var refreshToken = WebConfigurationManager.AppSettings["RefreshToken"];
        var signingKey = WebConfigurationManager.AppSettings["SigningKey"];
        var testMode = bool.Parse(WebConfigurationManager.AppSettings["TestMode"]);
        var client = new OmniKassaApiClient(refreshToken, signingKey, testMode);

        var announcementResponse = RaboOmniKassaApi.Net.Models.Response.Response.CreateInstance<AnnouncementResponse>(jsonData, new SigningKey(Convert.FromBase64String(signingKey)));

        bool moreResultsAvailable;
        do
        {
            var response = client.RetrieveAnnouncement(announcementResponse);

            //... Update the order statuses	

            moreResultsAvailable = response.MoreOrderResultsAvailable;

        } while (moreResultsAvailable);
    }
}