# Module 11: Identity Management and Authorization on Azure Active Directory

Wherever  you see a path to a file that starts with *[repository root]*, replace it with the absolute path to the folder in which the 20487 repository resides. For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, the following path: **[repository root]\AllFiles\20487C\Mod06** should be changed to **C:\Users\John Doe\Downloads\20487\AllFiles\20487C\Mod11**.

# Lesson 1: Claims-based Identity Concepts

### Demonstration: Using claims in an ASP.NET MVC application

#### Demonstration Steps

1. Open a browser. 
2. In the address field, enter https://portal.azure.com and press enter.
3. In the **Azure Portal**, click on **Create a resource**.
4. In the **New** blade, Click **Web + Mobile** and then click **Web App**.
5. In the **App Name**, enter **claims-example-yourinitials** (replace yourinitials with your initials, e.g. – John Doe  j.d.)
6. Click **Create** and wait for the creation process to finish.
7. Go to the **Authentication/Authorization** blade.
8. In the **Authentication/Authorization** blade enable **App Service Authentication**.
9. In the **Action to take when request is not authenticated** dropdown select **Log in with Azure Active Directory**.
10. Under **Authentication Providers** click **Azure Active Directory**.
11.	In the **Azure Active Directory Settings**, change **Management Mode** to **Express**.
12.	Select **Create New AD App** and click **OK**, the blade should close.
13.	Back in the **Authentication/Authorization** blade, click **Save**.
14.	Click on the **Overview** blade and then click again on the **Authentication/Authorization** blade.
    >**Note**: Due to a bug in the Azure portal, you need to go out of the Authentication/Authorization blade and enter it again in order for the portal to recognize the new authentication settings.
15.	In the **Authentication/Authorization** blade, click **Azure Active Directory**.
16.	In the **Azure Active Directory Settings**, click **Azure AD App**.
17.	In the **Azure AD Applications** blade, copy the value under the **CLIENT** ID column.
18.	Open **Visual Studio 2017**.
19.	Click **File**, point to **New** and click **Project**.
20.	In the **New Project** modal, expand **Installed** and then expand **Visual C#**.
21.	Click **Web** and then select the **ASP.NET Web Application (.NET Framework)** template.
22.	In the **Name** field, enter **ClaimsExampleApp**.
23.	Click **OK**.
24.	In the **New ASP.NET Web Application** modal, select the **MVC** template.
25.	Click **Change Authentication**.
26.	In the **Change Authentication** dialog, select **Work or School accounts**.
27.	Expand **More Options**.
28.	Check **Overwrite the application entry if one with the same ID exists**.
29.	Paste the value you copied in step 17 into the **Client ID** field.
30.	Click **OK**.
31.	In the **New ASP.NET Web Application** click **OK**.
32.	In the **Solution Explorer**, expand the **App_Start** folder and open the **Startup.Auth.cs** file.
33.	Add the following Using directives:
    ```cs
        using System.Threading.Tasks;
        using Microsoft.Owin.Security.Notifications;
        using Microsoft.IdentityModel.Protocols;
    ```
34.	Under **PostLogoutRedirectUri**, add the following piece of code:
    ```cs
        Notifications = new OpenIdConnectAuthenticationNotifications
        {
            SecurityTokenValidated = OnTokenValidated
        }
    ```
35.	Add the following code after the end of the **ConfigureAuth** method:
    ```cs
        private async Task OnTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context) => await Task.FromResult(0);
    ```
36.	Put a breakpoint on the **OnTokenValidated** method that you’ve just added.
37.	Press F5 to debug the application.
38.	The browser should be redirected to an address starting with https://login.microsoftonline.com and a page with **Accept** and **Cancel** buttons should appear.
39.	Click **Accept**.
40.	The application should break.
41.	Inspect the **context** object, expand the **AuthenticationTicket** and inspect the **Identity** member.
42.	Expand **Identity**, and then expand **Claims**, inside expand the **Results View** to see the list of claims.
43.	Review the list of claims and note some familiar claims, like your name and the email address you used to sign up to Microsoft Azure.
    >**Note**: The list of claims you saw in step 43 is provided by Microsoft Azure. In the next lessons you will be introduced to Azure Active Directory, the identity provider used in this demonstration. 

