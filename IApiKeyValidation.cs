﻿namespace TesseractOCRAPI
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}