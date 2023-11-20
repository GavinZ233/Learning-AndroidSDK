  
Unity版本：2021.3.11f1      

# 1. 发布相关

## 1.1 Unity旧版发布准备（2019以下）

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

## 1.2 Unity发布设置

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
|**Identification**|身份证明|
|`Override Default Package Name`|覆盖默认包名称|当公司名或游戏名不是英文时，需要在此处手动填写英文包名
|`Package Name`|软件包名|相当于应用程序的ID，用于在设备和安卓应用商店中的唯一标识。格式：com.公司名.产品名。只能包含字母数字和下划线，每段都必须以字母开头
|`Version`|版本|用于标识应用程序包的迭代,格式：1.3.4,用户可见
|Bundle Version Code|内部版本号|确定一个版本是否比另一个版本新，数字越大表示版本越新。该值为整数，比如每次发布新版本时可以让数字加一。用户不可见
|`Minimum API Level`|最低API级别|决定应用程序运行需要的最低的API级别，如果用户手机的SDK版本低于该设置，Android系统会阻止用户安装该应用程序
|Target API Level|目标API级别|主要用于通知Android系统，我们已经针对目标版本进行了测试，并且系统不应该通过启用任何兼容性行为，以保持你的应用与目标版本的向前兼容性。应用程序一样可以在较低版本上运行（取决于Minimum API Level）
|**Configuration**|配置相关|
|`Scripting Backend`|脚本后端|切换`Mono`和`IL2CPP`
|`Api Compatibility Level`|API兼容性级别| `.NET Standard 2.1`相对更小，具有完整的跨平台支持。 `.NET Framework`包含更多API支持，会生成更大的包。默认是2.1，如果使用C#高级功能遇到报错，尝试切换到.NET Framework
|`C++ Compiler Configuration`|C++编译器配置|`Debug`将关闭所有优化，让代码生成速度更快，但运行速度较慢，可以在这种模式下进行调试 `Release`会对代码进行优化，编译后的代码运行会更快，二进制文件大小更小，但编译时间更长 `Master`可实现所有可能的优化，从而压缩每一点可能的优化，发布时间比发布模式还要长，如果接受较长的发布时间，建议在发布`最终版本时使用Master模式`
|`Use incremental GC`|使用增量GC|启用后可以使用增量垃圾回收器，会`将GC分散到多个帧上执行`，避免在一帧中进行GC造成的卡顿
|Assembly Version Validation|程序集版本验证|Mono验证强名称程序集的类型。是对程序集进行强命名，为程序集创建唯一标识，可以防止程序集冲突
|`Mute Other Audio Sources`|其他音源静音|运行Unity应用程序时停止在后台运行的应用程序中的音频
|Target Architectures|目标体系结构|允许应用程序允许的CPU，Mono模式只能选择ARMv7，IL2CPP模式可以选择更多的模式
|Split APKs by target architecture|按目标架构拆分APK|启用后，可以为目标体系结构中选择的每个CPU架构创建单独的APK，这样在Google Play中为用户提供下载时，它会根据设备的情况下载对应版本的APK，可以让apk更小，用户下载更小的包。但是主要是针对`Google Play`，因此面向国内的产品几乎不会使用。
|Target Devices|目标设备|`All Devices`（所有设备）：允许apk在所有Android和Chrome OS设备上运行。`Phones，Tablets，and TV Devices Only`（手机、平板、电视设备）：允许apk在Android手机和平板电脑、电视上运行，但是不能在Chrome OS设备上运行。`Chrome OS Devices Only`（Chrome OS设备）：允许apk在Chrome OS设备上运行，但是不能在Android手机、平板、电视上运行
|Install Location|安装位置|`Automatic`：自动让操作系统决定，用户可以自己移动安装位置`Prefer External`：首选外部安装，如果可以，将应用程序安装到外部存储中（SD卡），如果不能，应用程序安装到手机存储空间中F`orce Internal`：强制内部，强制将应用程序安装到手机存储空间中，用户无法将应用程序移动到外部存储中安装
|`Internet Access`|互联网接入|选择是否始终将网络权限添加到Android清单（即使你没有使用任何网路API）`Auto`：仅当使用了网络API时才会添加互联网访问权限`Require`：使用添加互联网访问权限
|`Write Permission`|写入权限|是否启用对外部存储（SD卡）的写入访问权限，并向Android应用清单添加相应的权限`Internal`（内部）：仅授予对内部存储的写入权限`External`（外置SD卡）：启用对外部存储的写入权限
|Filter Touches When Obscured|遮挡时过滤触摸|启用后可以丢弃在另一个可见窗口覆盖Unity应用程序时收到的触摸（触屏事件），可以放置窃听劫持
|Sustained Performance Mode|持续性能模式|启用此选项可在较长的时间段内设置可预测且一致的设备性能级别，而无需进行热限制。启用此设置时，整体性能可能会降低
|Low Accuracy Location|低精度定位|启用后可改为低精度值与Android位置API配合使用,降低消耗
|Chrome OS Input Emulation|ChromeOS触摸板事件转触屏输入|默认行为是将鼠标和触摸板输入事件转为触屏输入事件。
|Android TV Compatibility|安卓电视兼容性|`Android Game`：启用后可将输出的apk标记为游戏而不是常规应用。`Android Gamepad Support Level`：安卓游戏输入板支持等级。输入方式`D-pad`是遥控器
|Warn about App Bundle size|警告应用程序包的大小|当应用程序包大小超过阈值时会受到警告。GooglePlay会限制包体大小
|`Active Input Handling`|活动输入处理|选择使用新旧哪种输入系统