# Lesson 2: Introduction to Azure Active Directory

### Demonstration: Exploring Azure AD

#### Preparation Steps

  You need to have two available emails, one for the azure portal and the second for creating new user.

#### Demonstration Steps

1. Open **Microsoft Edge**.
2. Go to the Azure portal at **http://portal.azure.com**.
3. If a page appears, prompting for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials.

4. On the navigation menu on the left, click **Azure Active Directory**.
5. On the **Azure Active Directory** blade, click **Custom domain names**, and copy the **NAME**.
6. Go back to **Azure Active Directory** view, click **Users** under **Manage** subtitle.
7. Click **New user**, on the top menu.
    1. **Name**, enter full name.
    2. **User name**, enter name + **@[Paste Domain name]** .
    3. Click **Create**.
8. Click **New guest user**, on the top menu.
9. Enter email address in the textbox and click invite.
10. Go to the email and accept the invitation.
11. Now in the portal go back to **Azure Active Directory**.
12. On the **Azure Active Directory** blade, click **Groups** under **Manage** subtitle.
13. Click **New group**, on the top menu.
    - **Group type**, select **security**.
    -  **Group name**, enter **Mod11Group**.
    - **Membership type**, select **Assigned**.
    - Click on **Members** and select the guest user, click **Select**.
    - Click **Create**.
14. Go back to **Groups**, see that the **Group** was created.      


### Demonstration: Using Azure AD to secure ASP.NET Web Applications.

#### Demonstration Steps

1. Open **Visual Studio 2017**.
2. On the **File** menu, point to **New**, and then click **Project**.
3. In the **New Project** dialog box, on the navigation pane, expand the **Installed** node, expand the **Visual C#** node and then click the **Web** node.
4. Select **ASP.NET Web Application (.NET Framework)** from the list of templates.
5. In the **Name** text box, type **AzureADWebApp**.
6. In the **Location** text box, type **[repository root]\Allfiles\20487C\Mod11\Democode\AzureADWebApp\Begin**, and then click **OK**.
7. In the **New ASP.NET Web Application - AzureADWebApp** dialog box, click **MVC**, and then click **OK**.
8. In **Solution Explorer**, under the **MyApp** project, expand the **App\_Start** folder, and then double-click **WebApiConfig.cs**.
9. Right click the **AzureADWebApp** project, and then click **Manage NuGet Packages**.
    1. In the **NuGet: AzureADWebApp** window, click **Browse**.
    2. In the search box on the top left of the window, enter **Microsoft.Owin.Host.SystemWeb** and press **Enter**.
    3. In the results select **Microsoft.Owin.Host.SystemWeb** and click **Install**.
    4. If a **Preview Changes** modal appears, click **OK**.
    5. In the **License Acceptance** modal, click **I Accept**.
    6. In the search box on the top left of the window, enter **Microsoft.Owin.Security.Cookies** and press **Enter**.
    7. In the results select **Microsoft.Owin.Security.Cookies** and click **Install**.
    8. If a **Preview Changes** modal appears, click **OK**.
    9. In the **License Acceptance** modal, click **I Accept**.
    10. In the search box on the top left of the window, enter **Microsoft.Owin.Security.OpenIdConnect** and press **Enter**.
    11. In the results select **Microsoft.Owin.Security.OpenIdConnect** and click **Install**.
    12. If a **Preview Changes** modal appears, click **OK**.
    13. In the **License Acceptance** modal, click **I Accept**.
    14. Wait until the package is completely downloaded and installed. To close the **NuGet Package Manager: AzureADWebApp** dialog box, click **Close**.
