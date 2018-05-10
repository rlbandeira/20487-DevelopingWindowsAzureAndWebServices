# Module 8: Deploying Services

Wherever  you see a path to a file that starts with *[repository root]*, replace it with the absolute path to the folder in which the 20487 repository resides. For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, the following path: **[repository root]\AllFiles\20487C\Mod06** should be changed to **C:\Users\John Doe\Downloads\20487\AllFiles\20487C\Mod06**.

## Setup

For this demo you will need IIS activated on your machine.
To activate IIS on Windows 10, perform the following steps:
1. Open the **Start** menu and search for **Turn windows features on or off** and click it.
2. In the **Windows Features** window, check **Internet Information Services**.
3. Expand **Internet Information Services**.
4. Expand **World Wide Web Services**.
5. Check **Application Development Features**.
6. Click **OK**.

# Lesson 1: Web Deployment with Visual Studio 2017

### Demonstration: Deploying a Web Application by Using Visual Studio

#### Demonstration Steps

1. Click **Start** and in the **Search box** type Visual Studio. In the **list of search results**, right-click **Visual Studio 2017** and then click **Run as administrator**, In the **User Account Control** dialog box, click **Yes**.
2. On the **File** menu, point to **New**, and then click **Project**.
3. In the **New Project** dialog box, in the left pane, expand the **Installed** node, expand the **Visual C#** node, click the **Web** node, and then select **ASP.NET Web Application (.NET Framework)** from the list of templates.
4. In the **Name** text box, type **MyApp**.
5. In the **Location** text box, type **[repository root]\Allfiles\20487C\Mod08\DemoFiles\DeployWebApp\begin**.
6. Clear the **Create directory for solution** check box, and then click **OK**.
7. In the **New ASP.NET Web Application - MyApp** dialog box, select **Web API**, and then click **OK**.
8. In **Solution Explorer**, right-click the **MyApp** project, and then click **Publish**.
9. On the **Pick a publish target** page, select **IIS, FTP, etc**, and then click **Publish**.
10. In the **Publish** dialog box, verify that the selected publish method is **Web Deploy**, and set the rest of the fields by using the following values:
    - Server: **localhost**
    - Site Name: **Default Web Site/MyApp**
    - Destination URL: **http://localhost/MyApp/api/values**
11. Click **Validate Connection**, and wait for the green check mark to appear.
12. Click **Next** and then click **Save**.
13. Wait until the publishing completes and the browser opens. You will see either a XML or a JSON output depending on your browser.
14. Return to Visual Studio 2017, and in **Solution Explorer**, under the **MyApp** project, expand the **Controllers**  folder, and then double-click **ValuesController.cs**.
15. Locate the parameterless **Get** method, and change its code as follows:

  ```cs
		return new string[] { "value1", "value2", "value3" };
```
16. To save changes that you made to the code file, press Ctrl+S.
17. In **Solution Explorer**, right-click the **MyApp** project, and then click **Publish**.
18. In the **Publish** page, click **Publish**.
19. Wait until the publishing completes and the browser opens. Verify that you see three values.
20. Close all open windows.

# Lesson 2: Creating and Deploying Web Application Packages

### Demonstration: Exporting and Importing Web Deploy Packages by Using IIS Manager

#### Preparation Steps

> This demonstration is based on Lesson 1, make sure to complete it before starting this lesson.

If you are using Windows 10, Windows Server 2016 (or newer), application package export and import feature is not available in the default IIS configuration and you will need to enable it.

First, check that you have the application package export/import available:
1. In the **Start** menu, search for **IIS**, and then click **Internet Information Services (IIS) Manager**.
2. In the **Internet Information Services (IIS) Manager** window, expand the _**yourcomputername**_ node. (*yourcomputername* is your computer name, you will most likely have only one node under connections).
3. Expand the **Sites** node, expand the **Default Web Site** node, and then click the **MyApp** web application node.
4. On the right-hand side of the window, you should see either **Actions**, **Deploy** or both.
5. If you have the **Application Export/Import** feature enabled, then you should see two items under either **Actions** or **Deploy**:
    - **Export Application Package**
    - **Import Application Package**

In the case the above feature isn't enabled and you have Windows 10 or Windows Server 2016 or later:

First, you will have to replace the existing web deploy supplied by Visual Studio 2017 as you will be unable to modify it.
1. In the **Start** menu, search for **Add or Remove Programs** and click it.
2. In the **Apps & Features** pane, in the search box, enter **Microsoft Web Deploy 3.6**.
3. Click the result with the name **Microsoft Web Deploy 3.6**, click **Uninstall**, and then follow the on-screen instructions.
4. After installation of **Microsoft Web Deploy 3.6** is complete, open a browser and go to **https://www.microsoft.com/en-us/download/details.aspx?id=43717**, and then click **Download**, Download the Software accoridng to your system requirements.
5. When the download finishes, open the downloaded file.
6. Click **Next**, select the **I accept the terms...** check box, and then click **Next**.
7. In  **Choose Setup Type**, click **Complete**, click **Install**,If any **User Account control** dialog box appears, then click **Yes** to proceed.
8. After the setup completes, click **Finish**.

