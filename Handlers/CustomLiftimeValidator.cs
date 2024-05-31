using Microsoft.IdentityModel.Tokens;

namespace HandCrafter.Handlers
{
    public class CustomLiftime
    {
        static public bool CastomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if(expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }
}
