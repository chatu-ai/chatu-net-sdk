# ChatU.AI.SDK

![Nuget](https://img.shields.io/nuget/v/ChatU.AI.SDK)


## SDK

您如果使用 SDK 则通过 NuGet 进行安装：`ChatU.AI.SDK`

或通过命令执行
```ps
Install-Package ChatU.AI.SDK -Version 1.1.2
```
```ps
dotnet add package ChatU.AI.SDK --version 1.1.2
```

## Release Notes

- [x] 图片生成接口
- [x] 文本生成接口
- [x] 流式文本生成接口
- [x] 支持 GPT-4
- [x] 支持 GPT-3.5


## Demo

ChatUAISDK.Console 是命令行示例

## 使用方法

修改 ChatUAISDK.Console  的 Program.cs 文件中的配置

```csharp

var accessToken = "你的AccessToken"; // 或在运行中直接填写

```

之后运行即可。

```
AccessToken 请于 https://admin.chatu.pro 获取
```

![chatu-net-sdk-demo](https://user-images.githubusercontent.com/274085/234198322-3b042329-1ad8-4450-9595-3cde9864962b.gif)
