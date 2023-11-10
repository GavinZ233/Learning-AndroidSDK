  
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

### 1. Android

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




### 2. Player
|名称|说明|作用|
|--|--|--
|Company Name|公司名称|
|Product Name|项目名称|
|Version|版本号|
|Default Icon|默认Icon|
|Default Cursor|默认光标|替换游戏内光标
|Cursor Hotspot|光标热点|光标实际触发点相对图片的坐标

### 3. Icon
|名称|说明|作用|
|--|--|--
|Adaptive(API 26)|Android8.0及以上版本的图标|在老版本的基础上加了背景图
|Round(API 26)|Android7.1及以上版本的图标|
|Legacy|Android7.1以下版本的图标|


### 4. Resolution and Presentation
|名称|说明|作用|
|--|--|--
|Fullscreen Mode|全屏模式|`Fullscreen Window`保持纵横比铺满屏幕，可能会有黑边，`Windowed`窗口模式，自定义分辨率
|Resizable Window|可调整窗口大小|可在多窗口模式调整大小
|`Hide Navigation Bar`|隐藏导航栏|隐藏顶部的系统导航信息，如：信号，电量等
|`Render outside safe area`|渲染超出安全范围的屏幕|刘海屏的刘海区域也会被渲染
|Optimized Frame Pacing|均匀分布帧|启用后会更流畅
|Resolution Scaling Mode|分辨率缩放模式|有UI自适应不需要开启，`Fixed DPI`使用API应用分辨率，可优化性能和电池寿命
|Reset resolution on window resize|重新设置分辨率|当开启分辨率缩放模式时，开启此选项，重新计算分辨率
|Blit Type|光高类型|`Always`使用Blit渲染到设备的帧缓冲区，兼容性高速度慢  `Nerver`直接渲染到操作系统的帧缓冲区，兼容性低速度快 `Auto` 当直接渲染失败时使用Blit
|`Aspect Ratio Mode`|应用程序支持的最大纵横比|超出纵横比时用黑条填充，`Legacy Wide Screen`传统宽屏宽高比 `Native Aspect Ratio` 本机宽高比 `Custom`自定义纵横比，在`Up To`设置比例
|`Default Orientation`|默认屏幕取向|`Portrait` 竖屏 `Portrait Upside Down` 竖屏反向 `Landscape Right` 右侧横屏，设备底部是程序右侧 `Landscape Left` 左横屏 `Auto Rotation` 自动旋转，会多出四个方向的选择
|Use 32-Bit Display Buffer|显示缓冲区保存32位颜色值|如果后处理出现条带效果或者需要Alpha值，启用32位颜色
|Disable Depth and Stencil|深度和模板缓冲区|
|Render Over Native UI|使用设备本机的UI渲染|一般不开启
|Show Loading Indicator|显示加载指示器|一般采用自己写的进度条


### 5. Splash Image
|名称|说明|作用|
|--|--|--
|Virtual Really Splash Image|VR的初始屏幕图片|
|Show Splash Screen|启动展示UnityLogo|付费版可以关闭UnityLogo
|Splash Style|Logo风格|黑白两种选择
|Animation|Logo动画|静态，动态，定制三种
|Draw Mode|Logo绘制模式|Unity一直在下方和按顺序展示两种
|Overlay Opacity|覆盖不透明度|
|Background Color|背景颜色|
|Blur Background Image|模糊背景图像|
|Background Image|背景图片|
|Alternate Portrait Image|备选图像|
|Static Splash Image|静态启动图像|动态Logo关闭了才能看见，付费版


### 6. OtherSettings-渲染相关
|名称|说明|作用|
|--|--|--
|||
|||
|||

### 7. OtherSettings-配置相关
|名称|说明|作用|
|--|--|--
|||
|||
|||

### 8. OtherSettings-其他
|名称|说明|作用|
|--|--|--
|||
|||
|||


Package Name 按照 `com.CompanyName.ProduckName`填写      
Minimum APILevl 最低版本选择较低的版本，好兼容更多手机


### 9. Publishing Settings
|名称|说明|作用|
|--|--|--
|||
|||
|||







