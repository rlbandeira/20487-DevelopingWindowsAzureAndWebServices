# Module 11: Identity Management and Authorization on Azure Active Directory

Wherever you see a path to file starting with **[repository root]**, replace it with the absolute path to the directory in which the 20487 repository resides. For example, if you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path, **[repository root]\AllFiles\20487C\Mod01** will become **C:\Users\John Doe\Downloads\20487\AllFiles\20487C\Mod01**.

# Lesson 1: Claims-based Identity Concepts

### Demonstration: Using claims in an ASP.NET MVC application

#### Demonstration Steps

1. Open a browser. 
2. In the address bar, enter **https://portal.azure.com**, and the press Enter.
3. In theAzure portal, click **Create a resource**.
4. In the **New** blade, click **Web + Mobile**, and then click **Web App**.
5. In the **App Name** text box, enter **claims-example-***yourinitials* (replace *yourinitials* with your initials, e.g. – John Doe with  j.d.)
6. Click **Create** and wait for the creation process to finish.
7. In the **Authentication/Authorization** blade, enable **App Service Authentication**.
8. In the **Action to take when request is not authenticated** list box, select **Log in with Azure Active Directory**.
9. Under **Authentication Providers**, click **Azure Active Directory**.
10.	In **Azure Active Directory Settings**, change **Management Mode** to **Express**.
11.	Select **Create New AD App**, and the click **OK**. The blade closes.
12.	On the **Authentication/Authorization** blade, click **Save**.
13.	Click the **Overview** blade, and then click the **Authentication/Authorization** blade.
    >**Note**: Due to a bug in the Azure portal, you need to go out of the **Authentication/Authorization** blade and enter it again for the portal to recognize the new authentication settings.
14.	On the **Authentication/Authorization** blade, click **Azure Active Directory**.
15.	In **Azure Active Directory Settings**, click **Azure AD App**.
16.	On the **Azure AD Applications** blade, copy the value under the **CLIENT ID** column.
17.	Open **Visual Studio 2017**.
18.	Click **File**, point to **New**, and then click **Project**.
19.	In the **New Project** modal, expand **Installed**, and then expand **Visual C#**.
20.	Click **Web**, and then select the **ASP.NET Web Application (.NET Framework)** template.
21.	In the **Name** text box, enter **ClaimsExampleApp**.
22.	Click **OK**.
23.	In the **New ASP.NET Web Application** modal, select the **MVC** template.
24.	Click **Change Authentication**.
25.	In the **Change Authentication** dialog box, select **Work or School accounts**.
26.	Expand **More Options**.
27.	Select the **Overwrite the application entry if one with the same ID exists** check box.
28.	In the **Client ID** text box, paste the value you copied in step 16.
29.	Click **OK**.
30.	In the **New ASP.NET Web Application** modal, click **OK**.
31.	In **Solution Explorer**, expand the **App_Start** folder, and then open the **Startup.Auth.cs** file.
32.	Add the following **using** directives:
   ```cs
        using System.Threading.Tasks;
        using Microsoft.Owin.Security.Notifications;
        using Microsoft.IdentityModel.Protocols;
   ```
33.	Under **PostLogoutRedirectUri**, add the following piece of code:
   ```cs
        Notifications = new OpenIdConnectAuthenticationNotifications
        {
            SecurityTokenValidated = OnTokenValidated
        }
   ```
34.	At the end of the **ConfigureAuth** method, add the following code:
   ```cs
        private async Task OnTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context) => await Task.FromResult(0);
   ```
35.	Put a breakpoint in the **OnTokenValidated** method that you just added.
36.	To debug the application, press F5.
		The browser opens a page with the address starting with **https://login.microsoftonline.com**. The page has two buttons, **Accept** and **Cancel**.