10. Right click the **AzureADWebApp** project, and then point to **Add** and select **Class**.
11. In the **Add New Item- AzureADWebApp** dialog box, in the **Name** box, type **Startup.Auth.cs**, and then click **Add**.
12. Add the following **using** directives at the beginning of the class.

    ```cs
        using Owin;
        using Microsoft.Owin.Security;
        using Microsoft.Owin.Security.Cookies;
        using Microsoft.Owin.Security.OpenIdConnect;
    ```
13. Replace the class declaration with the following code.

    ```cs
        public partial class Startup
    ```    
14. Add the following properties to the class.

    ```cs
        private static string clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["AADInstance"];
        private static string tenantId = ConfigurationManager.AppSettings["TenantId"];
        private static string authority = aadInstance + tenantId;
     ```
15. Add the following method to the class.

    ```cs
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority
                });
        }
    ```

16. Drag the current class to folder **App_Start**.
17. Right click the **AzureADWebApp** project, and then point to **Add** and select **Class**.
18. In the **Add New Item- AzureADWebApp** dialog box, in the **Name** box, type **Startup.cs**, and then click **Add**. 
19. Add the following **using** directives at the beginning of the class.

    ```cs
        using Owin;
    ```
20. Replace the class declaration with the following code.

    ```cs
        public partial class Startup
    ```    
21. Add the following method to the class.

    ```cs
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    ```

22. Expand **Controller** folder, select **HomeController.cs**.
23. Add a **[Authorize]** attribute to the **HomeController** class.
24. Add the following **using** directives at the beginning of the class.

    ```cs
        using System.Security.Claims;
    ```
25. Replace the **Contact** action content with the following code.

    ```cs
        string userfirstName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.GivenName).Value;
        ViewBag.Message = String.Format("Welcome {0}!", userfirstName);

        return View();
    ```
26. Click on the **Web.config** file and locate the **\<appSettings\>** section.
27. Add the following code under **\<appSettings\>** section.

    ```xml
        <add key="ClientId" value="[Azure Application ID]" />
        <add key="AADInstance" value="https://login.microsoftonline.com/" />
        <add key="Domain" value="[Azure AD Default Domain]" />
        <add key="TenantId" value="[Directory ID]" />
    ```
28. Select the **AzureADWebApp** project, and then go to the properties view.
29. Change **SSL Enabled** from **False** to **True**.  
30. Copy to clipboard **SSL Url** value.
31. Right click the **AzureADWebApp** project, and select **Properties**.
32. Select **Web** on the left menu, replace **Project Url** with **SSL Url** that we copied before and press **Ctrl+S**.
33. Open Microsoft Edge.
34. Navigate to **https://portal.azure.com**.
35. If a page appears prompting for your email address, enter your email address, and then click **Next** and enter your password, and then click **Sign In**.
36. If the **Stay signed in?** dialog appears, click **Yes**.

   >**Note**: During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.   
37. Click **Azure Active Directory** on the left side of the portal, and then click **App registrations**.
38. Click on **New application registration**.
    - **Name**, enter **AzureADWebApp**.
    - **Sign-on URL**, enter the **SSL Url** from the privous task.
    - click **Create**.
39. Copy **Application ID** value, and go back to visual studio to **web.config** file and replace **ClientId** value.
40. Click **Azure Active Directory** on the left side of the portal, then click **Custom domain names**, and copy the **NAME**.
41. Go back to visual studio to **web.config** file and replace **Domain** value.
42. Click **Azure Active Directory** on the left side of the portal, then click **Properties**, and copy the **Directory ID**.
43. Go back to visual studio to **web.config** file and replace **TentantId** value.
44. To save the changes, press Ctrl+S.
45. To run the application, press F5.
46. Login with user name and password.
47. After the site was loaded, click in **Contact**, and see your name displayed.   




# Lesson 3: Azure Active Directory B2C

### Demonstration: Authorizing client applications using Azure AD B2C

#### Preparation Steps

  You need to have two available emails, one for the azure portal and the second for creating new user.

#### Demonstration Steps

