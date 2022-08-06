### プロジェクト参照を追加してください
https://github.com/aijkl/zenly-api-client
# zenly-analytics  
- 通知コマンド
- ステータスコマンド

### 通知コマンド  
予め定義しておいた場所への出入を通知する  
![image](https://user-images.githubusercontent.com/51302983/161363660-71b55098-00ac-46fc-a5f0-c5a232a3c716.png)  
```
"notificationCommandSettings": {
    "token": "**************************",
    "toleranceMeter": 200,
    "users": [
      {
        "zenlyId": "u-3QJ57r8E0Uvn4y5JnntTao6C*********",
        "name": "みかん",
        "tokenId": "token-2",
        "notificationChannel": {
          "guildId": 957439194807009311,
          "channelId": 957439707803959376
        },
        "profileUrl": "https://cdn.discordapp.com/attachments/893093461652291634/957137402877587476/X5EPqjff_400x400_1.jpg"
      },
      {
        "zenlyId": "u-BgUiXncsnSk0C0mib0KJXmgo*********",
        "name": "みんとこ",
        "tokenId": "token-1",
        "notificationChannel": {
          "guildId": 957439194807009311,
          "channelId": 957439195473920002
        },
        "profileUrl": "https://cdn.discordapp.com/attachments/791661684061372436/935698866320310383/IMG_0397.jpg"
      },
      {
        "zenlyId": "u-t0B0P4ub1FUR2pZc2YyvxU0Li*********",
        "name": "おかだー",
        "tokenId": "token-2",
        "notificationChannel": {
          "guildId": 957439194807009311,
          "channelId": 957439680012501013
        },
        "profileUrl": "https://media.discordapp.net/attachments/791661684061372436/936280129175453696/IMG_6260.jpg?width=676&height=676"
      }
    ],
    "tokens": [
      {
        "id": "token-1",
        "value": "s-mT*******************",
        "summary": "みかんのトークン"
      },
      {
        "id": "token-2",
        "value": "s-mSQ5***************",
        "summary": "ともやのトークン"
      }
    ],
    "inspectionLocations": [
      {
        "locationName": "HAL 名古屋", //学校
        "longitude": "136.88559371349305",
        "latitude": "35.1682231601466"
      },
      {
        "locationName": "BizComfort", //コワーキングスペース
        "longitude": "136.889705370732",
        "latitude": "35.16629740816833"
      }
    ],
    "scribanArrival": "**{{location_name}}に到着しました**",
    "scribanLeave": "**{{location_name}}から離れました**",
    "pollingIntervalMs": 30000
  },
```

## ステータスコマンド  
定義しておいた場所にいるときは場所の名前を表示する  
例:「みかんの家にいるよ！」
例: 「学校にいるよ！」
定義していた場所に該当しない場合は家からの距離を表示する  
![image](https://user-images.githubusercontent.com/51302983/162597081-6e8c5d31-9554-4bf9-bc46-13b9e350369c.png)  
```
 "statusCommandSettings": {
    "token": "OTYyMzM3MjMzMDgyNDE3MTcy***************************",
    "pollingIntervalMs": 300000,
    "zenlyToken": "s-mSQ5DbdwP2YVFHZaj**************************",
    "userId": "u-3QJ57r8E0Uvn4y5JnntTao6CWMHsCovH",
    "scribanMeterFromHome": "家から{{meter}}メートル",
    "scribanLocationName": "{{location_name}}にいるよ！",
    "home": {
      "locationName": "みかんの家",
      "longitude": "136.53117***********",
      "latitude": "3*************",
      "toleranceMeter": 7000
    },
    "locations": [
      {
        "locationName": "HAL 名古屋", //学校
        "longitude": "136.88559371349305",
        "latitude": "35.1682231601466",
        "toleranceMeter": 300
      },
      {
        "locationName": "BizComfort", //コワーキングスペース
        "longitude": "136.889705370732",
        "latitude": "35.16629740816833",
        "toleranceMeter": 300
      },
      {
        "locationName": "おかでぃーの家",
        "longitude": "136.89*************",
        "latitude": "35.15***********",
        "toleranceMeter": 300
      }
    ]
  },
```

