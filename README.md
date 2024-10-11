# PinewoodTechTest
Technical Test for Pinewood Technologies

NOTE: You MUST use an up-to-date Visual Studio 2022 to run the following as this allows for targeting .NET 8.

Before Running:

- Ensure the Port Number '5136' is allowed by your firewall and if not then either allow it or change it in both the UI project (Program.cs) and API project (launchsettings.json) to your preferred Port.

Running the UI and API at Once:

1. Open the Customers Solution
2. Right-Click the Solution and select 'Set Startup Projects'
3. Select 'Multiple startup projects'
4. Set the Action for the 'CustomersAPI' project to 'Start without debugging'
5. Set the Action for the 'CustomersUI' project to 'Start'
6. Apply
7. OK
8. Click 'Start'
