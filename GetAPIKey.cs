using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace TesseractOCRAPI
{
    public static class GetAPIKey
    {
        public static string Getkey()
        {
            SecretClient client = new SecretClient(new Uri("<Vault URL>"), new DefaultAzureCredential());

            // Replace 'secretName' with your actual secret name
            string secretName = "<SecretName>>";
            Azure.Response<KeyVaultSecret> secret = client.GetSecret(secretName);
            // Access the secret value

            return secret.Value.Value;
        }
    }
}