# Module 10: Monitoring and Diagnostics

Wherever  you see a path to a file that starts with *[repository root]*, replace it with the absolute path to the folder in which the 20487 repository resides. For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, the following path: **[repository root]\AllFiles\20487C\Mod06** should be changed to **C:\Users\John Doe\Downloads\20487\AllFiles\20487C\Mod06**.

# Lesson 2: Configuring Service Diagnostics

### Demonstration: Tracing WCF Services

#### Demonstration Steps

1. Open **Visual Studio 2017**.
2. On the **File** menu, point to **Open**, and then click **Project/Solution**.
3. Go to **[repository root]\Allfiles\20487C\Mod10\DemoFiles\TracingWCFServices\CalcService**.
4. Select the **CalcService.sln** file, and then click **Open**.
5. In **Solution Explorer**, under the **CalcService** project, right-click **Web.config**, and then click **Edit WCF Configuration**.
6. In the **Configuration** pane, click **Diagnostics**.
7. Click **Enable Tracing**.
8. Click **Enable MessageLogging**.
9. In the **Configuration** pane, expand the **Diagnostics** node, and then click the **Message Logging** node.
10. In the pane on the right, locate the **LogEntireMessage** property in the property grid. If it is set to **False**, set it to **True**.
11. On the **File** menu, click **Save**.
12. On the **File** menu, click **Exit**.
13. Return to Visual Studio 2017 and in **Solution Explorer**, under the **CalcService** project, right-click **SimpleCalculator.svc**, and then click **Set As Start Page**.
14. Press Ctrl+F5. The WCF Test Client window will open.
15. In the pane to the left, double-click the **Div** node.
16. Set the **a** value to **4** and the **b** value to **2** , and click **Invoke**. If a **Security Warning** dialog box appears, click **OK**.
17. Change the **b** value to **0**, and click **Invoke**. If a **Security Warning** dialog box appears, click **OK**. A dialog box with an exception will be shown.
18. Click **Close** and then close the WCF Test Client window.
19. Open **File Explorer** and go to **[repository root]\Allfiles\20487C\Mod10\DemoFiles\TracingWCFServices\CalcService\CalcService**.
20. Double-click **web_tracelog.svclog**. This will open the Microsoft Service Trace Viewer application.
21. On the **File** menu, click **Add**.  
22. In the **Open** dialog box, go to **[repository root]\Allfiles\20487C\Mod10\DemoFiles\TracingWCFServices\CalcService\CalcService, click web_messages.svclog**, and then click **Open**.
23. To view the trace logs, click the **Activity** tab.
24. Click the line in red that reads **Process action http://tempuri.org/ICalc/Div**.
25. On the pane to the right, click the red line that starts with **Handling an exception**.  
26. On the **Formatted** tab, show the **Exception Information** group.
27. On the pane to the right, click the line that says **Message Log Trace**.
28. Click the **Message** tab.
29. Show the message content.
30. Close the Microsoft Service Trace Viewer window.
31. Close all open windows.

# Lesson 3: Monitoring Services Using Windows Azure Diagnostics

### Demonstration: Configuring Microsoft Azure Diagnostics

#### Demonstration Steps

1. Open **Visual Studio 2017**.
2. On the **File** menu, point to **Open** , and then click **Project/Solution**.
3. Go to **[repository root]\Allfiles\20487C\Mod10\DemoFiles\AzureDiagnostics\Begin\AzureDiagnostics**.
4. Select the **AzureDiagnostics.sln** file, and then click **Open**.
5. Right click the **DiagnosticsWebRole** project, and then click **Publish**.
6. Select **Microsoft Azure App Service**, select **Create New** and click **Publish**.
7. In the **App Name** text box, type **AzureDiagnosticsExample-**_yourinitials_ (*yourinitials* are your initials, e.g. John Doe -> jd)
8. In the **Resource Group** drop-down list, click **New..** and type **Mod10RC-**_yourinitials_. Then click **OK**.
9. Click **Create** and wait for the deployment to finish.
10. Open a browser and navigate to **http://portal.azure.com**.
11. If a page appears, prompting you to enter your email address, enter your email address, and then click **Continue**.
12. Wait for the **Sign In** page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to enter your credentials.

