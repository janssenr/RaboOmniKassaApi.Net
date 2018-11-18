using System;
using System.Web.Configuration;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

public partial class Return : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var orderId = Request.QueryString["order_id"];
        var status = Request.QueryString["status"];
        var signature = Request.QueryString["signature"];
        var signingKey = new SigningKey(Convert.FromBase64String(WebConfigurationManager.AppSettings["SigningKey"]));
        var paymentCompletedResponse = PaymentCompletedResponse.CreateInstance(orderId, status, signature, signingKey);
        if (paymentCompletedResponse == null)
        {
            throw new Exception("The payment completed response was invalid.");
        }

        // Use these variables instead of using the URL parameters ($orderId and $status). Input validation has been performed on these values.
        var validatedMerchantOrderId = paymentCompletedResponse.OrderId;
        var validatedStatus = paymentCompletedResponse.Status;
        // ... complete payment
    }
}