37.	Click **Accept**. The application stops.
38.	Inspect the **context** object, expand **AuthenticationTicket**, and then inspect the **Identity** member.
39.	To see the list of claims, expand **Identity**, expand **Claims**, and then expand **Results View**.
40.	Review the list of claims and note some familiar claims, such as your name and the email address that you used to sign up to Microsoft Azure.
    >**Note**: The list of claims you saw in step 39 is provided by Microsoft Azure. In the next lesson you will be introduced to Azure Active Directory, the identity provider used in this demonstration. 

# Lesson 2: Introduction to Azure Active Directory

### Demonstration: Exploring Azure AD

#### Preparation Steps

  You need two available emails, one for the Azure portal and the second for creating a new user.

#### Demonstration Steps

1. Open **Microsoft Edge**.
2. Go to the Azure portal at **http://portal.azure.com**.
3. If a page appears, prompting for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials.

4. On the navigation menu on the left, click **Azure Active Directory**.
5. On the **Azure Active Directory** blade, click **Custom domain names**, and then copy the value in the **NAME** field.
6. Go back to the **Azure Active Directory** view, under the **Manage** section, click **Users** .
7. On the top menu, click **New user**.
    1. In the **Name** text box, enter your full name.
    2. In the **User name** text box, enter your name + **@[Paste Domain name]** .
    3. Click **Create**.
8. On the top menu, click **New guest user**.
9. Enter the email address in the text box, and then click **Invite**.
10. Open the email and accept the invitation.
11. In the portal, go to **Azure Active Directory**.
12. On the **Azure Active Directory** blade, under the **Manage** section, click **Groups**.
13. On the top menu, click **New group**.
    - In the **Group type** list box, select **security**.
    -  In the **Group name** text box, enter **Mod11Group**.
    - In the **Membership type** list box, select **Assigned**.
    - Click **Members**, select the guest user, and then click **Select**.
    - Click **Create**.
14. Open **Groups**, see that the **Group** is created.      


### Demonstration: Using Azure AD to secure ASP.NET Web Applications.

#### Demonstration Steps

1. Open **Visual Studio 2017**.
2. On the **File** menu, point to **New**, and then click **Project**.
3. In the **New Project** dialog box, on the navigation pane, expand the **Installed** node, expand the **Visual C#** node, and then click the **Web** node.
4. From the list of templates, select **ASP.NET Web Application (.NET Framework)**.
5. In the **Name** text box, type **AzureADWebApp**.
6. In the **Location** text box, type **[repository root]\Allfiles\20487C\Mod11\Democode\AzureADWebApp\Begin**, and then click **OK**.
7. In the **New ASP.NET Web Application - AzureADWebApp** dialog box, click **MVC**, and then click **OK**.
8. In **Solution Explorer**, under the **MyApp** project, expand the **App\_Start** folder, and then double-click **WebApiConfig.cs**.
9. Right-click the **AzureADWebApp** project, and then click **Manage NuGet Packages**.
    1. In the **NuGet: AzureADWebApp** window, click **Browse**.
    2. In the search box on the top left of the window, enter **Microsoft.Owin.Host.SystemWeb**, and then press **Enter**.
    3. From the results, select **Microsoft.Owin.Host.SystemWeb**, and then click **Install**.
    4. If a **Preview Changes** modal appears, click **OK**.
    5. In the **License Acceptance** modal, click **I Accept**.
    6. In the search box on the top left of the window, enter **Microsoft.Owin.Security.Cookies**, and then press **Enter**.
    7. From the results, select **Microsoft.Owin.Security.Cookies**, and then click **Install**.
    8. If a **Preview Changes** modal appears, click **OK**.
    9. In the **License Acceptance** modal, click **I Accept**.
    10. In the search box on the top left of the window, enter **Microsoft.Owin.Security.OpenIdConnect**, and then press **Enter**.
    11. From the results, select **Microsoft.Owin.Security.OpenIdConnect**, and then click **Install**.
    12. If a **Preview Changes** modal appears, click **OK**.
    13. In the **License Acceptance** modal, click **I Accept**.
    14. Wait until the package is completely downloaded and installed. To close the **NuGet Package Manager: AzureADWebApp** dialog box, click **Close**.