1. Open **Microsoft Edge**.
2. Go to the Azure portal at **http://portal.azure.com**.
3. If a page appears, prompting for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials.

4. On the navigation menu on the left, click **Create a resource**, and search **Azure Active Directory B2C**, click **Create**.
5. In **Create new B2C Tenant to existing Tenant** ,select **Create a new Azure AD B2C Tenant**, provide the following information:
    - Organization name type **B2CMod11**.
    - Initial domain name type **b2cmod11**YourInitials (Replace YourInitials with your initials).
    - Country or region select your Country.
6. Click on **Create**.
7. In **Create new B2C Tenant to existing Tenant** ,select **Link an existing Azure AD B2C Tenant to my Azure subscription**, provide the following information:
    - **Azure AD B2C Tenant** select **``b2cmod11[YourInitials].onmicrosoft.com``** (Replace YourInitials with your initials).
    - Resource group select **Create new** and type **B2C**.
8. Click on **Create**, and wait until the resource is created.
9. On top bar click on your user information, the menu will open and under **DIRECTORY** select **B2CMod11**.
    >**Note :** if you don't see **B2CMod11** just refresh the page.
10. On top search bar type **Azure AD B2C** and navigate.
11. On the **Azure AD B2C** blade, click **Applications** under **Manage** subtitle.
12. Click on **Add** and provide the following information:
    - Name type **B2C App**.
    - Web App / Web API select **Yes**.
    - Reply URL type **``http://localhost:51136/``**
    - App ID URL type **api**.
    - Native Client select **Yes**.
13. Click on **Create**.
14. On the **Azure AD B2C** blade, click **Sign-up or sign-in policies**, under **POLICIES** subtitle.
15. Click on **Add** and provide the following information: 
    - **Name** type **SignIn-Policy**
    - Click on **Identity providers**, select **Email signup**, click **OK**.
    - Click on **Sign-up attributes** , select **Display Name**, click **OK**.
    - Click on **Application claims**, select **Display Name**, click **OK**. 
16. Click **Create**.
17. On the **Azure AD B2C** blade, click **Applications** under **Manage** subtitle.
18. Click on **B2CApp**, and copy the **Application ID** to the next steps.
19. On the **B2C App - Properties** blade, click **API access**, under **API ACCESS** subtitle.
20. Click on **Add**, in **Select Scopes** select **user_impersontion**, and click **Save**.  
21. Open **Visual Studio 2017**.
22. On the **File** menu, point to **Open**, and then click **Project/Solution**.
23. Go to **[repository root]\Allfiles\20487C\Mod11\DemoFiles\ClientAppUsingB2C**.
24. Select the **ClientAppUsingB2C.sln** file, and then click **Open**.
25. In **Solution Explorer**, under the **ClientAppUsingB2C.Server** project, double-click **Web.config**.
26. Replace the following information: 
    - **Tenant** replace YourInitials with your initials.
    - **ClientId** replace with point 32.
27. In **Solution Explorer** right click on **ClientAppUsingB2C.Server**, select **Publish**.
28. In **Pick a publish target** window select **App Service**, select **Create New** and click on **Create Profile**.
29. In **App Name** type **ClientAppUsingB2CServer**[YourInitials] (Replace YourInitials with your initials).
30. Click on **Create**, and select **Publish**.
31. Open **Microsoft Edge**, and type in the url **``http://clientappusingb2cserver[YourInitials].azurewebsites.net/api/values``** (Replace YourInitials with your initials).
32. You should get the following message **Authorization has been denied for this request**.
33. Go back to visaul studio.
34. In **Solution Explorer**, under the **ClientAppUsingB2C.Client** project,expand **App.xaml** and double-click **App.xaml.cs**.
35. Replace the following information: 
    - **Tenant** replace YourInitials with your initials.
    - **ClientId** replace with point 32.
    - **ApiScopes** replace YourInitials with your initials.
    - **ApiEndpoint** replace YourInitials with your initials.
