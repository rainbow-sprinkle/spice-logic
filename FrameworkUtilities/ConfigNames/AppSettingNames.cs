namespace FrameworkUtilities.ConfigNames;

public static class AppSettingNames
{
    public const string SqlConnectionString = "DefaultConnection";
    public const string RenderState = "RenderState";
    public const string WasmRenderState = "wasm";
    public const string EnableEmailSending = nameof(EnableEmailSending);
    public const string AzureStorageConnectionString = "SpicyVisionAzureStorage:ConnectionString";
    public const string AzureStorageUrl = "SpicyVisionAzureStorage:StorageUrl";
    public const string SQlConnectionString = "SpicyVisionWebDbConnection";

    public const string MaxThumbWidth = "MaxThumbWidth";
    public const string MaxThumbHeight = "MaxThumbHeight";
    public const string MaxUltraThumbWidth = "MaxUltraThumbWidth";
    public const string MaxUltraThumbHeight = "MaxUltraThumbHeight";
    public const string MaxRegularFullImageWidth = "MaxRegularFullImageWidth";
    public const string MaxRegularFullImageHeight = "MaxRegularFullImageHeight";

    public const string MasterWorkflowId = "MasterWorkflowId";
    public const string SyncFusionBlazorLicenseKeyName = "SyncFusionBlazorLicenseKey";

    public const string CarLiterPerKilometer = "CarLiterPerKilometer";
    public const string FuelCostPerLiter = "FuelCostPerLiter";

    
}