using Microsoft.AspNetCore.Mvc;

namespace Shopscan.Host.Controllers;

// ne pas oublier d'updater égalment BaseController
public abstract class BaseApiController : ControllerBase
{
        protected BaseApiController()
        {
                
        }

#if NOSECURITY
        public Guid SupplierId => Supplier.S2FId; // throw new Exception("Il faut mettre le Guid du supplier ICI");
#else
        public Guid SupplierId => Request.Headers.ContainsKey("") ? Guid.Parse(Request.Headers[""]) : Guid.Empty;
#endif

#if NOSECURITY
        public Guid CustomerId => Guid.Parse(""); // throw new Exception("Il faut mettre le Guid du Customer ICI");
#else
        public Guid CustomerId => Request.Headers.ContainsKey("") ? Guid.Parse(Request.Headers[""]) : Guid.Empty;
#endif


#if NOSECURITY
        //public Guid CustomerAnonymousId => Guid.Parse(""); // throw new Exception("Il faut mettre le Guid du Customer ICI");
#else
    //public Guid CustomerAnonymousId => Request.Headers.ContainsKey(MultiUserStore.AnonymousCustomerId) && Request.Headers[MultiUserStore.AnonymousCustomerId].Count > 0 ?  Guid.Parse(Request.Headers[MultiUserStore.AnonymousCustomerId][0]) : CustomerId;
#endif

#if NOSECURITY
        public string Username => "gregory@corellia.be@"+Supplier.FacozincId.ToString().ToLower();
#else
    public string Username => User?.Identity.Name;
#endif
}