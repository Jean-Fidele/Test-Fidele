using Microsoft.AspNetCore.Mvc;

namespace Test_Fidele.Controllers;

public abstract class BaseApiController : ControllerBase
{
    protected BaseApiController()
    {
        
    }

    public string Matricule => Request.Headers.ContainsKey("matricule") ? Request.Headers["matricule"] : "10000";

    public string Username => Request.Headers.ContainsKey("username") ? Request.Headers["username"] : "R.Denis";

}