# Palworld Server Setting Editor

# [README (English)](./README_EN.md)

### 為了讓自己方便修改設定所製作，可能有些代碼不太友好

### 需要請自取, 當然我不能保證不會有錯誤, 目前自己測試是正常的

### [發布 Release (Latest)](https://github.com/murayuki/Palworld-SettingTools/releases/latest)

# 注意事項

> ##### **遊戲版本: `0.1.2.0+` (除官方修改變數)**

> 所有帶有 `_EN` 的 `json`  翻譯都來自 `ChatGPT`

- **A01:**
  
  - **如果是需要英文備註請將 `data_EN.json` 重新命名為 `data.json`**

- **A02:**
  
  - **請自行選擇語言文件 `I18n.json`/`I18n_EN.json`**
  
  - **如果沒有請自行翻譯只要檔案名稱是 `I18n.json` 即可**

- ##### **更新資訊**
  
  - 新增 紀錄上次開啟文件路徑 (程式自動生成 ini 設定文件用於紀錄)
  
  - 變更 `Remark` 文件記錄方式轉為 `data`並修改資料結構
  
  - 新增 `判定輸入類型` 輸入錯誤彈出提示
  
  - 新增 顯示 `預設值` 以及 `可調整範圍` 
    
    - 可調整範圍如果需要`更大數值`請自行修改 `data` 文件

# 功能

* ##### **介面化修改變量數值**

* ##### **讀取 `PalWorldSettings.ini` 文件進行編輯**

* ##### **介面排序 `PalWorldSettings.ini` 文件中變量**

* ##### **將變量標註備註說明**
  
  ![img03](./Images/img03.png)

* ##### 圖片功能介紹
  
  - **(1) 數值編輯框**
  
  - **(2) 開啟上次開啟文件**
  
  - **(3) 讀取文件 (請直接選擇 `PalWorldSettings.ini` 所在目錄即可自動獲取)**
  
  - **(4) 已讀取文件狀態, 如果編輯錯誤可以直接重新讀取原數值**
  
  - **(5) 關閉文件 (不保存)**
  
  - **(6) 保存文件並關閉文件**

# 圖片介紹

![img01](./Images/img01.png)

![img02](./Images/img02.png)

# 資料來源

#### **關於 `data.json`文件中的備註資料下列**

- [Palworld tech guide - Optimize game balance (palworldgame.com)](https://tech.palworldgame.com/optimize-game-balance)

- [【討論】伺服器參數難度調整分享 @幻獸帕魯 哈啦板 - 巴哈姆特 (gamer.com.tw)](https://forum.gamer.com.tw/C.php?bsn=71458&snA=227)
  
  - **特別感謝 [DANIEL6505](https://home.gamer.com.tw/daniel6505) 巴哈版網友的分享`整理`和`測試`**

- [【情報】伺服器參數表 (附單機模式對照) @幻獸帕魯 哈啦板 - 巴哈姆特 (gamer.com.tw)](https://forum.gamer.com.tw/C.php?bsn=71458&snA=466)
  
  - **感謝 [jorden2895 (Jiaa)](https://home.gamer.com.tw/jorden2895) 巴哈版網友 整理的`對照表`**

- [【密技】手把手教學，如何提升Dedicated Server據點帕魯數量上限的方法。（在系統預設範圍內） @幻獸帕魯 哈啦板 - 巴哈姆特 (gamer.com.tw)](https://forum.gamer.com.tw/C.php?bsn=71458&snA=661)
  
  - 關於 BaseCampWorkerMaxNum 目前不能超過 `20`
  - 感謝 [ivan880613](https://home.gamer.com.tw/ivan880613) 巴哈版網友 `圖文教學`
    - 原始來自 [Dedicated Server - Increase Pal Base Worker limit? :: Palworld / 幻獸帕魯 綜合討論 (steamcommunity.com)](https://steamcommunity.com/app/1623730/discussions/0/4132683013933660878/)

- **所使用依賴來自網路開源資源, 如果需要請自行尋找這邊只列出我還記得的來源出處的**
  
  - [Bunny83/SimpleJSON: A simple JSON parser in C# (github.com)](https://github.com/Bunny83/SimpleJSON)
  
  - **<u>當然對於無法列出處的依賴感到非常抱歉</u>**

> ##### LICENSE `GPL-3.0 license`