13. If the **Windows Azure Tour** dialog box appears, close it.
14. On the navigation menu on the left, click **App Services**.
15. In the **App Services** blade, click **AzureDiagnosticsExample**-_yourinitials_.
16. In the **AzureDiagnosticsExample-**_yourinitials_ blade, in the menu under **MONITORING**, click **Diagnostics logs**.
17. In the **Diagnostics logs** blade, turn on **Application Logging (Filesystem)**.
18. Set **Level** to **Verbose**.
19. Click **Save**.
20. In the blade menu, click **Overview**.
21. To open a new tab and explore the application, click the value in **URL**.
22. Go back to the portal and in the menu click **Log stream**.
23. Go back to the tab with the open application.
24. Write a message in the **Log Text** text box and click **Write to trace provider**.
25. Go back to the tab with the open portal and you should now see **PID[number] Verbose [your message]**.
26. Explain to the students that **Application Logging (Filesystem)** must be enabled for the log stream to work.
27. In the blade menu, click **Diagnostics logs** and turn off **Application Logging (Filesystem)**.
28. Turn on **Application Loggings (blob)** and change **Level** to **Verbose**.
29. Click **Storage Settings**.
30. In the **Storage accounts** blade, click **+ Storage account**.
31. In the **Name** text box, type **azdiagnostics***yourinitials* (replace *yourinitials* with your initials, e.g. John Doe -> jd), and then click **OK** and wait for the storage account to finish deploying.
32. In the **Storage accounts** blade, click the **azdiagnostics***yourinitials* account.
33. In the **Containers** blade, click **+ Container**.
34. In the **Name** text box, type **diagnostics-logs**.
35. In **Public access level**, choose **Private**, and then click **OK**.
36. Click the **diagnostics-logs** container and click **Select**.
37. In the **Diagnostics logs** blade, click **Save**.
38. In the menu bar, click **Log stream**, You should now get the following message: **Application logs are switched off. You can turn them on using the 'Diagnostic logs' settings.**
39. Go back to the tab with the application open.
40. Enter another message in the **Log Text** text box and click **Write to trace provider**.
41. Go back to the tab with the portal open.
42. On the portal menu on the left, click **Storage accounts**.
43. Click **azdiagnostics***yourinitials*, and then click **Blobs**.
44. In the **Blob service** blade, click the **diagnostics-logs** container.
45. In the **diagnostics-logs** blade, click **AzureDiagnosticsExample-_yourinitials_**.
46. Click **[current year]**, click **[current month]** and then click **[current day]**.
47. Click **[current hour]** and then click the most recently modified blob.
48. In the **Blob properties** blade, click **Download**.
49. Open the downloaded file in Excel or notepad.
50. One of the rows with level **Verbose** should have your message listed in the **message** column.
51. Close all open windows.

# Lesson 4: Collecting Windows Azure Metrics

### Demonstration: Viewing Web App Metrics

#### Demonstration Steps

1. Open a browser.
2. Go to **http://portal.azure.com**.
3. If a page appears, prompting you to enter your email address, enter your email address, and then click **Continue**.
4. Wait for the **Sign In** page to appear, enter your email address and password, and then click **Sign In**.

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to enter your credentials.

5. If the **Windows Azure Tour** dialog appears, close it.
6. In the navigation pane, click **App Services**, click **+ Add**, click **Web App** and then click **Create**.
7. Enter the following information:

    - App name: **metricsdemo***YourInitials* (Replace *YourInitials* with your initials).
    - App Service plan/Location: Click it, click **Create new**, and then provide the following details:
        - App Service plan: **metricsdemo***YourInitials*
        - Location: The closest place to your location
        - Pricing tier: **S1 Standard** select and click ok
    - Resource Group: Select **Create new** and then type **demo**.

8. Click **Create**, and wait for the web app to be created.
9. Close all the blades.
10. In the navigation menu, click  **Monitor**.
11. In the **Monitor** blade, click **Metrics (preview)**.
    > Note for future maintainer: when the **Metrics (preview)** has replaced the current non-preview **Metrics** service, remove all **(preview)** annotations.
12. In the **Metrics (preview)** blade, in the **RESOURCE** dropdown, select the **metricsdemo***YourInitials* app service.
13. In the **METRIC** drop-down list box, select **Http 2xx**.
14. Open **Visual Studio 2017**.
15. On the **File** menu, point to **Open**, and then click **Project/Solution**.
16. Go to **[repository root]\Allfiles\20487C\Mod10\DemoFiles\WebSiteMonitoring\SimpleWebApplication**.
17. Select the **SimpleWebApplication.sln** file, and then click **Open**.
18. In **Solution Explorer**, right click the **SimpleWebApplication** project, and then click **Publish**.
19. In the **Pick a publish target** page, select **Microsoft Azure App Service**, and then select **Select Existing**.
20. Click **Publish**.
21. In the **App Service** modal, expand the **demo** folder, and then select the **metricsdemoYourInitials** app service.
22. Click **OK** and wait for the publish operation to finish and for a browser to open.
23. In the browser, click **ClickHere** several times.
24. Go back to the Azure portal and wait for the graph to update. 
25. Verify that the number of **Http 2xx** has increased.
    > Note: It can take a minute or two for the graph to show the new requests
26. Close all open windows.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
