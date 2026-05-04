using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/config")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _config;
    public ConfigController(IConfiguration config) => _config = config;

    [HttpGet("msal")]
    public IActionResult GetMsalConfig()
    {
        var msalConfig = new
        {
            clientId = _config["AzureAd:ClientId"],
            authority = $"https://login.microsoftonline.com/{_config["AzureAd:TenantId"]}",
            redirectUri = "http://localhost:5000/signin-oidc", // This should match the redirect URI registered in Azure AD for your application
        };


        return Ok(msalConfig);
    }
}