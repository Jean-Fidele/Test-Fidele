using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Test_Fidele.Systems;

public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        var ns = controller.ControllerType.Namespace;
        controller.ApiExplorer.GroupName = ns != null && ns.Contains("Backoffice") ? "backoffice" : ns.Contains("Fatimia") ? "fatimia" : "v1";
    }
}