### 8. OtherSettings-脚本相关
|名称|说明|作用|
|--|--|--
|`Scripting Define Symbols`|脚本定义符号|可以在此设置自定义编译标志。使用第三方内容有时会在此处添加脚本符号。比如：Lua热更需要添加`HOTFIX_ENABLE`,Lua代码中的某些逻辑会被启动
|Additional Compiler Arguments|其它编译器参数|向此列表添加条目以将其他参数传递给 Roslyn 编译器。对每个附加参数使用一个新条目。要创建新条目，请按“+”按钮。要删除条目，请按“-”按钮。添加完所有所需参数后，单击“应用”按钮以在将来的编译中包括其他参数。“还原”按钮将此列表重置为最近应用的状态。
|Suppress Common Warnings|禁止显示常见警告|以下两种: CS0169：从不使用私有字段，声明了私有变量，但是从没有使用。CS0649：编译器检测到从未分配值的未初始化的私有或内部字段声明
|`Allow 'unsafe' Code`|允许使用“不安全”代码|启用对在预定义程序集中编译“unsafe”C# 代码的支持
|Use Deterministic Compilation|使用确定性编译|启用此设置后，编译的程序集在每次编译时都是完全相同的。禁用此设置可防止使用 -确定性 C# 标志进行编译。确定性编译可用于确定二进制文件是否从受信任的源编译
|Enable Roslyn Analyzers|启用Roslyn分析器|禁用此设置可编译用户编写的脚本，而无需项目中可能存在的 Roslyn 分析器 DLL（ Roslyn 就是微软的.Net开源编译器，编译器支持 C# 编译，并提供丰富的代码分析 API。）


#### 拓展知识

##### unsafe 关键词

默认情况下，C#是不支持 指针 的，unsafe 关键词用于在C#表示不安全的上下文，如果想要在C#中进行任何和指针相关的操作，就必须配合unsafe关键词使用。         
在公共语言运行时（CLR）中，不安全代码是指无法验证的代码。         
C# 中的不安全代码不一定是危险的，只是 CLR 无法验证该代码的安全性。因此，CLR 将仅执行完全信任的程序集中的不安全代码。          

开启unsafe关键字：Player Settings==>Other Settings ==>Allow'unsafe'Code       

>unsafe可以修饰类、成员变量、函数以及代码块。

