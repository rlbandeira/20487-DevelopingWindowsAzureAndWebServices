# Module 6: Hosting Services

  Wherever  you see a path to a file that starts with *[repository root]*, replace it with the absolute path to the folder in which the 20487 repository resides.
  For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, the following path: **[repository root]\AllFiles\20487C\Mod06** should be changed to **C:\Users\John Doe\Downloads\20487\AllFiles\20487C\Mod06**.

# Lesson 1: Hosting Services On-Premises

### Demonstration: How to Host WCF Services in IIS

#### Preparation Steps

You need to activate Microsoft Internet Information Services (IIS) on your machine. The instructions below are for Windows 10 only.

1. To open the **Search** window,click the magnifying glass next to the Start menu.
2. Type **Turn windows features on or off**, and then press Enter.
3. Select the **Internet Information Services** once, a black square should appear within the check box.
4. Expand **Internet Information Services**, and then expand **World Wide Web Services**.
5. Select the **Application Development Features** check boxes.
6. Expand the **Common HTTP Features** and select all the check boxes. 
7. You will also need to activate WCF support before you can deploy WCF services to IIS. To do this:
8. In the **Windows Features window**, expand **.NET Framework 4.7 Advances Services**.
Expand **WCF Services**, and then select all the check boxes inside it (HTTP Activation, Message Queuing (MSMQ) Activation, and so on).
Click **Ok**, Windows proceeds to install the required components; when it is done, IIS will be running.

8. Verify that IIS works:  
	a) Open a browser.  
	b) Navigate to **http://localhost/**, an Internet Information Services page should appear.

#### Demonstration Steps

1. Click **Start** and in the Search box type **Visual Studio**. In the list of search results, right-click **Visual Studio 2017**, and then click **Run as administrator**, in **User Control** dislog box click **Yes**.
2. On the **File** menu, point to **New**, and then click **Project**.
3. In navigation pane of the **New Project** dialog box, expand the **Installed** node, and then expand the **Visual C#** node.
4. Click the **WCF** node, and then from the list of templates, select **WCF Service Application**.
5. In the **Name** text box, type **MyIISService**.
6. In the **Location** text box, type **[repository root]\Allfiles\20487C\Mod06\DemoFiles\WCF**.
7. Clear the **Create directory for solution** check box, and then click **OK**.
8. In Solution Explorer, under the **MyIISService** project, double-click **web.config**.

    >**Note**: The reason there is no &lt;services&gt; section in the &lt;system.serviceModel&gt; is because IIS automatically defines the base address according to the virtual directory where the services are hosted, and the default endpoint configuration of WCF therefore does not require any specific configuration for each endpoint.

9. In Solution Explorer, under the **MyIISService** project, right-click **Service1.svc**, and then click **View Markup**.

    >**Note**: When IIS receives a request addressed to the .svc file, it uses the value of the **Service** attribute from the file to know which service it needs to call.

10. In Solution Explorer, click the **MyIISService** project node.
11. To build and debug the application, press F5. After the application is built, a browser opens.
12. In opened browser window, locate the address bar. To the base address of the service, add the absolute path: **Service1.svc?wsdl**, and then press Enter. Review the **Web Services Description Language (WSDL)** file.

    >**Note**: Lesson 4, &quot;Consuming WCF Services&quot; in Module 5, "Creating WCF Services" of Course 20487 explains how to use WSDL to generate service proxies.

13. To stop the debugger, return to Visual Studio and press Shift+F5.
14. Publish the service to IIS. To do this:  
	a. In Solution Explorer, right-click the **MyIISService** project, and then click **Publish**. The **Publish** tab opens.  
	b. Select **IIS, FTP, etc**, and then click **Publish**. The **Publish** window opens.  
	c. In **Publish method**, select **Web Deploy**.  
	d. In **Server**, type **localhost**.  
	e. In **Site Name**, type **Default Web Site/MyIISService**.  
	f. In **Destination URL**, type **http://localhost/MyIISService/Service1.svc?wsdl**.  
	g. Click **Save**.  
	
    Visual Studio 2017 proceeds to build and deploy the service to IIS. When it's done, a browser automatically opens and navigates to the service WSDL.
    
15. Review the WCF service WSDL.
16. Close all open windows.

# Lesson 2: Hosting Services in Microsoft Azure

### Demonstration: Hosting in Microsoft Azure

#### Demonstration Steps

1.  Click **Start**, type **Visual Studio**. From the search results, right-click **Visual Studio 2017**, and then select **Run as administrator**, In User **Control dialog** box  click **Yes**.
2.  On the **File** menu, point to **New**, and then click **Project**.
3.  In the **New Project** dialog box, on the navigation pane, expand the  **Installed** node, expand the **Visual C\#** node, click the **Web** node, and then in the list of templates, click **ASP.NET Web Application(.NET Framework)**.
4.  In the **Name** text box, type **MyWebSite**.
5.  In the **Location** text box, type **[repository root]\Allfiles\20487C\Mod06\DemoFiles\DeployWebApp**.
6.  Clear the **Create directory for solution** check box, and then click **OK**.
7.  In the **New ASP.NET Web Application** dialog box, select **Web API**, and then click **OK**.
8.	Open **Microsoft Edge** and go to **https://portal.azure.com**.
9.	If a page appears asking for your email address, enter your email address, and then click **Next**. Wait for the sign in page to appear, enter your email address and password, and then click **Sign In**.

	>**Note :** If during sign in, a page appears asking you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials.

10.	In the left menu of the portal, click **App Services**.
11. In the action bar, click **Add**, select **Web APP**, and then at the bottom of the third screen, click **Create** .
12.	In the **App name** box, type the name **wawsdemo***YourInitials* (YourInitials contains your initials). This is a unique value, which when combined with the **.azurewebsites.net** suffix is used as the URL to your web app.
13. In the **Resource Group** section, select **Create new**, and then change the name of the Resource Group to **BlueYonder.Demo.06**.
12.	Click **App Service Plan/Location** and then click **Create new**.
13. In the **App Service Plan** box, type **BlueYonderDemo06**.
14.	In the **Location** drop-down list, select the region closest to your location.
15. In the **Pricing tier** section, select **D1 Shared**, and then click **OK**.
16. Click **Create** . The site is added to the Web Apps table and its status is set to **Creating**. 
17.	After the status changes to **Running**, in Visual Studio, on the **View** menu, click **Server Explorer**.
18.	In **Server Explorer**, right-click the **Azure** pane, and then click **Connect To Microsoft Azure Subscription**.
19.	On the login screen, if a page appears asking you to choose from a list of previously used accounts, select the account you previously used, enter your credentials, and then click **Sign in**.

    >**NOTE :**	Wait until the login process completes.
        You only need to perform this step once, to import your Microsoft Azure account settings to Visual Studio.
        Now **Visual Studio 2017** can display the list of Web Apps and Cloud Services to which you can deploy applications.

20.	In Visual Studio, in **Solution Explorer**, right-click **MyWebSite** project, and then click **Publish**.
21.	In the **Pick a publish target** dialog box, choose **App Services**, in the **Microsoft Azure App Service** view, select the **Select Existing** option, and then click **Publish**.
22.	In the **App Service** dialog box, expand **BlueYonder.Demo.06** folder, select **wawsdemoYourInitial**, and then click **OK**.
23. Click **Publish**. Visual Studio publishes the application according to the settings that are provided in the profile file. After deployment finishes, Visual Studio opens Internet Explorer and displays the web app. The deployment process is quick, because the process only copies the content of the application to an existing virtual machine and does not need to wait for a new virtual machine to be created.
24. Close all open windows.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
