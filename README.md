  
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

>***挖坑***:目前出现了打包卡在`Building Gradle project`的问题，构建失败，大致为版本问题，自己布置时没有完全使用教程上的文件。      
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
|**Rendering**|
|`Color Space`|色彩空间|切换渲染计算的色彩空间，线性效果更好，但需要设备能够支持将线性转换到伽马输出
|`Auto Graphics API`|自动图形接口|勾选：尝试使用`Vulkan`，如果不支持会在下方询问使用的GLES3.2、GLES3.1、GLES3.0中做备选。不勾选：下方出现列表，可自定义图形优先级，其中GLES2.0过于老旧不支持线性渲染
|Color Gamut|色域|sRGB 色域是默认（也是必需的）色域。当定位具有宽色域显示的设备时，使用 DisplayP3 来利用完整的显示功能。一般的手机使用`sRGB`即可，如果是PC主机游戏，可以考虑添加`DisplayP3`
|`Multithreaded Rendering`|多线程渲染|将图形API调用从Unity的主线程移动到单独的工作线程。个别设备渲染出现问题，可通过关闭此选项做排查是否不支持。
|`Static Batching`|静态批处理|静态物体合批DrawCall
|`Dynamic Batching`|动态批处理|动态合批DrawCall，当`SRP`(URP\HDRP)处于活动状态时，动态批处理不起作用。使用`FairyGUI`时，需要开启，FairyGUI的DrawCall会得到优化
|`Compute Skinning`|计算蒙皮|启用 DX11/ES3 GPU 计算蒙皮，从而释放 CPU 资源。图形接口的版本要求较高，PC上`DX11`、移动设备`ES3`
|Graphics Jobs(Experimental)|图形作业|将渲染循环的图形任务移到线程，减少主线程的计算量
|`Texture compression format`|纹理压缩格式|在ASTC、ETC、ETC2中选择
|`Normal Map Encoding`|法线地图编码|有`XYZ`、`DXT5nm`两种,DXT5nm质量更高,解码成本也高。一般 XYZ 足够用
|`Lightmap Encoding`|光照贴图编码|三种不同质量，影响光照贴图的编码方案和压缩格式
|`Lightmap Streaming`|光照贴图流|对光照贴图使用Mipmap流式处理，没有远近视角的项目可以关闭。Streaming Priority：流式传输优先级，范围-128到127，默认0不用改
|Frame Timing Stats|帧时序统计|需要打开相机上的动态分辨率(AllowDynamicResolution)，来收集CPU和GPU帧时间统计信息，用来确定应用程序是受到CPU还是GPU限制。
|OpenGL:Profiler GPU Recorders|OpenGL的探查GPU记录器|
|Virtual Texturing(Experimental)|虚拟纹理|可在场景中具有很多高分辨率纹理时减少 GPU 内存使用量和纹理加载时间。它将纹理拆分为瓦片，然后在需要时将这些瓦片逐步上传到 GPU 内存中。虚拟纹理和`Android不兼容`
|Shader precision model|着色器精度模型|控制着色器中使用的采样器的默认精度
|360 Stereo Capture|360 度立体捕捉|Unity 是否可以捕获立体 360 度全景图像和视频。360 度立体捕捉与
|**Vulkan Settings**||**以下内容安卓不宜修改**
|SRGB Write Mode|SRGB 写入模式|允许呈现器在运行时切换 sRGB 写入模式，如果要暂时关闭线性到 sRGB 写入颜色转换，可以启用该选项。它会增加移动设备GPU的负担，产生负面影响，`移动端不宜开启`
|Number of swapchain buffers|交换链缓冲区数量|设为2位双缓冲，设为3为三重缓冲可以和Vulkan渲染器一起使用。该设置可以帮助解决移动平台上的延迟问题。注意：一般情况下我们不要修改此选项，保持为3，不要在安卓设备上使用双缓冲，会产生负面影响
|Acquire swapchain image late as possible|尽可能晚的获取交换链图像|启用后，Vulkan会延迟获取后缓冲器，直到它将帧渲染为屏幕外图像。Vulkan 使用暂存映像来实现此目的。启用此设置会导致在显示反向缓冲器时产生额外的光圈。此设置与`双缓冲`相结合，可以提高性能。但是，它也可能导致性能问题，因为额外的 blit 会占用带宽。`移动端不宜开启`
|Recycle command buffers|回收命令缓冲区|Unity 执行命令缓冲区后是回收还是释放命令缓冲区
|Apply display rotation during rendering|在渲染期间应用显示旋转|启用此选项可在显示的本机方向上执行所有渲染。这在许多设备上具有性能优势，但是使用它会带来一些限制，所以`移动端不宜开启`


#### 拓展知识