### 9. OtherSettings-优化相关
|名称|说明|作用|
|--|--|--
|Prebake Collision Meshes|预烘焙碰撞到网格|启用该选项可以在构建时将碰撞数据添加到网格
|Keep Loaded Shaders Alive|保持加载的着色器的活动状态|启动后，不能卸载着色器。着色器加载时会造成性能开销，可能会出现卡顿现象，不允许卸载着色器可以避免卸载后重复加载
|`Preloaded Assets`|预装资源|设置启动时加载的资源数组，将想要预加载的内容拖入框中
|`Strip Engine Code`|剥离引擎代码|选择IL2CPP模式才会出现的字段。能够删除Unity引擎功能中没有使用的代码，可以有效的减小包体大小
|`Managed Stripping Level`|管理剥离水平|`Disabled`：不剥离，只有在Mono模式下才能选择。`Minimal`：最小剥离，Unity只会搜索Unity引擎未使用的.Net类库，不会删除任何用户编写的代码，该设置基本不会出现意外剥离，在使用IL2CPP模式后，该模式是默认模式。`Low`：低级剥离，处理Unity相关，玩家自己编写的代码也会被剥离，会尽量减小意外剥离发生。`Medium`：中级剥离，比起Low更多一些剥离。`High`：高级剥离，优先考虑打包大小，会最大限度剥离代码。可以采配合`link.xml`来手动拒绝剥离，或使用`[Preserve]`特性（在不希望被剥离的函数前加该特性）。一般Minimal足够
|Enable Internal Profiler|(弃用)启用内部探查器|用此选项以从Android SDK的设备中获取profiler数据adblogcat测试项目时输出。这仅在开发版本中可用。
|`Vertex Compression`|顶点压缩|选择要设置的通道，以便在顶点压缩方法下压缩网格。通常，顶点压缩用于减少内存中网格数据的大小，减小文件大小，提高CPU性能。
|`Optimize Mesh Data`|优化网格数据|构建时会从使用的网格中剥离未使用的顶点属性。如果启用了该设置，运行时就`不能更改材质或着色器相关设置`，该选项会删除切换前无用的信息，导致切换后会无法访问丢失的数据
|`Texture MipMap Stripping`|贴图纹理剥离|在构建时会从纹理中剥离没有使用的纹理贴图。会根据你发布平台的质量设置进行比较来确定哪些贴图用不到。`mipmap`生成的多分辨率图也会被剥离
|`Stack Trace`|堆栈跟踪|选择在特定的上下文中允许的日志记录类型,可以选择日志记录的方式,None：不记录,ScriptOnly：只在运行脚本时记录信息,Full：一直记录
|**Legacy**|以前的内容|
|Clamp BlendShapes（Deprecated）|骨骼蒙皮动画中钳制混合形状权重的范围|



### 10. Publishing Settings
|名称|简介|说明|
|--|--|--
|**Keystore Manager**|密钥管理器|
|Create New|创建新的密钥库|`Anywhere`：默认在项目根目录。`In Dedicated Location`：默认在文档文件夹
|Select Existing|选择现有密钥库|默认在项目根目录
|Password|密钥库密码|加载和创建都需要输入
|**New Key Values**|创建新键|
|Alia|密钥的标识名字|
|Password|密钥的密码|
|Validity（years）|有效期（年）|可通过密钥管理应用程序的有效时间
|First and Last Name|姓名|
|Organizational Unit|组织单位|所处部门
|Organization|组织|一般是公司名
|City Or Locality|城市或地区|
|State or Province|州或省|
|Country Code（XX）|国家代码|中国的国家代码为：86
|**Build**|构建|
|`Custom Main Manifest`|自定义主清单文件|决定一些权限设置（比如：网络、定位、拍照等权限配置），还可以设置是否启用一些安卓功能等等
|Custom Launcher Manifest|自定义启动器清单|我们可以在此决定一些应用程序启动之前的外观和行为。（比如：图标、名称、安装位置等等）
|Custom Main Gradle Template|自定义主Gradle构建模板|Gradle 是一个构建系统，可自动执行许多构建过程并防止许多常见的构建错误。Unity将Gradle用于所有Android版本。您可以在Unity中构建输出包（.apk，.aab），也可以从Unity导出Gradle项目，然后使用Android Studio等外部工具构建它。是一个gradle文件，包含有关如何将Android应用程序构建为库的信息
|Custom Launcher Gradle Template|自定义启动器Gradle构建模板|是一个gradle文件，包含有关如何构建Android应用程序的说明
|Custom Base Gradle Template|自定义基础Gradle构建模板|是一个gradle文件，包含在所有其它模板和Gradle项目之间的共享配置
|Custom Gradle Properties Template|自定义属性Gradle构建模板|属性文件，包含Gradle生成环境的配置设置。比如 ：JVM（Java虚拟机）内存配置，允许Gradle使用多个JVM构建的属性，用于选择进行缩小的工具的属性，构建应用程序包时不压缩本机库的属性等等
|Custom Proguard File|自定义Proguard文件|如果缩小删除了一些应该保留的Java代码，你可以添加一条规则来将这些代码保留在此文件中
|**Minify**|代码缩减|会加长发布时间，并且还会让调试变得复杂，所以一般在最终发布时才会使用。
|Use R8||默认情况下，Unity 使用 Proguard 进行缩小。启用此复选框可改为使用 R8。
|`Release`||如果希望 Unity 在发布构建中缩小应用程序的代码，请启用此复选框。
|Debug||如果希望 Unity 在调试构建中缩小应用程序的代码，请启用此复选框。
|`Split Application Binary`|拆分应用程序二进制文件|将输出包拆分为主包 (APK) 和扩展包 (OBB) 包。如果要发布大于 100 MB 的应用程序，则 `Google Play` 应用商店需要此功能。

