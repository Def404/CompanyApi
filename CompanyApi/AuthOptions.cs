using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CompanyApi;

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient"; 
    const string KEY = "FDsgsdDGD3532_safdas2142d_dsa";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}