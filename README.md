# Surface API Mananagement Service

The Surface API Management Service is a collection of APIs that allow for management of your organization's Surface devices.

## Obtaining API access

Permissions to call the Surface API Manangement Service are provided on an as-needed basis. If you are interested in the API offerings, please email surfaceapims@microsoft.com for onboarding details.

Please include the following information in your request:
> * Company name
> * [Tenant id](https://learn.microsoft.com/en-us/entra/fundamentals/how-to-find-tenant)
> * Estimated quantity of Intune-registered Surface devices

## Making API calls

Authentication to the Surface API Management Service is gated by two security checks:
1. Subscription key
    - This must be passed in the request headers as `Ocp-Apim-Subscription-Key`
    - Details on obtaining this key will be provided during the onboarding process noted in the previous section.
2. Access token
    - The token must be generated from an Entra application of the tenant that you provided during onboarding.
    - For instructions on how to create an Entra application and generate a token, see [Create a Microsoft Entra application and service principal that can access resources](https://learn.microsoft.com/entra/identity-platform/howto-create-service-principal-portal).

Please see [SurfaceApiManagementServiceSample](./src/SurfaceApiManagementServiceSample/Program.cs) for sample code that sets these headers and sends a request.

# APIs

## Warranty API

This API provides warranty information for Intune-enrolled Surface devices. Currently, this data is only available in bulk via CSV export.

In order for the data to be available, you must first enroll your tenant for scanning:

> `PUT https://appx-sms-int-apim.azure-api.net/api/external/warranty/enrollment`

Then, twice per month, our services will scan your tenant's devices and a device export will be available export API. Please note that if any devices are added/removed from your tenant or any new warranties are added in between scans, the export data may be out-of-date.

> `GET https://appx-sms-int-apim.azure-api.net/api/external/warranty/export`
> 
> Response: 
> ```
> {
>    "downloadUrl": "url to csv download",
>    "expiresOn": "date/time the downloadUrl will expire on"
> }
> ```

Finally, if you wish to unenroll your tenant, you can do so via:

> `DEL https://appx-sms-int-apim.azure-api.net/api/external/warranty/enrollment`