10. Right click the **AzureADWebApp** project, and then point to **Add**, and then select **Class**.
11. In the **Add New Item- AzureADWebApp** dialog box, in the **Name** text box, type **Startup.Auth.cs**, and then click **Add**.
12. Add the following **using** directives at the beginning of the class.
   ```cs
        using Owin;
        using Microsoft.Owin.Security;
        using Microsoft.Owin.Security.Cookies;
        using Microsoft.Owin.Security.OpenIdConnect;
   ```
13. Replace the **class** declaration with the following code.
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
16. Drag the current class to the **App_Start** folder.
17. Right-click the **AzureADWebApp** project, point to **Add**, and then select **Class**.
18. In the **Add New Item- AzureADWebApp** dialog box, in the **Name** text box, type **Startup.cs**, and then click **Add**. 
19. At the beginning of the class, add the following **using** directives.
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
22. Expand the **Controller** folder, select **HomeController.cs**.
23. To the **HomeController** class, add an **[Authorize]** attribute.
24. At the beginning of the class, add the following **using** directives.
   ```cs
        using System.Security.Claims;
   ```
25. Replace the **Contact** action content with the following code.
   ```cs
        string userfirstName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.GivenName).Value;
        ViewBag.Message = String.Format("Welcome {0}!", userfirstName);
        return View();
   ```
26. Click the **Web.config** file, and locate the **\<appSettings\>** section.
27. Under the **\<appSettings\>** section, add the following code.
   ```xml
        <add key="ClientId" value="[Azure Application ID]" />
        <add key="AADInstance" value="https://login.microsoftonline.com/" />
        <add key="Domain" value="[Azure AD Default Domain]" />
        <add key="TenantId" value="[Directory ID]" />
   ```
28. Select the **AzureADWebApp** project, and then go to the **properties** view.
29. Change the value for **SSL Enabled** from **False** to **True**.  
30. Copy the value from **SSL Url**.
31. Right-click the **AzureADWebApp** project, and select **Properties**.
32. On the left menu, select **Web**, replace **Project Url** with the value for **SSL Url** that we copied earlier, and then press **Ctrl+S**.
33. Open **Microsoft Edge**.
34. Navigate to **https://portal.azure.com**.
35. If a page appears prompting for your email address, enter your email address, click **Next**, enter your password, and then click **Sign In**.
36. If the **Stay signed in?** dialog box appears, click **Yes**.
   >**Note**: During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.   
37. On the left side of the portal, click **Azure Active Directory**, and then click **App registrations**.
38. Click **New application registration**.
    - In the **Name** text box, type **AzureADWebApp**.
    - In the **Sign-on URL** text box, enter the value from **SSL Url** from the previous task.
    - Click **Create**.
39. Copy the value from **Application ID**, go back to Visual Studio and in the **web.config** file, replace the value in **ClientId** with the copied value.
40. On the left side of the portal, click **Azure Active Directory**, then click **Custom domain names**, and copy the **NAME**.
41. Go back to Visual Studio, in the **web.config** file, replace the value in **Domain** with the copied value.
42. On the left side of the portal, click **Azure Active Directory**, click **Properties**, and then copy the value from **Directory ID**.
43. Go back to Visual Studio, in the **web.config** file, replace the value in **TentantId** with the copied value.
44. To save the changes, press Ctrl+S.
45. To run the application, press F5.
46. Login with user name and password.
47. After the site loads, click **Contact**, and your name appears.   

# Lesson 3: Azure Active Directory B2C

### Demonstration: Authorizing client applications using Azure AD B2C

#### Preparation Steps

  You need two available emails, one for the Azure portal and the second for creating a new user.

#### Demonstration Steps

1. Open **Microsoft Edge**.
2. Go to the Azure portal at **http://portal.azure.com**.
3. If a page appears, prompting for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials.

