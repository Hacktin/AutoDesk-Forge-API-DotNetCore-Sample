using System;
using System.Threading.Tasks;
using Autodesk.Forge;
using forgeSampleAPI_DotNetCore.Core.Configurations;
using forgeSampleAPI_DotNetCore.Services.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Concerete
{
    public class AutoDeskAuthService:IAuthService
    {
        private static dynamic InternalToken { get; set; }
        private static dynamic PublicToken { get; set; }

        public dynamic GetPrimaryToken()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetPrimaryTokenTask()
        {
            return await TokenGeneratorTask(PublicToken, new Scope[] {Scope.ViewablesRead});
        }

        public dynamic GetSecondaryToken()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetSecondaryTokenTask()
        {
            return await TokenGeneratorTask(InternalToken, 
                new Scope[] { Scope.BucketCreate, Scope.BucketRead, Scope.BucketDelete, Scope.DataRead, Scope.DataWrite, Scope.DataCreate, Scope.CodeAll });
        }


        #region  privateMethods

        private  async Task<dynamic> TokenGeneratorTask(dynamic Token,Scope[] scopes)
        {
            if (Token == null || Token.ExpiresAt < DateTime.UtcNow)
            {
                Token = await Get2LeggedTokenAsync(scopes);
                Token.ExpiresAt = DateTime.UtcNow.AddSeconds(Token.expires_in);
            }

            return Token;
        }

        private async Task<dynamic> Get2LeggedTokenAsync(Scope[] scopes)
        {
            TwoLeggedApi oauth = new TwoLeggedApi();
            string grantType = "client_credentials";
            dynamic bearer = await oauth.AuthenticateAsync(
                AppSettings.GetAppSetting("FORGE_CLIENT_ID"),
                AppSettings.GetAppSetting("FORGE_CLIENT_SECRET"),
                grantType,
                scopes);
            return bearer;
        }

        #endregion



    }
}
