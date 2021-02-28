namespace HR_Project
{
    //Necessary to add Claim with UserRole from HR-Project-Database to User.Claims fetched from Azure AD B2C
    //https://github.com/Azure-Samples/active-directory-b2c-dotnetcore-webapp/blob/master/WebApp-OpenIDConnect-DotNet/AzureAdB2COptions.cs
    public class AzureAdB2COptions
    {
        public const string PolicyAuthenticationProperty = "Policy";

        public AzureAdB2COptions()
        {
            AzureAdB2CInstance = "<AzureAdB2C INSTANCE URL>";
        }

        public string ClientId { get; set; }
        public string AzureAdB2CInstance { get; set; }
        public string Tenant { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string SignInPolicyId { get; set; }
        public string SignUpPolicyId { get; set; }
        public string ResetPasswordPolicyId { get; set; }
        public string EditProfilePolicyId { get; set; }
        public string RedirectUri { get; set; }

        public string DefaultPolicy => SignUpSignInPolicyId;
        public string Authority => $"{AzureAdB2CInstance}/{Tenant}/{DefaultPolicy}/v2.0";

        public string ClientSecret { get; set; }
        public string ApiUrl { get; set; }
        public string ApiScopes { get; set; }
    }
}