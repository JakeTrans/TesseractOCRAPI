using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace TesseractOCRAPI.APIKeyFunctions
{
    public static class GetAPIKey
    {
        public static string Getkey()
        {
            SecretClient client = new SecretClient(new Uri("<VaultUrl>"), new DefaultAzureCredential());

            // Replace 'secretName' with your actual secret name
            string secretName = "<secretName>";
            Azure.Response<KeyVaultSecret> secret = client.GetSecret(secretName);
            // Access the secret value

            return secret.Value.Value;
        }
    }
}