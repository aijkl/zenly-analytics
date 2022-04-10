# zenly-analytics  
- 通知コマンド
- ステータスコマンド

### 通知コマンド  
予め定義しておいた場所への出入を通知する  
![image](https://user-images.githubusercontent.com/51302983/161363660-71b55098-00ac-46fc-a5f0-c5a232a3c716.png)  
```
    "toleranceMeter": 100,//誤差の許容値 半径100メートルの円
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
      },
      {
        "locationName": "みかんの家",
        "longitude": "",
        "latitude": ""
      },
      {
        "locationName": "みんとこの家",
        "longitude": "",
        "latitude": ""
      },
      {
        "locationName": "おかでぃーの家",
        "longitude": "",
        "latitude": ""
      }
    ],
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

