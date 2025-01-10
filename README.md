# ChatU.AI.SDK

![Nuget](https://img.shields.io/nuget/v/ChatU.AI.SDK)


## SDK

If you use SDK, you can install it via NuGet: `ChatU.AI.SDK`
Or execute the command

```ps
Install-Package ChatU.AI.SDK -Version 1.2.4
```
```ps
dotnet add package ChatU.AI.SDK --version 1.2.4
```

## Release Notes

- [x] Support .NET 8.0 & .NET 9.0
- [x] Image generation interface
- [x] Text generation interface
- [x] Streaming text generation interface
- [x] Allow all background configuration models
- [x] Allow call assistant


## Demo

ChatUAISDK.Console is a command line example

## How to run a demo 

Modify the configuration in the Program.cs file of ChatUAISDK.Console

```csharp

var accessToken = "Your AccessToken"; // Or fill in directly during operation

```

After that, you can run it.

```
AccessToken can be obtained from https://admin.chatu.pro (Global version:https://admin.chatu.ai)
```



![chatu-net-sdk-demo](https://user-images.githubusercontent.com/274085/234198322-3b042329-1ad8-4450-9595-3cde9864962b.gif)
