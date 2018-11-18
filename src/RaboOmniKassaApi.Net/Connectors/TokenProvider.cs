using System;
using RaboOmniKassaApi.Net.Models;

namespace RaboOmniKassaApi.Net.Connectors
{
    /// <summary>
    /// Summary description for TokenProvider
    /// </summary>
    public abstract class TokenProvider
    {
        public const string RefreshToken = "REFRESH_TOKEN";
        public const string AccessToken = "ACCESS_TOKEN";
        public const string AccessTokenValidUntil = "ACCESS_TOKEN_VALID_UNTIL";
        public const string AccessTokenDuration = "ACCESS_TOKEN_DURATION";

        public string GetRefreshToken()
        {
            return GetValue(RefreshToken);
        }

        public AccessToken GetAccessToken()
        {
            var token = GetValue(AccessToken);
            var validUntil = GetValue(AccessTokenValidUntil);
            var durationInMilliseconds = GetValue(AccessTokenDuration);
            return new AccessToken
            {
                Token = token,
                ValidUntil = DateTime.Parse(validUntil),
                DurationInMilliseconds = int.Parse(durationInMilliseconds)
            };
        }

        public void SetAccessToken(AccessToken accessToken)
        {
            SetValue(AccessToken, accessToken.Token);
            SetValue(AccessTokenValidUntil, accessToken.ValidUntil.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
            SetValue(AccessTokenDuration, accessToken.DurationInMilliseconds.ToString());
            Flush();
        }

        public abstract string GetValue(string key);

        public abstract void SetValue(string key, string value);

        public abstract void Flush();
    }
}