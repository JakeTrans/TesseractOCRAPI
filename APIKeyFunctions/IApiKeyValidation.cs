namespace TesseractOCRAPI.APIKeyFunctions
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}