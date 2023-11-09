  
Unity版本：2021.3.11f1      

# 1. 发布相关

## 1.1 旧版发布准备（2019以下）

Unity2019版本在编辑器模块可以一键添加。         
以下是2019以下版本手动添加SDK和NDK的流程         

1. Android构建支持工具安装（Unity提供下载,安装时需要关闭Unity）          

2. JDK（Java开发工具包）（网络下载） JDK 必须是版本 8            
      https://www.oracle.com/java/technologies/downloads/#jdk18-windows         

3. NDK(本地开发工具包)（网络下载）NDK 版本必须是 Unity2018——r16b, Unity2017——r13d            
注意：NDK是可选的工具，如果之后要发il2cpp的应用程序，那么NDK是必须的，如果是Mono可以不使用它          
https://blog.csdn.net/momo0853/article/details/73898066           
https://developer.android.google.cn/ndk/downloads/#lts-downloads      

4. Android SDK（安卓软件开发工具包）（网络下载或通过AndroidStudio下载）          
建议通过AndroidStudio下载     
https://developer.android.com/studio      
安装AndroidStudio后 通过它来安装AndroidSDK        
在`More Actions`——>`SDK Manager`——>`SDK Tools`勾选`Show Package Details`可以选择详细的Tools，多选择不同版本可以避免不兼容的情况


5. Java环境变量配置      
我的电脑——>系统属性——>查找——>系统环境变量——>环境变量          
5-1:`JAVA_HOME`: 变量值为JDK在你电脑上的安装路径        
安装好后可以利用%JAVA_HOME%作为JDK安装目录的统一引用路径       
5-2:Path: 编辑Path属性，在原变量后追加  `%JAVA_HOME%\bin;%JAVA_HOME%\jre\bin `        
5-3:CLASSPATH: 变量值为 `.;%JAVA_HOME%\lib\dt.jar;%JAVA_HOME%\lib\tools.jar`    


6. Unity设置        
Preferences——>External Tools——>Android          
设置SDK、JDK、NDK的路径

>目前出现了打包卡在`Building Gradle project`的问题，构建失败，大致为版本问题，自己布置时没有完全使用教程上的文件。      
深入了解再解决

## 1.2 发布设置

### Android

|名称|说明|作用|
|--|--|--
|`Texture Compression`|纹理压缩|和Texture里的压缩设置一样，常用`ETC2`支持透明通道，老设备不支持2，需要用ETC
|`ETC2 fallback`|ETC2回退的选择|ETC2失败了，可以选择的格式，32位相较于16位质量高内存多,默认`32`
|`Excport Project`|导出Gradle项目而不是APK|可以导入到AndroidStudio接SDK
|Symlink Sources|符号链接源|当Excport Project勾选才能选择，导出项目对之前的Java修改会保留，适合二次开发
|Build App Bundle(GooglePlay)|构建应用捆绑包|不导出APK而是AAB（适合安卓app里面加一部分3D效果的app项目，不适合游戏）
|`Create symbols.zip`|（调试）创建符号压缩文件|`Disabled`:不生成。`Public`:生成一个公共符号为程序打包,公共符号文件包含将函数地址解析为人类刻度字符串的信息，包较小。`Debugging`:生成一个调试符号为程序打包，包含完整的调试信息和符号表，可以用来解析对战、将本机调试器附加到应用程序调试代码。
|`Run Device`|（调试）运行的设备|选择连接的调试设备，连接后点击`Refresh`刷新
|`Build to Device`|（调试）构建到设备|不完整创建，而是将修改的部分文件部署到设备上，便于调试
|`Development Build`|（调试）开发模式构建|是否包含脚本调试符号和性能分析器到项目中
|Autoconnect Profiler|（调试）自动连接分析器|是否自动将分析器连接到生成的应用程序
|Deep Profiling|（调试）深度剖析|分析器监测每个函数调用，分会详细数据，会降低脚本执行速度
|Script Debugging|（调试）脚本调试|开启后有`Wait For Managed Debugger`等待托管调试器，可以`打断点`
|Compression Method|压缩方法| 常用`LZ4`，`Default`：使用ZIP压缩，效果好但解压慢，`LZ4HC`:LZ4高压缩版本，压缩更慢解压更快
|Max Texture Size|(新版本)最大纹理大小|
|Texture COmpression|(新版本)纹理压缩|




### Player
|名称|说明|作用|
|--|--|--
|Company Name|公司名称|
|Product Name|项目名称|
|Version|版本号|
|Default Icon|默认Icon|
|Default Cursor|默认光标|替换游戏内光标
|Cursor Hotspot|光标热点|光标实际触发点相对图片的坐标

### Other Settings

Package Name 按照 `com.CompanyName.ProduckName`填写      
Minimum APILevl 最低版本选择较低的版本，好兼容更多手机









