# Module 11: Identity Management and Authorization on Azure Active Directory

>Wherever  you see a path to file starting at *[repository root]*, replace it with the absolute path to the directory in which the 20487 repository resides.
> e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487,
then the following path: [repository root]\AllFiles\20487C\Mod06 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487C\Mod06

# Lab: Securing ASP.NET Web API using Azure AD B2C

#### Scenario

The information security team has filed a complaint that the BlueYonder application is widely open to anyone who seeks to obtain information about tickets purchased by customers. This poses a big issue for the company and thus it was decided that the BlueYonder application has to be properly secured.
It was decided to leverage the already existing Microsoft Azure platform to secure the application using cutting edge authorization protocols such as OAuth 2.0 and OpenID Connect. To achieve this kind of security, it was decided to use Azure Active Directory B2C as an identity provider that will host the customer profiles.



#### Objectives

After completing this lab you will be able to:
- Create an Azure AD B2C tenant.
- Link an Azure AD B2C tenant to your primary Microsoft Azure subscription.
- Create Azure AD B2C registered applications.
- Create Azure AD B2C policies.
- Secure ASP.NET Web API applications using Azure AD B2C.
- Consume secured ASP.NET Web API endpoints using a UWP client.


#### Lab Setup
Estimated setup time: 60 minutes.

1. Open Windows Explorer.
2. Go to **[repository root]\AllFiles\20487C\Mod11\Setup**.
3. Run the following command:
   ```batch
   ps createAzureServices.ps1
   ```
4. Follow the on-screen instructions.

### Exercise 1: Creating and configuring an Azure AD B2C tenant

#### Scenario

In this exercise you will create an Azure AD B2C tenant. Then you will link that tenant to your primary Azure subscription. After having the Azure AD B2C tenant in place, you will proceed to create a registered Azure AD B2C application and configure authentication policies.



#### Task 1: Create Azure AD B2C tenant

1. Open a browser and navigate to the Azure portal.
2. Create a new Azure AD B2C Tenant.
3. Link your existing Azure AD B2C Tenant to Azure subscription.

#### Task 2:  Configure an Azure AD B2C tenant

1. Change **Directory** to the new created tenant.
2. Create new **Application** in **Azure AD B2C**.
3. Create new policy in the **Sign-up or sign-in policies** tab.
4. Add new **Scope** with the following information: 
    - Under **SCOPE**, enter value **read**.
    - Under **DESCRIPTION**, enter value **read**.
5. Add the new scope **API access**.

#### Task 3:  Configure social logins using Azure AD B2C

1. Open a browser and navigate to the **https://apps.dev.microsoft.com**.
2. Create new app.
3. Generate New Password for your new application.
4. Add new **Web** platform.
    >**Note :** In **Redirect URLs**, type ``https://login.microsoftonline.com/te/[YourTenantName].onmicrosoft.com/oauth2/authresp`` (Replace YourTenantName with the tenant name).
5. On **Azure Protal** Create new **Identity provider**, with **Microsoft Account** provider.
6. Attach your new provider with the policy that was created in the previews task.
  
   >**Results**: After completing this exercise, you will have the required Azure AD B2C infrastructure in place, configured it and made it ready for integration with BlueYonder server and client applications.

### Exercise 2: Integrating the BlueYonder server and client applications with Azure AD B2C

#### Scenario

In this exercise you will configure the BlueYonder Companion Web API and UWP applications to work with the Azure AD B2C tenant created in the previous exercise. The applications will use the Azure AD B2C tenant as their identity provider and let the user authenticate against the Azure AD B2C tenant.

#### Task 1:  Integrating the BlueYonder server

1. Open the **BlueYonder.Server.sln** solution from the **D:\AllFiles\Mod11\LabFiles\begin\BlueYonder.Companion** folder.
2. Open the **Web.config** file from the **BlueYonder.Companion.Host** project, and Add the following code in the **\<appSettings\>**:
    ```xml
        <add key="ida:ClientId" value="[Application id]" />  
        <add key="ida:Tenant" value="[Your Tenant Domain Name]" />
        <add key="ida:SignUpSignInPolicyId" value="[SignIn-Policy]" />
        <add key="ida:AadInstance" value="https://login.microsoftonline.com/{0}/v2.0/.well-known/openid-configuration?p={1}" />
    ```
3. Replace the **Values** with the missing information.
4. In the **BlueYonder.Companion.Host** open **Startup.Auth.cs**.
5. Add the following code:
    ```cs
        // These values are pulled from web.config
        public static string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        public static string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        public static string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string SignUpSignInPolicy = ConfigurationManager.AppSettings["ida:SignUpSignInPolicyId"];
        public static string DefaultPolicy = SignUpSignInPolicy;
    ```
6. Locate **ConfigureAuth** method and paste the following code:
    ```cs
         TokenValidationParameters tvps = new TokenValidationParameters
            {
                // Accept only those tokens where the audience of the token is equal to the client ID of this app
                ValidAudience = ClientId,
                AuthenticationType = Startup.DefaultPolicy
            };

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                // This SecurityTokenProvider fetches the Azure AD B2C metadata & signing keys from the OpenIDConnect metadata endpoint
                AccessTokenFormat = new JwtFormat(tvps, new OpenIdConnectCachingSecurityTokenProvider(String.Format(AadInstance, Tenant, DefaultPolicy)))
            });
    ```
7. In the **BlueYonder.Companion.Controllers** project, open the **LocationsController.cs** file, and add **Authorizetion** attribute to the class.
8. **Publish** the **BlueYonder.Companion.Host** to azure.
9. In the browser check on the site url + **/Locations** that the response is:
    ```xml
        <Error>
            <Message>Authorization has been denied for this request. </Message>
        </Error>
    ```
#### Task 2: Integrating the BlueYonder native client

1. Open the **BlueYonder.Server.sln** solution from the **D:\AllFiles\Mod11\LabFiles\begin\BlueYonder.Companion.Client** folder.
2. In the **BlueYonder.Companion.Client** project, open **App.xaml.cs** , and then locate the following comment. 

     **// TODO: Lab 11, Exercise 2: Task 2.2: Configure Azure B2C sttings.**

     - Replace the properties under the comment.
  
3. Replace the address used to communicate with the server:

    a. In the **BlueYonder.Companion.Shared** project, open the **Addresses** class and in the **BaseUri** property, replace the address of the emulator with the cloud service address you created earlier.  
    
4. Run the application.
5. Login to the app with the **Microsoft** social provider.
6. Search for a flight, and verify that get the list of the flights from the server.


   >**Results**: You will have integrated the BlueYonder server and client applications with Azure AD B2C and are able to create users from the UWP client, log in using the created users and edit their profiles and be able to reset their passwords.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
