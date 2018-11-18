using System.Collections.Generic;

namespace RaboOmniKassaApi.Net.Models.Signing
{
    /// <summary>
    /// This interface describes what each signature data provider must provide to be able to calculate the signature.
    /// </summary>
    public interface ISignatureDataProvider
    {
        List<string> GetSignatureData();
    }
}