#### 拓展知识

##### 1.  Android 中的签名
Android要求所有已安装的应用程序都使用数字证书做数字签名，数字证书的私钥由应用开发者持有，Android使用证书作为标示应用程序作者的一种方式，并在应用程序之间建立信任的关系。 证书并不用来控制用户能否安装哪个应用。证书不需要由证书认证中心签名；完全可以使用自制签名证书。       
没有正确签名的应用，Android系统不会安装或运行。此规则适用于在任何地方运行的Android系统，不管是在模拟器还是真实设备上。因为这个原因。在真机或模拟器上运行或者调试应用前，必须为其设置好签名。          
>Android应用的包名就像名字，携带了信息，但有重复的可能性。而签名就像学号，具有唯一性，避免相同包名应用互相覆盖的情况



##### 2. Android 应用程序清单
 `AndroidManifest.xml` 配置文件主要用于声明应用程序的组件，并且还有以下的一些重要作用：

1. 确定应用程序要求的用户权限，比如：网络访问、通讯录访问、信息读取等权限

2. 声明应用程序要求的最低API Level

3. 声明应用程序将要使用的或要求的硬件和软件特性，比如：摄像头访问、蓝牙服务、多点触碰等

>安卓游戏使用一些系统功能或硬件访问权限时，都需要在该文件当中进行设置。


 
##### 3. Gradle

Gradle 是一个自动化构建开源工具，主要面向Java应用为主，也支持其它语言，比如C++、Kotlin、Swift，未来还会支持更多的语言。       

它是一个基于`JVM`（Java虚拟机）的`构建工具`，是一款通用灵活的构建工具，也可以用于Android 项目的构建工作，它可以让安卓项目变得更加简洁。         

在Unity中简单理解Gradle，它就是用于帮助我们打包出安卓应用程序 .apk 的一个工具，在Android Studio中也使用Gradle进行应用程序打包。           


##### 4. ProGuard 和 R8

1. ***代码混淆***  
代码混淆(Obfuscated code)，是将计算机程序的代码，转换成一种功能上等价，但是难于阅读和理解的形式的行为。代码混淆主要用于程序源代码，也可以用于程序编译而成的中间代码。执行代码混淆的程序被称作代码混淆器。已经存在许多种功能各异的代码混淆器。         
将代码中的各种元素，如变量，函数，类的名字改写成无意义的名字。比如改写成单个字母，或是简短的无意义字母组合，甚至改写成“__”这样的符号，使得阅读的人无法根据名字猜测其用途。重写代码中的部分逻辑，将其变成功能上等价，但是更难理解的形式。比如将for循环改写成while循环，将循环改写成递归，精简中间变量，等等。打乱代码的格式。比如删除空格，将多行代码挤到一行中，或者将一行代码断成多行等等。          
代码混淆的主要目的是提升源代码的安全性，别人反编译你的应用程序代码后，增加他们阅读分析逻辑的难度。          

2. ProGuard       
ProGuard是一个压缩、优化和混淆Java字节码（Java源代码通常被编译为字节码）文件的免费的工具，它可以删除无用的类、字段、方法和属性。可以删除没用的注释，最大限度地优化字节码文件。它还可以使用简短的无意义的名称来重命名已经存在的类、字段、方法和属性。常常在Android开发用于混淆最终的项目，增加项目被反编译的难度。             
Unity发布安卓应用程序时，默认使用的就是ProGuard来进行处理最终的代码。

3. R8       
R8是相对ProGuard较新的Android混淆编译器，它可以尽可能的减小应用的大小，早期的Android Studio版本中，混淆编译器使用的是ProGuard执行编译时的代码优化，如果我们使用Android Gradle 3.4.0或更高版本构建项目时，不再使用ProGuard进行代码优化，而是采用R8编译器协同工作。       
Android Sutdio 3.3版本开始，就使用R8作为代码压缩器来对代码进行混淆、压缩、优化了。        
R8 相对 ProGuard来说，它可以更快地缩减代码，同时改善输出的大小。        
Unity发布安卓应用程序时，我们可以选择`使用R8`混淆编译器进行发布处理。


## 1.3 AndroidStudio打包

### 1.导出Gradle

      1. 设置基本游戏信息：公司名，游戏名，图标，包名等
      2. Build Settings勾选Export Project 
      3. Export导出

### 2.AndroidStudio打开项目

打开项目时，优先使用AndroidStudio的SDK。
Unity版本与Gradle需要注意版本匹配。           
[Gradle for Android](https://docs.unity3d.com/Manual/android-gradle-overview.html)