To verify that the installation worked, follow the first set of instructions above.

If the installation still did not help, do the following:

1. In the **Start** menu, search for **Microsoft Web Platform Installer** and click on it. In **User Control** dialogue box click **Yes**.
2. In the right search bar, type **Web Deployment tool** and press **Enter**.
3. Locate the **Web Deployment Tool 2.1** , click **ADD** and then click **Install**.
4. Follow the installer instructions.

To verify that the installation worked, follow the first set of instructions above.

#### Demonstration Steps

1. In the **Start** menu, search for **Internet Information Services (IIS)** and click it.
2. In the **Connections** pane, expand the _**yourcomputername**_ node. (*yourcomputername* is your computer name, you will most likely have only one node under connections).
3. If you receive a message that asks whether you want to get started with Microsoft Web Platform, click **No**. 
4. While still on the **Connections** pane, expand the **Sites** node, expand the **Default Web Site** node, and then click the  **MyApp** web application node. 
5. In the **Actions** pane, under **Deploy**, click **Export Application**.
6. In the **Export Application Package** dialog box, click **Next**, and then click **Next** again.
7. In the **Package path** text box, browse to the **Downloads** folder, in the **File name** text box, type **MyApp.zip**, click **Save**, and then click **Next**.
8. Wait for the export to complete, and then click **Finish**.
9. On the left-hand side of the window, right-click **MyApp**, and then click **Remove**.
10. Click **Yes**.
11. Open a browser and navigate to **http://localhost/MyApp/api/values**, you should get a 404 error.
12. Click the **Default Web Site** node.
13. In the **Actions** pane, under **Deploy**, click **Import Application**.
14. In the **Package path** text box of the **Import Application Package** dialog box, browse to the **Downloads** folder, select **MyApp.zip**, and then click **Open**.
15. Click **Next** twice, verify that in the **Enter Application Package Information** step of the wizard, the **Application Path** text box is set to **MyApp**, and then click **Next**.
16. In the **Overwrite Existing Files** step, select **Yes**, and then click **Next**.
17. Wait for the import to complete, and then click **Finish**.
18. Open a browser and navigate to **http://localhost/MyApp/api/values**, you should see either XML or JSON output, depending on your browser.
19. Close all open windows.

# Lesson 3: Command-Line Tools for Web Deploy

### Demonstration: Using Windows PowerShell Cmdlets

#### Preparation Steps

> This lesson is based on Lesson 2, make sure to complete lesson 2 before starting this lesson.

#### Demonstration Steps

1. Open the **Start** menu and search for **Windows Powershell**.
2. Right-click **Windows Powershell**, click **Run as Administrator**, then click **Yes**.
3. In the **Administrator: Windows PowerShell** window, type the following command, and then press Enter.

  ```cs
		Add-PSSnapin WDeploySnapin3.0
```
4. To create a publish settings file, type the following command, and then press Enter.

  ```cs
		New-WDPublishSettings -ComputerName 127.0.0.1 -AgentType MSDepSvc -FileName "C:\[path to downloads folder]\Server.publishsettings"
```

   >**Note** :**path to downloads folder** variable should be replaced with your downloads folder path.
	
5. To synchronize the web application to the remote server, type the following command, and then press Enter.

  ```cs
		Sync-WDApp "Default Web Site/MyApp" "Default Web Site/MyAppDeployedWithPowerShell" -DestinationPublishSettings "C:\[path to downloads folder]\Server.publishsettings"
```
6. Click **Start** and search for **services.msc**, in results click **services**.
7. In the **Services** window go to **Web Deployment Agent Service** and then under status verify that the service is running. If not right click and then click **Start**.
8. Open a browser and navigate to **http://localhost/MyAppDeployedWithPowerShell/api/values**.
9. Wait until the response is returned, you should see three values in the form of either XML or JSON, depending on your browser.
10. Close all open windows.

# Lesson 4: Continuous Delivery with TFS and Git

### Demonstration: Continuous Delivery with Visual Studio Team Services

#### Preparation Steps
  
To present this demonstration, you must have a Microsoft account. If you have not created a Microsoft account before, create one before you start the demonstration.

> **Important!** Make sure you are connected to your Microsoft account in Visual Studio 2017 before starting this demonstration!

#### Demonstration Steps