4. On the navigation menu on the left, click **Create a resource**, search for **Azure Active Directory B2C**, and then click **Create**.
5. In **Create new B2C Tenant to existing Tenant**, select **Create a new Azure AD B2C Tenant**, and then provide the following information:
    - In the **Organization name** text box, type **B2CMod11**.
    - In the **Initial domain name** text box, type **b2cmod11***YourInitials* (Replace *YourInitials* with your initials).
    - In the **Country or region** list box, select your country.
6. Click **Create**.
7. In **Create new B2C Tenant to existing Tenant**, select **Link an existing Azure AD B2C Tenant to my Azure subscription**, and then provide the following information:
    - In the **Azure AD B2C Tenant** list box, select **``b2cmod11***[YourInitials]***.onmicrosoft.com``** (Replace *YourInitials* with your initials).
    - In the **Resource group** list box, select **Create new**, and then type **B2C**.
8. Click **Create**, and then wait until the resource is created.
9. On top bar, click your user information, the menu opens. Under **DIRECTORY**, select **B2CMod11**.
    >**Note :** if you don't see **B2CMod11**, refresh the page.
10. On top search bar, type **Azure AD B2C**, and then navigate.
11. On the **Azure AD B2C** blade, under the **Manage** section, click **Applications**.
12. Click **Add**, and then provide the following information:
    - In the **Name** text box, type **B2C App**.
    - In **Web App / Web API**, select **Yes**.
    - In the **Reply URL** text box, type **``http://localhost:51136/``**
    - In the **App ID URL** text box, type **api**.
    - In **Native Client**, select **Yes**.
13. Click **Create**.
14. On the **Azure AD B2C** blade, under then **POLICIES** section, click **Sign-up or sign-in policies**.
15. Click **Add** and provide the following information: 
    - In the **Name** text box, type **SignIn-Policy**
    - Click **Identity providers**, select **Email signup**, and then click **OK**.
    - Click **Sign-up attributes** , select **Display Name**, and then click **OK**.
    - Click **Application claims**, select **Display Name**, and then click **OK**. 
16. Click **Create**.
17. On the **Azure AD B2C** blade, under the **Manage** section, click **Applications**.
18. Click **B2CApp**, and copy the value from **Application ID** for the next steps.
19. On the **B2C App - Properties** blade, under the **API ACCESS** section, click **API access**.
20. Click **Add**, in **Select Scopes**, select **user_impersonation**, and then click **Save**.  
21. Open **Visual Studio 2017**.
22. On the **File** menu, point to **Open**, and then click **Project/Solution**.
23. Go to **[repository root]\Allfiles\20487C\Mod11\DemoFiles\ClientAppUsingB2C**.
24. Select the **ClientAppUsingB2C.sln** file, and then click **Open**.
25. In **Solution Explorer**, under the **ClientAppUsingB2C.Server** project, double-click **Web.config**.
26. Replace the following information: 
    - In the **Tenant** text box, replace *YourInitials* with your initials.
    - In the **ClientId** text box, replace with point 32.
27. In **Solution Explorer**, right-click **ClientAppUsingB2C.Server**, and select **Publish**.
28. In the **Pick a publish target** window, select **App Service**, select **Create New** and click **Create Profile**.
29. In **App Name**, type **ClientAppUsingB2CServer***[YourInitials]* (Replace *YourInitials* with your initials).
30. Click **Create**, and select **Publish**.
31. Open **Microsoft Edge**, and type **``http://clientappusingb2cserver***[YourInitials]***.azurewebsites.net/api/values``** (Replace *YourInitials* with your initials).
32. The **Authorization has been denied for this request** message appears.
33. Go back to Visual Studio.
34. In **Solution Explorer**, under the **ClientAppUsingB2C.Client** project, expand **App.xaml**, and then double-click **App.xaml.cs**.
35. Replace the following information: 
    - In the **Tenant** text box, replace *YourInitials* with your initials.
    - In the **ClientId** text box, replace with point 32.
    - In the **ApiScopes** text box, replace *YourInitials* with your initials.
    - In the **ApiEndpoint** text box, replace *YourInitials* with your initials.
