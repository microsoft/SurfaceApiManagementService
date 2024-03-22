# Surface API Mananagement Service

The Surface API Management Service is a collection of APIs that allow for management of your organization's Surface devices.

## Obtaining API access

Permissions to call the Surface API Manangement Service are provided on an as-needed basis. If you are interested in the API offerings, please email surfaceapims@microsoft.com for onboarding details.

Please include the following information in your request:
> * Company name
> * Tenant id[^1]
> * Tenant primary domain[^1] (ex. contoso.onmicrosoft.com)
> * Application (client) id[^2]
> * Estimated quantity of Intune-registered Surface devices

[Image of sample ticket]

[^1]: See [Find the Microsoft Entra tenant ID and primary domain name](https://learn.microsoft.com/en-us/partner-center/find-ids-and-domain-names#find-the-microsoft-entra-tenant-id-and-primary-domain-name).
[^2]: See [Create a Microsoft Entra application and service principal that can access resources](https://learn.microsoft.com/entra/identity-platform/howto-create-service-principal-portal)

## Developer portal sign in

Once you have been added to the access list, you will receive a link to the developer portal to subscribe to the available APIs.

[Image of sign in screen]

1. Sign in using the approved Tenant using Azure Active Directory.
Note: You must sign in using the admin profile for the account.
2. Complete the Sign up request details
   * If you recieve this error that account used for logging in has not been added to the access list.

## Subscribing to a Product

Once you have logged in, you are able to view the APIs available within a Product. To access and use these APIs, you will need to subscribe to the Product associated to the API.

[Image of product page]

1. Select the Product menu to view the products and APIs associated to it.
2. Once you have identified the product you wish to subscribe to, enter a name in the Subscription text box and hit Subscribe.
3. This will generate subscription keys available on the Profile screen.

[Image of profile screen]

## Making API calls

Authentication to the Surface API Management Service is gated by two security checks:
1. Subscription key
    - This must be passed in the request headers as `Ocp-Apim-Subscription-Key`
    - Details on obtaining this key will be provided during the onboarding process noted in the previous section.
2. Access token
    - The token must be generated from an Entra application of the tenant that you provided during onboarding.
    - For instructions on how to create an Entra application and generate a token, see [Create a Microsoft Entra application and service principal that can access resources](https://learn.microsoft.com/entra/identity-platform/howto-create-service-principal-portal).

Please see [SurfaceApiManagementServiceSample](./src/SurfaceApiManagementServiceSample/Program.cs) for sample code that sets these headers and sends a request.

## Reports

Once you are actively using APIs you will be able to see reporting metrics and usage data on the reports page.

[Image]

# APIs

## Warranty API

This API provides warranty information for Intune-enrolled Surface devices. Currently, this data is only available in bulk via CSV export.

This API will only provide information for devices registered to the Intune tenant configured during onboarding. If you have devices across multiple tenants, you will need to register each tenant to the Surface API Management Service.

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
