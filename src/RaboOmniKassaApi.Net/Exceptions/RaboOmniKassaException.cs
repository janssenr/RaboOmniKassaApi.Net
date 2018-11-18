using System;

namespace RaboOmniKassaApi.Net.Exceptions
{
    public class RaboOmniKassaException : Exception
    {
        public RaboOmniKassaException(string message) : base(message) { }
    }
}
