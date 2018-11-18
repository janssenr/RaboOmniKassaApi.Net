using System;

namespace RaboOmniKassaApi.Net.Models.Signing
{
    /// <summary>
    /// Summary description for InvalidSignatureException
    /// </summary>
    public class InvalidSignatureException : Exception
    {
        public InvalidSignatureException() : base("The signature validation of the response failed. Please contact the Rabobank service team.") { }
    }
}