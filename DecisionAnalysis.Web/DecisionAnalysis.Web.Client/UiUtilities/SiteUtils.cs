using FrameworkUtilities.ConfigNames;

namespace DecisionAnalysis.Web.Client.UiUtilities;

public class SiteUtils(IConfiguration configuration) : ISiteUtils
{
    public bool IsWasmLoaded()
    {
        string state = configuration[AppSettingNames.RenderState] ?? "";
        return state == AppSettingNames.WasmRenderState;
    }
}