36. In **Solution Explorer**, under **ClientAppUsingB2C.Client**, right-click **Set as StartUp Project**.
37. To run the application, press Ctrl+F5.
38. In the app, click **Sign In**.
39. Click **Sign up now** and enter your details.
40. Click **Call Api**.
41. The **Success you finish the demo. You integrated  the WebApi with the client app** message appears. 
42. Go back to **Azure protal**.
43. On the **Azure AD B2C** blade, under the **MANAGE** section, click **Users**.
44. Your display name appears in the **All users** list.

### Demonstration: Configuring social logins using Azure AD B2C

#### Preparation Steps

  You need two available emails, one for the Azure portal and the second for creating new user.
  You must finish the previous demonstration before starting this one.

#### Demonstration Steps

1. Open **Microsoft Edge**.
2. Go to **https://apps.dev.microsoft.com**.
3. Login with your Azure id.
4. Click **Add an app**.
5. In the **Application Name** text box, type **B2CApp**, and then click **Create**.
6. Copy the value from **Application Id** to the following tasks.
7. In **Application Secrets**, click **Generate New Password**, copy the code to the following tasks, and then click **OK**.
8. Under **Platforms**, click **Add Platform**, and then select **Web**.
9. In **Redirect URLs**, type **``https://login.microsoftonline.com/te/b2cmod11***[YourInitials]***.onmicrosoft.com/oauth2/authresp``** (Replace *YourInitials* with your initials).
10. Click **Save**.
11. Open **Microsoft Edge**.
12. Go to the Azure portal at **http://portal.azure.com**.
13. If a page appears, prompting for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials.

14. On the top bar, click your user information, the menu will open, and under **DIRECTORY** select **B2CMod11**.
    >**Note :**If you don't see **B2CMod11**, refresh the page.
15. On the top search bar, type **Azure AD B2C** and navigate.
16. On the **Azure AD B2C** blade, under the **Manage** section, click **Identity providers**.
17. Click  **Add** and provide the following information:
    - In the **Name** text box, type **Microsoft**.
    - Click **identity provider type**, select **Microsoft Account**.
    - Click **Set up this identity provider**.
        - In the **Client ID** text box, paste the value from step 6.
        - In the **Client secret** text box, paste the value from step 7.
        - Click **OK**.
18. Click **Create**.
19. On the **Azure AD B2C** blade, under the **POLICIES** section, click **Sign-up or sign-in policies**.
20. In **Azure AD B2C - Sign-up or sign-in policies**, click **B2C_1_SignIn-Policy**.
21. On the top bar, click **Edit**, and then select **Identity providers**.
22. Select **Microsoft**, click **OK**, and then click **Save**.
23. On the **Azure AD B2C** blade, under **Manage** section, click **Applications**.
24. Click **B2CApp**, and copy the value from **Application ID** to the next steps.
25. On the **B2C App - Properties** blade, under the **GENERAL** section, click **Keys**.
26. Click **Generate key**, click **Save** and then copy the value from **App key** to the next steps.
27. Open **Visual Studio 2017**.
28. On the **File** menu, point to **Open**, and then click **Project/Solution**.
29. Go to **[repository root]\Allfiles\20487C\Mod11\DemoFiles\AzureSocialLoginB2C**.
30. Select the **AzureSocialLoginB2C.sln** file, and then click **Open**.
31. In **Solution Explorer**, under the **AzureSocialLoginB2C** project, double-click **Web.config**.
32. Replace the following information: 
    - In the **Tenant** text box, replace *YourInitials* with your initials.
    - In the **ClientId** text box, paste the value from step 24.
    - In the **ClientSecret**  text box, paste the value from step 26.
33. To run the project, press Ctrl+F5.
34. On the top right, click **Sign in**, and then under **Sign in with your social account**, click **Microsoft**.
35. Sign in using your second Microsoft account.
36. To confirm terms of use, click **Yes**.
37. Enter you display name, and click **Continue**.
38. Go back to the Azure portal.
39. On the **Azure AD B2C** blade, under the **MANAGE** section, click **Users**.
40. Your display name appears in the **All users** list.

