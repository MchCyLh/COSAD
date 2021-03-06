# 现代操作系统应用开发-期中PROJECT

标签（空格分隔）： 客户端

---

## 客户端类的使用说明

### Client类方法：

#### 1. Connect 方法，与服务器进行连接。

```C#
    Client client = new Client();
    string remoteHost = "127.0.0.1";
    int port = 8888;
    IPAddress remoteIPAddress = IPAddress.Parse(remoteHost);
    client.Connect(remoteIPAddress, port);
```

#### 2. Register 方法，账户注册。

```C#
    string username = "mchcylh";
    string password = "mchcylh";
    RegisterInfo registerInfo = new RegisterInfo(username, password);
    client.Register(registerInfo);
```
其中RegisterInfo类用于封装注册信息，作为Register方法的参数。

#### 3. Login 方法，账户登录。

```C#
    LoginInfo loginInfo = new LoginInfo(username, password);
    client.Login(loginInfo);
```

同理，Loginfo类用于封装登录信息，作为Login方法的参数。

#### 4. Publish 方法， 发布信息。

```C#
    HelpInfo helpInfo = new HelpInfo();
    helpInfo.Title = "title";
    Bitmap bitmap = new Bitmap(@"E:\Memory\picture\野良神\wow.jpeg");
    helpInfo.Photo = bitmap;
    helpInfo.Time = new DateTime(2015, 1, 16);
    helpInfo.Place = "place";
    helpInfo.More = "more!!!";
    helpInfo.ContactWay = "contactway";
    helpInfo.Contacter = "contacter";
    string publisher = "publisher";
    DateTime publishtime = DateTime.Now;
    PublishInfo publishInfo = new PublishInfo(helpInfo, publisher, publishtime);
    client.Publish(publishInfo);
```

其中 HelpInfo类封装具体的事件信息，包括：标题，图片，地点，联系人，联系方式，更多等。另外，PublishInfo类除了保存HelpInfo的信息，还有发布人和发布时间。

####　5. GetPublish 方法， 获取发布的信息。

```C#
    client.GetPublish(new GetPublishOption());
```

其中GetPublishOption类用于对获取哪些信息进行选择，可根据发布人，发布标题进行选择，还可以限制信息返回的条数。

# PS: 客户端使用时直接将所有文件拷贝到应用中（除Main.cs）,辅助类(RegisterInfo等)在Entity文件夹的CommandObject.cs中。

## 第三方库

[Json.NET][1] ： 辅助指令解释
[NDatabase][2] ： 数据持久化存储

[1]: http://www.newtonsoft.com/json
[2]: https://ndatabase.codeplex.com/