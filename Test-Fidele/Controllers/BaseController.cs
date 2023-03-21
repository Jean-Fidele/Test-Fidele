using System;
#if NOSECURITY
using Shopscan.Host.Authorizations;
#endif
using Microsoft.AspNetCore.Mvc;

namespace Shopscan.Host.Controllers;

// ne pas oublier d'updater égalment BaseApiController
public abstract class BaseController : Controller
{
        public BaseController()
        {
           
        }

#if NOSECURITY
        public Guid SupplierId => Supplier.S2FId; // throw new Exception("Il faut mettre le Guid du supplier ICI");
#else
        public Guid SupplierId => Request.Headers.ContainsKey("") && Request.Headers[""].Count > 0 ? Guid.Parse(Request.Headers[""]) : Guid.Empty;

#endif

#if NOSECURITY
        //public Guid AnonymousCustomerId => Guid.Parse(""); // throw new Exception("Il faut mettre le Guid du Customer ICI");
#else
        //public Guid CustomerAnonymousId => Request.Headers.ContainsKey(MultiUserStore.AnonymousCustomerId) ? Guid.Parse(Request.Headers[MultiUserStore.AnonymousCustomerId][0]) : CustomerId;
#endif

#if NOSECURITY
        public Guid CustomerId => Guid.Parse(""); // throw new Exception("Il faut mettre le Guid du Customer ICI");
#else
        public Guid CustomerId => Request.Headers.ContainsKey("") ? Guid.Parse(Request.Headers[""]) : Guid.Empty;
#endif
}