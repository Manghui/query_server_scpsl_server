# ServerQueryer

###### 学习/询问/共享接口 群：[219050931](https://qm.qq.com/cgi-bin/qm/qr?k=l40P7CkkOhWxWCJ-pNiH8xpqhoc9LgI2&jump_from=webapi&authKey=CNhFZrJoIqkY9wustKX28Cj9HPVSHYHov0xDvF0UjT53v1sruqRKRnp2sOCOexjJ)

#### 目前的原理是 

*A. 运行SCPSL.exe的服务器 ---> 架设接口的服务器 ---> 同步到数据库 ---> 查询*

可以省略架设接口，这个方法**日后补充**

*B. 运行SCPSL.exe的服务器 ---> 同步到数据库 ---> 查询*

但是考虑到有些人想白嫖接口 很显然 A更实在 只需要一个接口服务器 就可以共享给他人

还有一种方法为：

*C. 运行SCPSL.exe的服务器打开一个TCP端口，需要数据时请求.*

各个方法都有自己的利弊和优势


## ***这个项目是给运行SCPSL.exe服务器装的插件 NWAPI***
