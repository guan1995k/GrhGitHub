【Demo功能】

1、Demo介绍SDK初始化、登陆设备、登出设备、自动重连、打开监视预览、关闭监视、抓图、保存码流、关闭保存码流、PTZ控制功能。
2、Demo演示了在预览前可选择通道、码流类型，在窗口标题有设备连接状态，PTZ控制包括方向控制，可更改步长，缩放、焦距、光圈等部分功能。


【注意事项】
1、编译环境为VS2010，NETSDKCS库最低只支持.NET Framework 4.0,如用户需要支持低于4.0的版本需要更改NetSDK.cs文件中使用到IntPtr.Add的方法,我们不提供修改。
2、此Demo只支持单设备单通道预览。
3、此Demo不支持多设备登陆。
4、抓图没响应问题，可能设备不支持，如：智能交通设备有专用的抓图接口，不支持普通抓图;NVR需要正确配置才能抓图，取决于NVR链接的IPC是否支持抓图。
5、开始保存，保存的录像是大华视频格式录像，只能用大华播放SDK或播放器播发。
6、运行前请把"\General_NetSDK_Chn_Win32_IS_V3.XX.X.R.XXXXXX\库文件\"里所有的DLL文件复制到"\General_NetSDK_Chn_Win32_IS_V3.XX.X.R.XXXXXX\演示程序\CSharpDemo\RealPlayAndPTZ\RealPlayAndPTZDemo\bin\Release\"目录中，或"\General_NetSDK_Chn_Win64_IS_V3.XX.X.R.XXXXXX\库文件\"里所有的DLL文件复制到"\General_NetSDK_Chn_Win64_IS_V3.XX.X.R.XXXXXX\演示程\CSharpDemo\RealPlayAndPTZ\RealPlayAndPTZDemo\bin\x64\Release\"目录中, 不要有遗漏DLL文件，以防启动程序时提示找不到依赖的库文件或运行出现问题。
7、如把库文件放入程序生成的目录中，运行有问题，请到大华官网下载最新的网络SDK版本：http://www.dahuatech.com/index.php/service/downloadlists/836.html 替换程序中的库文件。

【Demo Features】
1、Demo SDK initialization,login device,logout device,auto reconnect device,start real,stop real, capture,save stream to file,stop save stream to file,PTZ control function.
2、Demo can select channel and stream type before realplay,connect status show in winodw title,PTZ control include direction,select step len,contorl zoom, focus, aperture.


【NOTE】
1、Complier for NetSDKCS project and Demo is VS2010,and target framework for NetSDKCS project is .NET Framework 4.Modify the code about IntPtr.Add method in the file "NetSDK.cs"，we don not support to modify it.
2、Just only support one device and one channel to realplay.
3、Not support multi-device to login.
4、Maybe the device is not support capture picture if did not respond.such as ITC device is not support and have a specical interface function to capture picture;NVR device need to config if you want cupture picture,whether the connected IPC device support caputre.
5、Start save function is saved DH-Video format video,must use DH player or DH playSDK funtion to play video.
6、Copy All DLL files in the directory "\General_NetSDK_Eng_Win32_IS_V3.XX.X.R.XXXXXX\bin\" into the directory "\General_NetSDK_Eng_Win32_IS_V3.XX.X.R.XXXXXX\demo\CSharpDemo\RealPlayAndPTZ\RealPlayAndPTZDemo\bin\Release\", or in the directory "\General_NetSDK_Eng_Win64_IS_V3.XX.X.R.XXXXXX\bin\" into the directory "\General_NetSDK_Eng_Win64_IS_V3.XX.X.R.XXXXXX\demo\CSharpDemo\RealPlayAndPTZ\RealPlayAndPTZDemo\bin\x64\Release\"  before running. To avoid prompting to cannot find the dependent DLL files when the program start, or running with some problems.
7、If run the program with some problems,please go to 
http://www.dahuasecurity.com/download_3.html download the newest version,and replace the DLL files.