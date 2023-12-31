  
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


## 1.3 AndroidStudio打包APK

### 1.导出Gradle

      1. 设置基本游戏信息：公司名，游戏名，图标，包名等
      2. Build Settings勾选Export Project 
      3. Export导出

### 2.AndroidStudio打开项目

1. 打开项目时，优先使用AndroidStudio的SDK。

2. 升级Gradle            
AndroidStudio读取项目时会提醒升级构建工具，可能该版本并不合适。参考[Gradle for Android](https://docs.unity3d.com/Manual/android-gradle-overview.html)的兼容说明自行判断。         
如果没有提醒，去File==>Project Structure==>Project处设置对应版本，并等待AS自动升级。            
3. 修改配置文件         
 Unity内置Gradle版本较低，导入AS时配置文件有老版本的信息，需要删除，避免影响打包。        
项目根目录的`gradle.properties`配置文件删除`android.enableR8=false`。
因为新版本Gradle默认使用R8，此时配置文件没有该字段。
4. 构建包       
`Build`==>`Build Bundle/APK`==>`Build APK`            
此时可能提示SDK Tools版本不匹配，可以使用AS自己的路径，或者更新SDK。
5. 发布APK        
构建成功后，`Build`==>`Generate Signed Bundle or APK`==>`APK`             
密钥选择Unity中创建的密钥库，发布可以选择`release`和`debug`两种版本


## 1.4 调试

### 1. Unity连接调试

#### 1.1 手机准备 
手机开启`开发者模式`，并开启`USB调试`     

#### 1.2 Unity设置            
1. `Build Settings`==>`Android`==>`Run Device`刷新后可以选择连接的手机           
2. 勾选`Development Build`开启开发模式构建            
3. `Autoconnect Profiler`自动连接分析器
4. `Deep Profiling`深度剖析         
5. `Script Debugging`脚本调试       
6. `Wait For Managed Debugger`开启断点调试      
7. 项目名称，包名，公司名，密钥库等确认是否设置       
               
#### 1.3 开始调试       
1. 在`Build Settings`==>`Build And Run`         
选择打包路径，Unity将包自动传入手机       
2. 漫长的打包之后，手机需要确认安装
3. 安装完成后，手机会弹窗提示是否等待断点调试
4. 如果需要断点调试，先选择VS的`调试`==>`附加Unity调试程序`选择手机设备
5. 点击手机上的确认，开始运行项目
6. 如果需要使用`Profiler`一定要**关闭防火墙**！！！如果还不行，就试试[Android真机调试](https://www.cnblogs.com/fengxing999/p/9958593.html)中的方法

#### 1.4 Unity Remote

1. 手机下载`Unity Remote`     
2. `Project Settings`==>`Editor`==>`Device`==>`Any Android Device`      
3. 手机连接电脑并打开USB调试，打开`unity Remote`
4. 电脑运行项目，手机端会有投影，并且可以响应手机端的反馈(触屏，GPS，陀螺仪等)

>如果电脑插入多个设备时，会自动选择第一台设备。             
Unity Remote仅为了快速测试输入功能以及表现效果，节省测试的打包时间

#### 1.5 Android Logcat 
Android Logcat是Unity的一个拓展包         
在Unity2019.4版本进入PackageManager搜索安装
##### 1. 功能     
1. 安卓日志打印信息
2. 安卓应用程序内存统计
3. 安卓屏幕截图
4. 安卓屏幕录像
5. 堆栈跟踪

##### 2. 界面介绍
1. `Auto Run` 自动运行
2. `No device` 设备选择，点开选择目标设备
3. `No Filter` 过滤选择器，指定连接设备上显示消息的应用程序
4. `过滤输入框` 输入搜索内容，可以在下方的`Priority`对消息进行二级过滤
5. Priority 优先级      
 Verbose：所有  
Debug：调试信息   
Info：信息  
Warn：警告  
Error：错误       
Fatal：致命的     
6. Filter Options 过滤器选项              
Use Regular Expressions：使用正则表达式         
Match Case：区分大小写        
7. Reconnect：重新连接
8. Disconnect：断开和设备的连接           
9. `Tools`：工具相关          
Screen Capture：屏幕捕获        
Open Terminal：开放终端 直接进入路径`SDK\platform-tools`         
Stacktrace Utility：堆栈跟踪应用程序             
Memory Window：内存窗口
跟踪为应用程序分配的内存
1.   `消息日志`             
Time:消息产生的时间       
Pid：生成消息的进程ID   
Tid：生成消息的线程ID   
Priority：消息的优先级        
Tag：消息相关联的标签   
Message：消息文本             
**右键日志目录可以设置显示内容**

##### 3. Screen Capture
路径:    Android Logcat==>Tools==>Screen Capture       
提供给测试人员记录bug的工具。       
功能分为`截图`和`录屏`        

下方为大致按钮布局 
|设备ID|切换截图和录屏|开始/停止捕获|打开文件|另存|
|--|--|--|--|--|
|时长限制|
|视频尺寸|默认是设备屏幕分辨率
|比特率|默认值
|显示器ID|没必要设置



##### 4. Stacktrace Utility
适用于非连接调试，获取了安卓崩溃日志时翻译日志 

下方为大致按钮布局 
|原始日志|解析后的日志|解析日志|
|--|--|--|
|||配置正则表达式(一般不动)
|||`配置符号路径`

>未配置的情况下，直接翻译日志会报错，需要点击`Configure Symbol Paths`配置符号路径，在弹出的`Android Logcat Settings`的`Symbol Paths`点击`+`选择Unity版本，打包方式和cpu版本，会配置默认应库文件的路径

库文件包含3个重要包文件为:          
libmain.so        
libunity.so             
libil2cpp.so(il2cpp模式时需要使用)              
so文件相当于就是windows下的dll库文件，里面包含所有代码信息
想要进行翻译就必须设置so文件的路径
我们可以使用默认的，也可以自己导出选择导出的so文件。        
在`Build Settings`中勾选`Create symbols.zip`时，会在构建APK时创建一份so文件压缩包         
项目逻辑被转成os库后，出错时的日志属于安卓日志，逻辑无法辨识，可以让Unity参考so库文件，寻找到日志对应信息，并以Unity日志的形式展示出来


##### 5. Memory Window
`Auto Capture`：自动捕获
Unity会定期捕获应用程序的内存快照
注意：自动捕获，可能会影响性能表现，可能会造成卡顿，如果严重影响测试，建议使用手动捕获

`Manual Capture`：手动捕获
通过点击按钮，自己手动捕获内存快照

|Group|名称|作用|
|--|--|--|
|Resident Set Size|常驻集大小|应用程序在运行时内存中分配的内存总量
|`Proportional Set Size`|比例集大小|应用程序主动使用的运行时内存总量
|`Heap Alloc`|堆分配|应用程序使用Java分配器和本机堆分配的内存总量。当检查内存泄露时，通过它可以进行很好的分析
|Heap Size|堆大小|应用程序保留的总内存，总是大于堆分配

内存图例:         
NativeHeap:本地堆       
JavaHeap:Java分配器堆         
Code:代码         
Stack:栈          
Graphics:图形相关       
PrivateOther:其它私有的       
System:系统       
Total:总共的            

#### 1.6 ADB调试
##### ADB是什么
ADB是 Android Debug Brige（安卓调试桥）的简称   
它是我们调试Android设备的一套指令集       
它可以让我们通过指令来进行一些操作，来获取日志信息    
比如：      
1.关机、重启      
2.安装、启动、卸载应用程序    
3.删除、移动、复制文件  
4.查看日志信息    
Android Logcat工具其实就是利用了ADB来获取的信息


##### 使用ADB

在`Android Logcat`==>`Open Terminal`自动打开cmd并定位到当前SDK路径下，可以使用adb相关指令          
如：        
显示日志信息      adb logcat    
获取Unity相关日志信息   adb logcat -s Unity         
更多指令访问[ADB详细介绍](https://developer.android.com/studio/command-line/adb.html)     


### 2 Android Studio调试

在前面将项目导出的基础上，手机打开USB调试，AS先Build项目确保可运行，再到右上角选择设备，`Run`==>`Run launcher`，会将项目打包并发布到设备上，运行后，AS下方有Logcat和Profiler，与Unity内部的两个工具类似。         
非老项目，一般会选择Unity内部的工具


以下是Developers提供的教程          
[探索 Android Studio](https://developer.android.google.cn/studio/intro?hl=zh-cn)    
[调试应用](https://developer.android.google.cn/studio/debug?hl=zh-cn)         
[分析应用性能](https://developer.android.google.cn/studio/profile?hl=zh-cn)


# 2. Java快速入门
**此处记录会较潦草，仅记录与C#的差异**

## 2.1 准备阶段           
### 1. 环境
JDK与环境变量在Lua的环境准备已经做过，略

### 2. IDE安装
在学校用的是Eclipse，此处使用IDEA   
[IDEA Community Edition](https://www.jetbrains.com/idea/download/download-thanks.html?platform=windows&code=IIC)

安装时，Create Associations勾选`.java``.kt``.kts`           
创建项目时，可以在`JDK`找到Unity用的JDK

IDEA可以装插件,`File`==>`Settings`==>`Plugins`       

设置文件编码`File`==>`Settings`==>`Editor`==>`Code Style`==>`File Encodings`上下两个都选UTF-8


## 2.2 语法

### 1. 注释

C#中的

      /// <summary>
      /// 函数名
      /// </summary>
      /// <param name="id">入参名</param>
在Java中

    /**
     * 函数名
     * @param args 入参名
     */

其他的：`//`  `/**/` 通用

### 2. 变量
#### 1. 无符号       
Java中没有uInt，uFloat这种无符号值，Java8加入了可转换至无符号的方法，但少用。       
#### 2. boolean        
单个boolean在编译时，使用int类型，在数组中使用字节数组，占一个字节。          

#### 3. 常量           
C#关键字const         
Java关键字finel       

#### 4. 类型转换       
1. 隐式转换             
byte->short->char->int->long->float->double
2. 显式转换             
parseInt("转换内容"，内容是几进制)，只填内容默认算10进制。

            int i=Integer.parseInt("231",8);



### 3. 运算符

Java有无符号右移`>>>`，右移后左侧补0   
`>>`在右移后，左侧会根据上一个数补充，1000==>1100       
在`>>>`的效果是 1000==>0100     

### 4. 条件语句

#### switch

C#的`switch`在贯穿时，被贯穿的case不能包含自己的逻辑        

        switch (id)
            {
                case "1":
                case "2":
                case "3":
                    print(1);
                    break;
            }

而Java中的可以          

        switch (a)
        {
            case    1:
                System.out.println(1);
            case    2:
                System.out.println(2);
            case    3:
                System.out.println(3);
                break;
        }


#### foreach

Java中的foreach，通过for达成同样的操作        
编辑器中输入`foreach`，会快捷输入一个for循环，参数内容可参考C#中的foreach

        for (var item:items) {
        }
此时的for就是一个C#中的foreach了，通过迭代器获取容器内容




### 5. 数组

Java中的声明可以是：int arr[];   
int[] arr;
而C#中只能:int[] arr;

#### Arrays类
是对数组操作的类        
1. 填充元素       
Arrays.fill(数组，填充值)       
Arrays.fill(数组，起始索引，结束索引，填充值)索引左闭右开


2. 排序     
Arrays.sort(数组),默认是升序排序

3. 复制     
Arrays.copyOfRange(被复制数组，起点索引，终点索引)左闭右开      
Arrays.copyOf(被复制数组，复制长度)         


4. 查询     
Arrays.binarySearch(数组，搜索元素)      
Arrays.binarySearch(数组，开始索引，结束索引，搜索元素)索引左闭右开      
此方法是对数组进行二分查找，需要先排序           
   

### 6. 函数

1. Java中没有`ref`和`out`关键字        

2. Java中函数入参没有默认参数

3. Java函数命名采用驼峰命名法，C#采用帕斯卡命名法       


4. Java可变参数写法不同     

        public void testFun(String... strs) {
        }


### 7. 类       

#### 基础

1. 无成员属性，需要使用时，自己声明getset方法，或者使用ide内置的快捷设置生成方法。  

2. finalize和C#中的析构函数类似，在gc时执行

3. 没有索引器

4. 不能运算符重载   
5. Java没有as关键字
6. `is`关键字在Java中是`instanceof`



#### 继承

1. 继承不是`:`，是`extends`
2. `getType()`换成`getClass`
3. 里氏替换同理
4. `final`密封关键字同理，都不能被继承、重写、修改      
5.  Java中没有`virtual`和`override`关键字
6.  Java中的`base`关键字是`super`
7. Java中也有`abstruct` 
8. 接口     
可以声明字段，字段为`static`和`final`,静态常量      
继承接口的关键字是`implements`      
Java中不同接口的同名方法共享重写方法，C#中两个接口有同名方法时，需要显示实现接口。


#### 包     
类似C#的命名空间        
1. 包名全小写，用`.`分割
2. 与命名空间不同的是，命名空间对脚本位置没有要求，只需要声明属于哪个namespace即可，脚本属于哪个包就要声明在包内，脚本也在该包的文件夹内        
3. 对包的使用，和命名空间类似 `import xxxx.xxxx.xxx.引入的类`,全部引入就是`import xxxx.xxxx.xxx.*`
4. 静态导入`import static java.lang.System.*;`，System类下的静态方法可以直接调用。
      
#### 内部类     
1. Java中的内部类创建，需要先创建外部类，才能创建内部类        

        Outer outer=new Outer();
        Outer.Inner inner=outer.new Inner();

2. 内部类可以使用外部类的成员变量，即使是private
3. 同名成员通过`this`和`外部类.this`区分        
>C#中的内部类更像被写在外部类里的独立类，而Java中的内部类属于外部类可以与外部类交互     
4. 匿名内部类可重写父类的方法       

        //外部类
        public class Outer{
            public void Eat(){

            }
        }
        //匿名内部类
        Outer outer2=new Outer(){       
            public void Eat(){      
                //覆写逻辑      
            }       
        }


### 8. String

#### 1. 字符串声明

Java在代码中直接赋值字符串时，同样字符串内容的string使用的内存地址是相同的，即Java只创建了一个空间，让重复的string指向一个地址。        
而通过string的构造方法写入的字符串即使内容相同也会开辟两个新的内存空间分别存储。

#### 2. 常用方法

1. 比较     
   对比引用地址时，使用`==`     
   对比内容时，使用`equals`方法     
   还有忽略大小写的比较方法`equalsIgnoreCase`

2. 去除首尾空格,`String.trim()`，仅去除字符串首尾的空格
3. 判断字符串开头和结尾字符
   
        boolean yes =str.startsWith("头部文本")；   
        boolean yes =str.endsWith("尾部文本")；

4. 字符串格式化     
   String str=String.format(" %b ",b);    

   |占位符|适用格式|
   |--|--|
   |%b、%B|boolean类型格式化符号|
   |%s、%S|String类型格式化符号
   |%c、%C|char类型格式化符号
   |%d|十进制数格式化符号
   |%o|八进制数格式化符号
   |%x、%X|十六进制数格式化符号
   |%e|十进制数的科学计数法格式化符号
   |%tF|年-月-日 时间格式
   |%tD|月/日/年 时间格式
   |%tc|全部日期和时间信息
   |%tr|时:分:秒 PM(AM) 时间格式
   |%tT|时:分:秒 24小时制 时间格式
   |%tR|时:分 24小时制 时间格式       

5. Java也有StringBuilder    
   使用方法和C#同理     

        StringBuilder sb=new StringBuilder();
        sb.append("拼接内容");      
        sb.insert(插入索引,"插入内容");     
        sb.delete(删除起点,删除终点);//同样是左闭右开     
        String str =sb.toString();      


### 9. 泛型     

1. 不能写类型，要写该类的封装类     
   
   TestT<Ingeter> tt=new TestT<Ingeter>();

2. 泛型方法的泛型入参书写位置不同
   
       public<T>  void  Test2(T t)

3. 泛型方法调用时不用额外填写类型

        tt.Test2("入参");

4. 泛型约束

       public<T extends Father>  void  Test2(T t)

5. 类型通配符   
   可以在声明泛型类时不填写类型，使用通配符`?`占位，使用时再填入类型。

6. 约束可以使用时再声明     
   妹整明白，回头再看链接[Java?通配符](https://blog.csdn.net/qq_40587575/article/details/78858249)

        TestT<? extends Father> tt=null;
        tt=new Test<Son>();


### 10. ArrayList和LinkedList
两者类似C#的List和LinkedList

1. 当ArrayList中存储的是int时，调用remove(value)只能将value当作索引来处理
2. 使用迭代器       
   
        //得到迭代器
        Iterator<Interger> it=list.iterator();
        //判断是否可迭代
        while(it.hasNext()){
            System.out.println(it.next());
        }

### 11. HashMap和TreeMap

使用方式和C#的字典差不多，一个是哈希表一个是树。因为两者的特性，键都有唯一性，但TreeMap的键不能为空                     
如果忘了哈希表和树的知识                
[哈希表](https://www.hello-algo.com/chapter_hashing/hash_map/)          
[树](https://www.hello-algo.com/chapter_tree/binary_tree/)


### 12. 异常捕获            
和C#类似                

        try {
            //异常捕获代码块
        }
        catch (Exception ex){
            //捕获异常信息
            ex.getMessage();
        }
        finally {
            //不管前面是否执行，finally都能继续
            //除非该程序退出或者线程被销毁
        }


1. 创建自定义异常捕获类        

        public class MyException extends  Exception{
                public int num;
                public MyException(String str){
                        //将str传给基类构造方法
                        super(str);
                }
        }

2. 使用自定义异常捕获类            

        try{
            int i=1;
            int targetNum=5;
            if (i<targetNum) {
                //创建类
                MyException me=new MyException("数小了");
                me.num=targetNum-i;
                //抛出
                throw me;
            }
        }
        //捕获
        catch (MyException ex){
            System.out.println(ex.getMessage()+"  比目标数值小"+ex.num);
        }


3. 在方法中抛出异常     
   
        //在方法后面提前抛出异常
        public static   void  TryException() throws ArrayIndexOutOfBoundsException
        {

        }
        //捕获异常
        try {
            TryException();
        }
        catch (ArrayIndexOutOfBoundsException ae){
            System.out.println("索引超出范围");
        }

### 13. Lambda
基本结构        
 `(参数)->{逻辑}`       
单行逻辑时      
 `(参数)->逻辑`       

Java中没有委托，但提供了函数式接口

创建接口        

        interface  ITest{
                String Test();
        }
使用    

        ITest t=()->"反参";
        System.out.println(t.Test());
>注意！！！此时接口只能声明一个方法             

lambda表达式不能修改函数局部变量，被lambda使用的变量对lambda来说是final             
lambda可以修改类成员变量的值


        public   class  LambdaTest{
                //可以修改可以使用
                public  int classNum=1;
                public  void  TestFun(){
                        //不可修改可以使用
                        int funNum=3;
                        ITest it=()->{
                                classNum=6;√
                                funNum=6;×
                                return null;
                        };
                }
        }


### 14. 方法引用
函数式接口可以接收匿名函数那么自然也可以接收类的方法。          
使用方式类似C#中的委托

 1. 接收静态方法：  
函数式接口 name=目标类`::`目标方法

        //声明接口=该类的静态方法Test
        ITest01 it=LearnFun::Test;
        //通过该接口的方法调用被记录的Test方法
        it.fun();

2. 接收成员方法         
函数式接口 name=已实例化的目标类`::`目标方法

        LearnFun lf=new LearnFun();
        ITest01 it2=lf::Test2;
        it2.fun();


3. 接收泛型方法                 
此时的接口声明也是泛型          
函数式接口<泛型类> name=目标类`::`目标方法

        interface  ITest03<T>{
                void  fun(T t);
        }
        //使用
        ITest03<Integer> iTest03=LearnFun::GenericFUn;
        iTest03.fun(32);





### 15. Function接口
和C#一样，自己动手声明一堆委托太麻烦了，官方有封装好的Function          



        Function<Integer,String> function= (i)->{
            return  i.toString();
        };

### 16. 常用类库

#### 1. Number类      
`Number`是Byte Integer Short Long Float Double类的父类
主要方法：

                byteValue()  以byte形式返回指定的数值
                intValue()   以int形式返回指定的数值
                floatValue() 以float形式返回指定的数值
                shortValue() 以short形式返回指定的数值
                longValue()  以long形式返回指定的数值
                doubleValue()  以double形式返回指定的数值


#### 2. Integer类      
它和Byte、Short、Long三个封装类方法基本相同
主要方法：

           parseInt(String str)                  将字符串转数值
           toString()                            将数值转字符串
           toBinaryString(int i)                 以二进制无符号整数形式返回一个整数参数的字符串表示形式
           toHexString(int i)                    以十六进制无符号整数形式返回一个整数参数的字符串表示形式
           toOctalString(int i)                  以八进制无符号整数形式返回一个整数参数的字符串表示形式
           equals(Object integerObj)             比较两个对象是否相等
           compareTo(Integer anotherInteger)     比较两个Integer对象，相等返回0；调用函数对象小于传入对象，返回负数；反之，返回正数


#### 3. Double类      
它和Float类的方法基本相同               
主要方法：

           parseDouble(String str)               将字符串转数值
           toString()                            将数值转字符串
           isNaN()                               如果该double值不是数字，返回true，否则返回false
           compareTo(Double d)                   和Integer类中方法作用一致
           equals(Object doubleObj)              和Integer类中方法作用一致
           toHexString(double d)                 返回double参数的十六进制字符串表示形式

#### 4. Boolean类

           equals(Object obj)                    和Integer类中方法作用一致
           parseBoolean(String s)                将字符串转Boolean
           toString()                            将数值转字符串
           valueOf(String s)                     返回一个用指定的字符串表示的boolean值


#### 5. Character类

           compareTo(Character anotherCharacter) 比较两个Character对象，若两个对象相等则返回0
           equals(Object obj)                    和Integer类中方法作用一致
           toString()                            转字符串
           toUpperCase(char ch)                  将字符转大写
           toLowerCase(char ch)                  将字符转小写
           isUpperCase(char ch)                  判断字符是否是大写
           isLowerCase(char ch)                  判断字符是否是小写
           isLetter(char ch)                     判断字符是否是字母
           isDigit(char ch)                      判断字符是否为数字


#### 6. BigInteger
该类主要用于存储任意大小的整数，也就是说它可以表示任何大小的整数值而不会丢失信息                
因为传统的整形类型都有最大最小区间，而该类没有，主要用于存储大数据      
主要方法：

           add(BigInteger val)               加法
           subtract(BigInteger val)          减法
           multiply(BigInteger val)          乘法
           divide(BigInteger val)            除法
           remainder(BigInteger val)         取余
           pow(int exponent)                 计算exponent次方
           negate()                          取反
           shiftLeft(int n)                  左移n位
           shiftRight(int n)                 右移n位
           and(BigInterger val)              位与
           or(BigInteger val)                位或
           compareTo(BigInteger val)         比较，类似Integer中
           equals(Object x)                  判断数值是否相等
           min(BigInteger val)               取最小
           max(BigInteger val)               取最大、


#### 7. BigDecimal
该类和BigInteger用于表示大数据，但是它主要用于表示浮点数（有小数点的数值）              
它的主要方法和BigInteger类似

#### 8. Math类
 主要方法：


三角函数                

        sin(double a)                         正弦
        cos(double a)                         余弦
        tan(double a)                         正切
        asin(double a)                        反正弦
        acos(double a)                        反余弦
        atan(double a)                        反正切
        toRadians(double angdeg)              角度转弧度
        toDegrees(double angrad)              弧度转角度


指数            

        exp(double a)                         获取e的a次方
        log(double a)                         取自然对数
        log10(double a)                       取底数为10的a的对数
        sqrt(double a)                        取a的平方根
        cbrt(double a)                        取a的立方根
        pow(double a, double b)               取a的b次方

取整    

        ceil(double a)                        向上取整
        floor(double a)                       向下取整
        rint(double a)                        返回与a最接近的整数，如果有两个，取偶数
        round(float a)                        将参数a加上0.5后返回与其最近的整数
        rount(double a)                       将参数a加上0.5后返回与其最近的整数，然后强转为Long
        
其他            

        max(参数1，参数2)                      最大值
        min(参数1，参数2)                      最小值
        abs(参数)                             绝对值


#### 9. Random

        Random r = new Random();              以当前系统时间作为随机数生成器种子
        Random r = new Random(seedValue);     自己设置随机数种子
        nextInt()                             返回一个随机整数
        nextInt(int n)                        返回大于等于0且小于n的随机整数
        nextLong()                            返回一个随机长整型
        nextBoolean()                         返回一个随机布尔值
        nextFloat()                           返回一个随机单精度浮点
        nextDouble()                          返回一个随机双精度浮点
        nextGaussian()                        返回一个概率密度为高斯分步的双精度浮点

#### 10. 其他类         

        Data：日期类 获取日期时间相关方法
        Calendar：日历类 比起Date更加国际化
        System:系统类 有获取当前时间的方法

# 3. Unity与Android交互         

## 3.1 Android相关介绍

1. Android SDK   
提供了用于开发Android应用程序的各种API和工具    
2. 编程语言      
使用Java或者Kotlin语言进行开发，他们提供了丰富的库和API 
3. XML配置文件   
Android应用程序使用XML来定义UI布局、样式和资源信息等    
### 1. Android四大件 
Android应用程序由四种组件组成： 
1. Activity(活动)    
主要用于实现用户界面，代表一个屏幕或窗口，包含了各种UI组件，按钮，文本，输入框等等
2. Service(服务)        
是一种可以在后台执行长时间运行操作的组件，没有用户界面，一般用于处理和交互无关的逻辑。          
比如：上传、下载、音乐播放等

3. Broadcast Receiver(广播接收器)       
主要用于接受系统或者其他应用程序发出的广播消息。
消息可以来自系统事件（比如网络连接变化、设备启动等），也可以来自其它应用程序
4. Content Provider(内容提供程序)       
用于管理应用程序数据，可以让其它应用程序或系统访问本应用中的数据，也可以让本应用访问其它应用或系统的数据。      
比如用于存储应用程序数据，图片、音频、视频等

我们接触的大部分都是Activity，类似在Unity中的UI模块，需要后台逻辑时，会使用另外三种组件

### 2. AndroidManifest.xml文件的作用

它是Android应用程序的应用清单文件       
每个应用程序都必须包含一个，并且文件名必须是AndroidManifest.xml         
该文件中包含了应用程序的配置信息，Android系统会根据该配置来运行应用程序

该文件中包含的重要信息有：
1. 应用程序包含的`四大组件`的内容（Activity,Service,Broadcast Receiver, Content Provider）
2. 应用程序的`权限`（存储权限、互联网访问权限等等）
3. 应用程序`元数据`（程序名称、版本号、图标、包名等等）
4. 应用程序`启动信息`（默认启动哪一个Activity）

常用标签作用：
|标签|作用|
|--|--|
| manifest|主要包含包名、版本号等等|    
| uses | permission：应用程序权限|
| application|应用程序各组件包含在其中，还可以配置一些图标、文本、样式等等信息|
| activity|Activity组件的具体信息|
| meta-data|为Activity提供元数据，可以通过API获取该数据|
|intent-filter|为组件声明意图(intent),其中还包含action(意图类型)和category(意图类别)两个子标签|


### 3. jar与aar
1. jar包包含Android项目中的脚本文件和清单文件，不包含资源文件，jar包导入其他工程后，可以引用源码，Eclipse打包一般是jar包        
2. aar包是AndroidStudio下打包Android工程中的src(脚本文件),res(资源文件)，lib（库文件）生成的打包文件


## 3.2 准备Android工程         
此安卓工程只是提供逻辑，并不是完整的项目，需要删除多余的资源

### 1. Unity创建工程设置好安卓包            
并记录安卓包名    
### 2. AndroidStudio创建工程        
选择`Empty Views Activity`
包名输入Unity工程设置的包名，语言选择Java，Minimum SDK选择与Unity中一致的版本
### 3. 删除安卓项目多余的内容       
项目切换成`Android`，删除`java`中带有`(test)(androidtest)`的java包，`res`是安卓项目自己的资源，也要删去无用的

### 4. 修改`build.gradle`           
项目切换成`Project`，从`app`打开`build.gradle`。        
`id("com.android.application")`改成`id("com.android.library")`,         
删除`applicationID`，如果同步报错，就按提示删除`versionCode`和`versionName`。        
点右上角`Sync Now`同步

### 5. 导入classes.jar
路径：Unity安装目录\Data\PlaybackEngines\AndroidPlayer\Variations\mono(il2cpp)\Release\Classes

1. 将包拷贝到AndroidStudio中的app\libs下

2. 导入后 选择包 右键点击 Add As Library

### 6. 导入UnityPlayerActivity
2019版本以上需要本步骤          
路径：Unity安装目录/Data/PlaybackEngines/AndroidPlayer/Source/com/unity3d
将路径下的文件夹拷贝到AndroidStudio中的 app/src/main/java/com中

### 7. 修改文件设置
1. MainActivity         
继承换成`UnityPlayerActivity`           
注释onCreate函数中的setContentView代码
2. 修改AndroidManifest          
在`manifest`最后添加`package="com.xx.xx"`具体的包名     
删除`<application>`中的信息，
在`intent-filter`下方添加`<meta-data android:name="unityplayer.UnityActivity" android:value="true"/>`      


3. `Build`==>`Make Module`              
可能出现报错`Recommended action: Update this project to use a newer compileSdk of at least 33, for example 34.`，根据提示修改complieSDk和targetSDK即可(Android的版本兼容问题真的太麻烦了)               
构建成功后，`Project`==>`app`==>`build`==>`outputs`==>`aar`下有一个aar包

### 8. 导入到Unity
1. 上一步的aar包导入到Unity`Plugins`==>`Android`
2. 将Android项目中的`AndroidManifest.xml`copy到Unity`Plugins`==>`Android`

## 3.3 Unity调用安卓    

### 1.C#调用AndroidJavaClass

        //初始化交互类
        using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
                //获取Activity对象,此时获取的是MainActivity类对象
                using(AndroidJavaObject ajo=ajc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                        //成员变量
                        int i = ajo.Get<int>("testI");
                        ajo.Set<int>("testI", 11);
                        i = ajo.Get<int>("testI");
                        //静态变量
                        int staticI = ajo.GetStatic<int>("testStaticI");
                        ajo.SetStatic<int>("testStaticI", 11);
                        staticI = ajo.GetStatic<int>("testStaticI");
                        //成员方法
                        string funStr =ajo.Call<string>("TestFun");
                        //静态方法
                        string staticStr = ajo.CallStatic<string>("TestStaticFun");
                }
        }

### 2. 打包项目
 
1. 设置密钥     
2. aar包删除多余内容      
压缩工具打开aar，删除libs下的classes.jar，Unity自带，不删除会报错  
以下无报错时不需要操作:             
aar下的的`classes.jar`打开，进入到自己的包中找到项目名称的文件夹内有`MainActivity.class`，为避免报错可以删去。（目前没发现报错）        
该路径下可能还有文件`BuildConfig.class`，报错也需要删去


## 3.4 安卓调用Unity


### 注意：         
想要被Android端调用的Unity函数  
1.需要写在`继承MonoBehaviour`的脚本中     
2.需要挂载在场景中处于`激活状态`的GameObject上

猜测：应该是使用UnityPlayer通过Find找到目标GameObject，拿到第一个脚本反射操作该脚本的目标方法
### 交互方法    

UnityPlayer.UnitySendMessage("GameObject名称", "函数名", "参数信息")
> 注意：该API中的参数只能是String类型或者为null


## 3.5 Unity嵌入Android

### 1. 创建Activity

`File`==>`New`==>`Activity`==>`Empty Views Activity`            
设置Activity类名，此项目不是完整的安卓项目，不需要设置`Launcher Activity`               

到`res`==>`layout`==>`activity_android`，或者通过创建的Activity类中的`setContentView`内容`activity_android`Ctrl点击也可以进入，打开UI编辑页面。

拖拽一个按钮和文本，并拖动两个组件的对齐                    

在`MainActivity`声明方法，调用新Activity                

    public void OpenActivity(){
        Intent intent=new Intent(this,AndroidActivity.class);
        startActivity(intent);
    }

### 2. 嵌入Unity

导出aar包，Unity通过`currentActivity`调用刚才的方法     

Unity逻辑写完后，**导出**项目,到AndroidStudio中打包项目



报错情况：      
1. [opens java.io报错解法](https://blog.csdn.net/crasowas/article/details/130002017)       
2. `Android resource linking failed`，指向新建的activity资源连接失败            
 构建aar包的项目根目录==>`build.gradle.kts`==>`dependencies`==>`implementation("androidx.constraintlayout:constraintlayout:2.1.4")`
 复制到构建项目==>`unityLibrary`==>`build.gradle`==>`dependencies`
1. `This project uses AndroidX dependencies`    
 构建项目根目录==>`gradle.properties`在org下面添加`android.useAndroidX=true`

>版本兼容太难搞了，暂时放弃     

## 3.6 接入SDK     

### 