36. In **Solution Explorer**, under the **ClientAppUsingB2C.Client** right click **Set as StartUp Project**.
37. Run the application, press Ctrl+F5.
38. In the App Click on **Sign In**.
39. Click on **Sign up now**, Enter your details.
40. Click on **Call Api**.
41. Now display on your screen **Success you finish the demo. You integrated  the WebApi with the client app**. 
42. Go back to **Azure protal**.
43. On the **Azure AD B2C** blade, click **Users**, under **MANAGE** subtitle.
44. Your Display Name should now appear in the **All users** list.


# Lesson 3: Azure Active Directory B2C

### Demonstration: Configuring social logins using Azure AD B2C

#### Preparation Steps

  You need to have two available emails, one for the azure portal and the second for creating new user.
  And also you need to finish the previous demo.

#### Demonstration Steps

1. Open **Microsoft Edge**.
2. Navigate to **https://apps.dev.microsoft.com**.
3. Login with your azure accout email.
4. Click on **Add an app**.
5. In Application Name type **B2CApp**, click **Create**.
6. Copy the **Application Id** to the following tasks.
7. **Application Secrets** click on **Generate New Password**, copy the code to the following tasks, and click **OK**.
8. Under **Platforms** click on **Add Platform**, select **Web**.
9. In **Redirect URLs** type **``https://login.microsoftonline.com/te/b2cmod11[YourInitials].onmicrosoft.com/oauth2/authresp``** (Replace YourInitials with your initials).
10. Click on **Save**.
11. Open **Microsoft Edge**.
12. Go to the Azure portal at **http://portal.azure.com**.
13. If a page appears, prompting for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials.

14. On top bar click on your user information, the menu will open and under **DIRECTORY** select **B2CMod11**.
    >**Note :** if you don't see **B2CMod11** just refresh the page.
15. On top search bar type **Azure AD B2C** and navigate.
16. On the **Azure AD B2C** blade, click **Identity providers** under **Manage** subtitle.
17. Click on **Add** and provide the following information:
    - **Name** type **Microsoft**.
    - Click on **identity provider type**, select **Microsoft Account**.
    - Click on **Set up this identity provider**.
        - **Client ID** paste from point 6.
        - **Client secret** paste from point 7.
        - Click **OK**.
18. Click **Create**.
19. On the **Azure AD B2C** blade, click **Sign-up or sign-in policies**, under **POLICIES** subtitle.
20. In **Azure AD B2C - Sign-up or sign-in policies**, click on **B2C_1_SignIn-Policy**.
21. On top bar click on **Edit**, select **Identity providers**.
22. Select **Microsoft** , click **OK** and click on **Save**.

23. On the **Azure AD B2C** blade, click **Applications** under **Manage** subtitle.
24. Click on **B2CApp**, and copy the **Application ID** to the next steps.
25. On the **B2C App - Properties** blade, click **Keys**, under **GENERAL** subtitle.
26. Click on **Generate key**, then click **Save** and copy the **App key** to the next steps.

27. Open **Visual Studio 2017**.
28. On the **File** menu, point to **Open**, and then click **Project/Solution**.
29. Go to **[repository root]\Allfiles\20487C\Mod11\DemoFiles\AzureSocialLoginB2C**.
30. Select the **AzureSocialLoginB2C.sln** file, and then click **Open**.
31. In **Solution Explorer**, under the **AzureSocialLoginB2C** project, double-click **Web.config**.
32. Replace the following information: 
    - **Tenant** replace YourInitials with your initials.
    - **ClientId** replace with point 24.
    - **ClientSecret**  replace with point 26.
33. To run the project, press Ctrl+F5.
34. On the top right, click on **Sign in**, and then click on **Microsoft** under **Sign in with your social account**.
35. Sign in using your second Microsoft account.
36. Confirm terms of use by clicking **Yes**.
37. Type in Display Name, then click **Continue**.
38. Go back to **Azure protal**.
39. On the **Azure AD B2C** blade, click **Users**, under **MANAGE** subtitle.
40. Your Display Name should now appear in the **All users** list.

