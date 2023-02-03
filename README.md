# ServerQueryer

###### 学习/询问/共享接口 群：[219050931]

###### 发电入口：[爱发电](https://afdian.net/a/manghui/plan)

## ***这个项目是给运行SCPSL.exe服务器装的插件 NWAPI***



#### 目前的原理是 

> *A. 运行SCPSL.exe的服务器 ---> 架设接口的服务器 ---> 同步到数据库 ---> 查询*
>
> 可以省略架设接口，这个方法**日后补充**
>
> *B. 运行SCPSL.exe的服务器 ---> 同步到数据库 ---> 查询*
>
> 但是考虑到有些人想白嫖接口 很显然 A更实在 只需要一个接口服务器 就可以共享给他人
>
> 还有一种方法为：
>
> *C. 运行SCPSL.exe的服务器打开一个TCP端口，需要数据时请求.*
> 
> ![image](https://user-images.githubusercontent.com/52086853/216711667-78c1f72c-4a50-43a6-8df2-d8e72b5f9abd.png) 
> 
> [官方已自带]
>
> 各个方法都有自己的利弊和优势

#### 使用教程

> *1. 插件拷贝到你的Plugins文件下面运行服务器*
>
> *2. 找到插件同等目录下的/ServerQueryer/config.yaml*
>
> *3. 更改你的接口和salt 申请一个id和key 即可使用共享接口和共享机器人*
> 
> *4. 请求接口,共享接口的update.php改为 query.php?id={id}&key={key} key是加密后的key 只需要在后台输入/getquerykey 即可获取*
