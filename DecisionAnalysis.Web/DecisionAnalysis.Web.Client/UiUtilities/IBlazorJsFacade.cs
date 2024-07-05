namespace DecisionAnalysis.Web.Client.UiUtilities;

public interface IBlazorJsFacade
{
    Task<bool> ShowConfirm(string message);
    Task ShowAlert(string message);
    Task<string> ShowPrompt(string question, string defaultAnswer = "");
    Task OpenWindow(string url);
    Task OpenPopup(string url, bool showScrollBar, bool isResizable, int width, int height);
    Task ReloadPage(bool refreshFromServer);

    Task NavigateToUrl(string url);
}