using Microsoft.JSInterop;

namespace DecisionAnalysis.Web.Client.UiUtilities;

public class BlazorJsFacade(IJSRuntime jSRuntime) : IBlazorJsFacade
{
    public async Task<bool> ShowConfirm(string message)
    {
        return await jSRuntime.InvokeAsync<bool>("window.confirm", message);
    }

    public async Task ShowAlert(string message)
    {
        await jSRuntime.InvokeVoidAsync("window.alert", message);
    }

    public async Task<string> ShowPrompt(string question, string defaultAnswer = "")
    {
        return await jSRuntime.InvokeAsync<string>("window.prompt", question, defaultAnswer);
    }

    public async Task OpenWindow(string url)
    {
        await jSRuntime.InvokeVoidAsync("window.open", url);
    }

    public async Task NavigateToUrl(string url)
    {
        await jSRuntime.InvokeVoidAsync("window.location.assign", url);
    }

    public async Task OpenPopup(string url, bool showScrollBar, bool isResizable, int width, int height)
    {
        await jSRuntime.InvokeVoidAsync("window.open", url, "_blank", $"toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars={BoolToYesNo(showScrollBar)},resizable={BoolToYesNo(isResizable)},width={width},height={height}");
    }

    public async Task ReloadPage(bool refreshFromServer)
    {
        await jSRuntime.InvokeVoidAsync("window.location.reload", refreshFromServer ? "true" : "false");
    }

    private static string BoolToYesNo(bool value)
    {
        return value ? "yes" : "no";
    }
}