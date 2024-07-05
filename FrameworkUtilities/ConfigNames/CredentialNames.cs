namespace FrameworkUtilities.ConfigNames;

public static class CredentialNames
{
    private const string CredentialSection = "Credentials";

    public static readonly string EmailServiceApiToken = $"{CredentialSection}:{nameof(EmailServiceApiToken)}";
}