<?php
$formatted_server_name = "测试服";
$api = "你的API";
$id = "你的ID";
$key = "你的KEY";
$json = json_decode(file_get_contents($api."?id=".$id."&key=".$key),true);
if(!$json["success"])
    die($json["reason"]);
echo $formatted_server_name != "" ? $formatted_server_name . " " : "";
if($json["formatVersion"] != "1.0.4")
    die("解析版本不匹配");

if(strtotime("now") - $json["updatetime"] >= 40)
    die("服务器离线");

$roundinfo = "游戏进行：";
switch ((int)$json["serverStatus"]["roundStartTime"]) {
    case -10086:
        $roundinfo = "回合重置";
        break;
    case -10010:
        $roundinfo = "等待玩家";
        break;
    case -10000:
        $roundinfo = "回合结束";
        break;
    default:
        $sec = strtotime("now") - (int)$json["serverStatus"]["roundStartTime"];
        $roundinfo .= str_replace("-","M",str_replace("=","S",date("i-s=",$sec)));
        break;
}
$adminList = "";
foreach ($json["playerStatus"] as $obj){
    if($obj["remoteAdminAccess"]){
        $adminList .= $obj["nickName"]."\n";
    }
}
echo "在线: ".count($json['playerStatus'])." ".$roundinfo;
if($adminList !== ""){
    echo "\n";
    echo "在线的管理员：\n";
    echo substr($adminList,0,strlen($adminList)-1);
}
