# TesseractOCRAPI

This is a .Net 7 API Implementation of TesseractOCRPlugin which can be deployed to accept files - OCR them and return the text it finds.  

Please note this will currently only work on Windows nodes and Azure App services (tested locally and on Azure) due the System.Drawing.Common not being cross-Compatible  

includes Swagger Documentation as part of the deployment for testing as only one function.

Currently this requires no authorisation process as such please be aware if hosting on azure etc, as anyone with the URL can use it.
