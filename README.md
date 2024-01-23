# Palworld Server Setting Editor

# [README (English)](./README_EN.md)

### 為了讓自己方便修改設定所製作，可能有些代碼不太友好

### 需要請自取, 當然我不能保證不會有錯誤, 目前自己測試是正常的

### [發布 (Releases)](https://github.com/murayuki/PalWorld-SettingTools/releases)

# 注意事項

> 所有帶有 `_EN` 的 `json`  翻譯都來自 `ChatGPT`

- **A01:**
  
  - **如果是需要英文備註請將 `Remark_EN.json` 重新命名為 `Remark.json`**

- **A02:**
  
  - ~~**程式中的語言為 `繁體中文`**~~
  
  - ~~**如果需要英文請自行建構 `EXE執行文件` 需要對 C# 有基本能力以及 Visual Studio 的基本操作 (開發使用 2022)**~~
  
  - **請自行選擇語言文件 `I18n.json`/`I18n_EN.json`**
  
  - **如果沒有請自行翻譯只要檔案名稱是 `I18n.json` 即可**

# 功能特點

* ##### **介面化修改變量數值**

* ##### **讀取 `PalWorldSettings.ini` 文件進行編輯**

* ##### **介面排序 `PalWorldSettings.ini` 文件中變量**

* ##### **將變量標註備註說明**
  
  ![./Images/img03.png](./Images/img03.png)

* ##### 圖片功能介紹
  
  - **(1) 數值編輯框**
  
  - **(2) 讀取文件 (請直接選擇 `PalWorldSettings.ini` 所在目錄即可自動獲取)**
  
  - **(3) 已讀取文件狀態, 如果編輯錯誤可以直接重新讀取原數值**
  
  - **(4) 關閉文件 (不保存)**
  
  - **(5) 保存文件並關閉文件**

# 圖片介紹

![img01](./Images/img01.png)

![img02](./Images/img02.png)

# 資料來源

#### **關於 `Remark.json`文件中的備註資料下列**

- [Palworld tech guide - Optimize game balance (palworldgame.com)](https://tech.palworldgame.com/optimize-game-balance)

- [【討論】伺服器參數難度調整分享 @幻獸帕魯 哈啦板 - 巴哈姆特 (gamer.com.tw)](https://forum.gamer.com.tw/C.php?bsn=71458&snA=227)
  
  - **特別感謝 [DANIEL6505](https://home.gamer.com.tw/daniel6505) 巴哈版網友的分享`整理`和`測試`**

- **所使用依賴來自網路開源資源, 如果需要請自行尋找這邊只列出我還記得的來源出處的**
  
  - [Bunny83/SimpleJSON: A simple JSON parser in C# (github.com)](https://github.com/Bunny83/SimpleJSON)
  
  - **<u>當然對於無法列出處的依賴感到非常抱歉</u>**

> ##### LICENSE `GPL-3.0 license`
