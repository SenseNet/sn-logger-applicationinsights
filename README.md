# Azure Application Insights logger for sensenet
This is an implementation of the `IEventLogger` interface found in [SenseNet.Tools](https://github.com/SenseNet/sn-tools). It will send event log messages (for example errors, system startup events and custom application logs) to [Azure Application Insights](https://docs.microsoft.com/en-us/azure/application-insights/) where operators can monitor, filter or archive log entries.

## Installation
To install the **Application Insights** feature in your application please follow the link above to the official documentation. It will involve registering your application on the Azure portal and acquiring the necessary connection string for the resource.

To get the logging provider for sensenet please download the following package from NuGet:

[![NuGet](https://img.shields.io/nuget/v/SenseNet.Logging.ApplicationInsights.svg)](https://www.nuget.org/packages/SenseNet.Logging.ApplicationInsights)

### Configuration
In your project's application class configure the logging provider for sensenet:

```csharp
protected override void BuildRepository(IRepositoryBuilder repositoryBuilder)
{
    base.BuildRepository(repositoryBuilder);

    repositoryBuilder.UseLogger(new ApplicationInsightsLogger());
}
```
After this you should be able to monitor sensenet events either locally in Visual Studio or online on the Azure portal.