##### 1. 线性和伽马颜色空间            
![渐变](https://docs.unity.cn/cn/2021.3/uploads/Main/LinearLighting-LinearGradients.png)
>人眼对光强的反应不呈线性。我们在观察光时会发现一些亮度比另一些亮度更容易看到，即从黑到白的线性渐变在我们人眼中不是线性渐变的。                  
由于历史原因，监视器和显示器具有相同的特性。向监视器发送线性信号会导致看起来像上图右侧的渐变，人眼观察感觉是错误的。为了弥补这一点，需要发送经校正的信号来确保监视器能够呈现出看起来自然的图像。这种校正称为伽马校正。        
伽马和线性颜色空间同时存在的原因是，光照计算应该在线性空间中进行，以便确保数学上的正确性，但结果应该在伽马空间中呈现以便让人眼看起来正确。            
在帧缓冲格式限制为每通道 8 位的旧硬件上，计算光照时使用伽马曲线可在人类可感知的范围内提供更高的精度。在人眼最敏感的范围内，使用的位数最多。           
即使当今的监视器是数字显示器，它们仍然采用伽马编码信号作为输入信号。图像文件和视频文件显式编码到伽马空间（这意味着它们带有伽马编码值，而不是线性强度）。这便是标准；一切数值都在伽马空间内。                

简言之：计算机计算色彩采用线性能够保证准确性，输出到显示器时采用伽马可以适应人眼的色彩感知更符合人的视觉。但伽马因为适应人眼的算法会导致数据失去精准度，使用伽马计算会出现数据偏差，最好是线性计算，将线性数据传到伽马缓冲区输出伽马数据到显示器显示。          

线性渲染的效果更好，但并非所有都支持。          
在`Android`上，线性渲染需要`OpenGL ES3.0`和`Android4.3`以上           
在`IOS`上，线性渲染需要`Metal`以上          
在`WebGl`上，线性渲染至少需要`WebGl 2.0`以上                 
以上情况不满足时，会因为不能将线性数据传输到伽马缓冲区，导致程序闪退。   

##### 2. 图形API            

|名称|平台|说明|
|--|--|--
|OpenGL（Open Graphics Library）          |Windows,macOS|开放图形库，它定义了一个跨平台、跨语言的编程接口规格的专业图形程序接口，可以用于3D、2D图形渲染，是一个功能强大、调用方便的底层图形库。
|Directx（Direct eXtension）              |Window|它是由微软公司创建的多媒体编程接口。不跨平台，只针对微软的相关产品，被广泛使用于Windows操作系统、xBox游戏主机的图形应用程序开发中。
|OpenGL ES（OpenGL for Embedded Systems） |Android,iOS|嵌入式系统的开放图形库，它是OpenGL的子级，主要针对手机、游戏主机等嵌入式设备而设计，免授权费、跨平台、功能完善。
|Metal                                    |iOS|苹果公司为游戏开发者提供的图形技术，该技术能够为3D图像提高10倍渲染性能，不支持跨平台，主要针对IOS、macOS苹果自家的操作系统。
|WebGL（Web Graphics Library）            |Web|针对Web端的3D绘图协议，这个标准允许把JavaScript和OpenGL ES 2.0结合在一起，网页开发人员可以借助系统显卡在浏览器里流畅的展示3D场景和模型，可以在网页里进行3D图形开发。
|Vulkan                                   |Windows,macOSm,Android,iOS|它有一套最新的图形加速API接口，是与DX12能够匹敌的GPU API标准，目标是提供更灵活和丰富的底层操作接口，以替代OpenGL 和 OpenGL ES接口，可以把Vulkan看做是OpenGL的升级版，目前新版本的Unity支持使用Vulkan方案。

##### 3. 图片压缩格式   

|格式|描述|支持
|--|--|--|
|ASTC|最灵活的格式，允许不同的块大小微调文件大小和质量，性能比ETC2略优|可运行`Vulkan`或者`GLES3.1`及以上的设备，GLES3.0部分支持
|ETC 2|GLES 3.0的标准纹理压缩格式。产生的图像质量很高，支持一到四分量纹理数据。|GLES3.0
|ETC|基于块的纹理压缩格式，图像被分为4x4的块，并且每个块使用固定位数进行编码。缺点是`没有直接的Alpha通道支持`，因此不适合具有透明度信息的纹理。Unity提供里一种自动处理纹理的方式，这种方法将Alpha通道放在纹理图集内。|所有Android
|RGBA 16|没有Alpha通道的未压缩格式|所有设备
|RGBA 32|有Alpha通道的未压缩格式，是16位的两倍大小|所有设备

>`ETC`中支持透明通道的方法：在导入精灵图集时，为纹理弃用特定于Android的`SplitAlphaChannel`选项。Unity会将生成的图集拆分为两个纹理，其中一个纹理有RGB，另一个有A通道，然后在渲染管线的最后部分将两个纹理组合起来


### 7. OtherSettings-配置相关
|名称|说明|作用|
|--|--|--
|||
|||
|||

### 8. OtherSettings-脚本相关
|名称|说明|作用|
|--|--|--
|||
|||
|||


Package Name 按照 `com.CompanyName.ProduckName`填写      
Minimum APILevl 最低版本选择较低的版本，好兼容更多手机


### 9. OtherSettings-优化相关
|名称|说明|作用|
|--|--|--
|||
|||
|||


### 10. Publishing Settings
|名称|说明|作用|
|--|--|--
|||
|||
|||