1. On the **Start** screen, click the **Microsoft Edge** tile.
2. Go to **https://www.visualstudio.com/team-services/** and click **Get started for free**.
3. On the **Sign in** page, enter your **Microsoft account** email address,and then click **Next**.
   > If instead of a **Sign in** page, you see a **Pick an account** page, pick your account and click **Next**.
4. On the **Enter password** page, enter your password, and then click **Sign in**.
   
   If this is the first time you logged in with your account to VSTS, follow the next steps, else skip to step 14.

5. In the **Host my projects at** page, enter a unique name. (We will relate to that unique name as _youraccount_ from now on.)
6. Under **Manage code using**, select **Team Foundation Version Control**.
7. Click **Continue**.
8. Wait until the account creation is done and you are redirected to the **MyFirstProject** page.
9. In the **MyFirstProject** page, point to the **settings** icon on the top menu bar, and then click **Account settings**.
10. Under **Projects**, point to **MyFirstProject**, and then click the **three dots** next to it.
11. Click **Delete**, in the text box enter **MyFirstProject**, and then click **Delete Project**, and then click **Close**.
12. Click the VSTS logo on the top left.
13. If you already have projects in your account, click **New Project**, else skip to the next step.
14. In the **Create new project** page, enter the following details:
    - Project name: **MyApp**
    - Version control: **Team Foundation Version Control**
15. Click **Create**. 
16. Wait for the project to be created.
17. Open **Visual Studio 2017**.
18. On the **View** menu, click **Team Explorer**.
19. In the **Team Explorer** window, click the **Manage Connections** icon, click the **Manage Connections** link.
20. In the **Connect to a Project** dialog box, expand the **https://**_**youraccount**_**.visualstudio.com** node, expand **MyApp**, click **$/MyApp**, and then click **Connect**.
21. In the **Team Explorer** window, under **Solutions**, click **New**.
22. In the **New Project** dialog box, in the left pane, expand the **Installed** node, expand the  **Visual C#** node, click the **Web** node, and then select **ASP.NET Web Application (.NET Framework)** from the list of templates.
23. In the **Name** text box, type **MyTFSWebApp**, and then click **OK**.
24. In the **New ASP.NET Web Application - MyTFSWebApp** dialog box, select **Web API**, and then click **OK**.
25. In **Solution Explorer**, right-click the **Solution &#39;MyTFSWebApp&#39;** node, and click **Check In**.
26. In **Team Explorer**, click **Check In**.
27. If you are prompted by a **Check-in Confirmation** dialog box, click **Yes**. Wait for the check in to complete.
28. Under **Folders**, Expand **MyApp** and right-click **MyTFSWebApp**, point to **Branching and Merging**, and then click **Convert to Branch**.
29. In the **Convert Folder to Branch -MyTFSWebApp** page click **Convert**.
30. Return to the browser window and go to **https://portal.azure.com**.
31. If a page appears, prompting for your email address, type your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.

32. If the **Windows Azure Tour** dialog box appears, click close (the **X** button).
33. In the navigation blade, click **App Services**, click **Add**, click **Web App**, and then click **Create**.
34. In the **App name** text box, enter a unique name.
35. Click **App service plan/Location**, click **Create new**.
36. From the **Location** drop-down list, select the region that is closest to your location.
37. In the **App Service plan** input, enter **MyAppService**, and then click **OK**.
38. Click **Create**. Wait for the web app to be created. Click the newly created web app.
39. In the newly created web app blade, in the **Deployment** section, click **Continuous Delivery(Preview)**.
40. In the **Continuous Delivery(Preview)blade**, click **Configure**.
41. Click **Source Code** and fill the following values:
    - Code repository: **Visual Studio Team Service**
    - Visual Studio Team Service Account: _**youraccount**_
    - Project: **MyApp**
    - Repository: **$/MyApp**
    - Branch: **$/MyApp/MyTFSWebApp**
42. Click **OK**
43. Click **Build** and in the **Web application framework** drop-down list, select **ASP.NET**, and then click **OK**.
44. Click **OK** and wait for the continuous delivery configuration to finish.
45. A new build will automatically start and should take about two minutes. 
46. After two minutes, click **Refresh logs**, you should see **Successfully setup Continuous Delivery and triggered build**. If you don't wait another minute and refresh the logs again.
47. Open a browser tab and navigate to the web app at the url: **http://[yourwebapp].azurewebsites.net/api/values** and you should see two values in a form of XML or JSON.

 	>**Note :** Replace **yourwebapp** variable with your web app name.
	
