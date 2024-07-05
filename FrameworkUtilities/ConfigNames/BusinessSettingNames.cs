namespace FrameworkUtilities.ConfigNames;

public static class BusinessSettingNames
{
    private const string SectionName = "BusinessSettings";
    public static readonly string CompanyName = $"{SectionName}:{nameof(CompanyName)}";
    public static readonly string BusinessDomainUrl = $"{SectionName}:{nameof(BusinessDomainUrl)}";
    public static readonly string SupportPhoneNumber = $"{SectionName}:{nameof(SupportPhoneNumber)}";
    public static readonly string BusinessEmailAddress = $"{SectionName}:{nameof(BusinessEmailAddress)}";

    public static class BusinessAddress
    {
        private static readonly string BusinessAddressSection = $"{SectionName}:{nameof(BusinessAddress)}";

        public static readonly string StreetAddress = $"{BusinessAddressSection}:{nameof(StreetAddress)}";
        public static readonly string City = $"{BusinessAddressSection}:{nameof(City)}";
        public static readonly string Province = $"{BusinessAddressSection}:{nameof(Province)}";
        public static readonly string PostalCode = $"{BusinessAddressSection}:{nameof(PostalCode)}";
        public static readonly string Country = $"{BusinessAddressSection}:{nameof(Country)}";
    }
}