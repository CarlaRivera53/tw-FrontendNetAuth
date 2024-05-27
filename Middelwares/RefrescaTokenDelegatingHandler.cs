
using System.Security.Claims;
using frontendnet.Services;

namespace frontendnet.Middelwares;

public class  RefrescaTokenDelegatingHandler(AuthClientService auth, IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var response = await base.SendAsync (request, cancellationToken);
      response.EnsureSuccessStatusCode ();
      //revisa si el servidor nos envio un nuevo token 
      if (response.Headers.Contains ("Set-Authorization"))
      {
        string jwt = response.Headers.GetValues("Set-Authorization").FirstOrDefault()!;
        var claims = new List<Claim>
        {

        
        //todo esto se guarda en la ccokie
        new(ClaimTypes.Name, httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name)!),
        new(ClaimTypes.GivenName, httpContextAccessor.HttpContext?.User.FindFirstValue (ClaimTypes.GivenName)!),

      };
      auth.IniciaSesionAsync(claims);

    }
    return response;
    }

}