48. Return to Visual Studio 2017, and on the **View** menu, click **Solution Explorer**.
49. In **Solution Explorer**, expand the **Controllers** folder, right-click the **ValuesController.cs** file, and then select **Check Out for Edit**.
50. In the **Check Out** dialog box, click **Check Out**.
51. In **Solution Explorer**, double-click **ValuesController.cs**, locate the **Get** method that is without parameters, and replace the  **return** line of the method with the following code:

  ```cs
		return new string[] { "value1", "value2", "value3" };
```
52. To save the file, press Ctrl+S.
53. In **Solution Explorer**, right-click **ValuesController.cs**, and then click **Check In Pending Changes**.
54. In **Team Explorer - Pending Changes**, click **Check In**.
55. If you are prompted by a **Check-in Confirmation** dialog box, click **Yes**. Wait for the check-in to complete.
56. In **Team Explorer**, click the **Pending Changes | MyApp** title, and then from the drop-down list, click **Builds**.
57. Double-click the build definition that appears under the **My Builds** group.
58. A browser will open in the MSTS build page where you can see the progress of the build.
59. When the build is done, return to the Azure portal, click **Refresh logs** and review the last deployment.
60. Open a browser tab and navigate to the web app at the url: **http://[yourwebapp].azurewebsites.net/api/values** and you should see three values in a form of XML or JSON. (replace yourwebapp with your WebApp's name)
	>**Note :** Replace the **yourwebapp** variable with your web app name.
61. Close all open windows.


# Lesson 6: Best Practices for Production Deployment

### Demonstration: Transforming Web.config Files

#### Demonstration Steps

1. Open **Visual Studio 2017**.
2. On the **File** menu, point to **New**, and then click **Project**.
3. In the **New Project** dialog box, in the navigation pane, expand the **Installed** node, expand the **Visual C#** node, click the **Web** node, and then select **ASP.NET Web Application (.NET Framework)** from the list of templates.
4. In the **Name** text box, type **WebConfigTransformations**.
5. In the **Location** text box, type **[repository root]\Allfiles\20487C\Mod08\DemoFiles\WebConfigTransformations\begin**.
6. Clear the **Create directory for solution** check box, and then click **OK**.
7. In the **New ASP.NET Web Application - WebConfigTransformations** dialog box, select **Web API**, and then click **OK**.
8. Right-click the **WebConfigTransformations** project and click **Manage NuGet Packages**.
9. In the **NuGet: WebConfigTransformations** tab, click **Browse**.
10. In the **Search** text box, enter **EntityFramework**.
11. From the search results, click **EntityFramework**, and then click **Install**.
12. In the **Preview Changes** modal, click **OK**.
13. In the **License Acceptance** modal, click **I Accept**.
14. In **Solution Explorer**, double-click **Web.config**.
15. Add a connnection string:
   ```
	xml
    <connectionStrings>
        <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=MyAppDB;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\db.mdf">
    </connectionStrings>
``` 
16. Under the **system.web** configuration group, review the **compilation** configuration section. Notice the **debug** attribute.
17. In **Solution Explorer**, expand **Web.config**, and then double-click **Web.Release.config**.
18. Under the **system.web** configuration group, review the **compilation** configuration section. Notice the **RemoveAttribute**  transform attribute.
19. In the **configuration** root element, add the following **connectionStrings** section:

  ```cs
		<connectionStrings>
          <add name="DefaultConnection"
             connectionString="Data Source=ProductionSQLServer;Initial Catalog=MyAppProductionDB;Integrated Security=True"
          xdt:Transform="SetAttributes" xdt:Locator="Match(name)"/>
        </connectionStrings>
```
20. On the **File** menu, click **Save Web.Release.config**.
21. In **Solution Explorer**, right-click the **WebConfigTransformations** project, and then click **Publish**.
22. In the **Publish Web** dialog box, in the **publish profile** drop-down list, select **New**.
23. In the **New Profile** dialog box, type **Production**, and then click **OK**.
24. Verify that the selected publish method is **Web Deploy**, and set the rest of the fields to the following values:
    - Server: **localhost**
    - Site Name: **Default Web Site/MyProductionApp**
    - Destination URL: **http://localhost/MyProductionApp**
25. Click **Validate Connection**, and wait for the green check mark to appear.
26. Click **Next**, from the **Configuration** drop-down list, select **Release** and then click **Save**, after that it automatically publish and the browser opens.If not, click **Publish**
27. Close the browser and return to Visual Studio 2017.
28. On the **File** menu, point to **Open**, and then click **Web Site**.
29. In the navigation pane, click **Local IIS**.
30. In the websites tree, expand the **IIS Sites** node, expand the **Default Web Site** node, select the **MyProductionApp** node, and then click **Open**.
31. In **Solution Explorer**, expand the **http://localhost/MyProductionApp** website, and double-click the **Web.config** file.
32. In the **&lt;connectionStrings&gt;** section, review the **DefaultConnection** connection string. Note that it now uses the  **ProductionSQLServer** database.
33. Show students the **&lt;compilation&gt;** configuration section under the **&lt;system.web&gt;** configuration group. Point out the missing **debug** attribute.
34. Close all open windows.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
