# Palworld Server Setting Editor

### Created for easy modification of settings; some code may not be user-friendly.

### Feel free to use, but I cannot guarantee the absence of errors. It has been tested and found to be functional.

### [Latest Release](https://github.com/murayuki/Palworld-SettingTools/releases/latest)

# Important Notes

> ##### **Game Version: `0.1.2.0+` (except for official variable modifications)**

> All translations with `_EN` in the `json` file come from `ChatGPT`.

- **A01:**
  
  - **If you need English annotations, rename `data_EN.json` to `data.json`.**

- **A02:**
  
  - **Choose the language file `I18n.json`/`I18n_EN.json`.**
  
  - **If not available, translate it yourself, just ensure the filename is `I18n.json`.**

- ##### **Update Information**
  
  - Added recording of the last opened file path (program automatically generates an INI settings file for recording).
  
  - Changed `Remark` file recording method to `data` and modified data structure.
  
  - Added "Input Type Verification" with error prompts.
  
  - Added display of `Default Value` and `Adjustable Range`.
    
    - If a larger numerical value is needed, modify the `data` file accordingly.

# Features

* ##### **Interface for modifying variable values**

* ##### **Reads and edits the `PalWorldSettings.ini` file**

* ##### **Organizes the variables in the `PalWorldSettings.ini` file in the interface**

* ##### **Annotates variables with explanatory remarks**
  
  ![img03_EN](./Images/img03_EN.png)

* ##### Image Feature Explanation
  
  - **(1) Value Editing Box**
  
  - **(2) Open Last Opened File**
  
  - **(3) Load File (Select the directory containing `PalWorldSettings.ini` for automatic retrieval)**
  
  - **(4) Status of the loaded file; if editing errors occur, you can reload the original values**
  
  - **(5) Close File (Without Saving)**
  
  - **(6) Save File and Close**

# Image Introduction

![img01_EN](./Images/img01_EN.png)

![img02_EN](./Images/img02_EN.png)

# Data Source

#### **Regarding the remarks in the `data.json` file, the following sources:**

- [Palworld tech guide - Optimize game balance (palworldgame.com)](https://tech.palworldgame.com/optimize-game-balance)

- [【討論】伺服器參數難度調整分享 @幻獸帕魯 哈啦板 - 巴哈姆特 (gamer.com.tw)](https://forum.gamer.com.tw/C.php?bsn=71458&snA=227)
  
  - **Special thanks to [DANIEL6505](https://home.gamer.com.tw/daniel6505) for sharing `organization` and `testing`.**

- [【情報】伺服器參數表 (附單機模式對照) @幻獸帕魯 哈啦板 - 巴哈姆特 (gamer.com.tw)](https://forum.gamer.com.tw/C.php?bsn=71458&snA=466)
  
  - **Thanks to [jorden2895 (Jiaa)](https://home.gamer.com.tw/jorden2895) for the `reference table`.**

- [【密技】手把手教學，如何提升Dedicated Server據點帕魯數量上限的方法。（在系統預設範圍內） @幻獸帕魯 哈啦板 - 巴哈姆特 (gamer.com.tw)](https://forum.gamer.com.tw/C.php?bsn=71458&snA=661)
  
  - Regarding BaseCampWorkerMaxNum, it currently cannot exceed `20`.
  - Thanks to [ivan880613](https://home.gamer.com.tw/ivan880613) for the `step-by-step tutorial`.
    - Originally from [Dedicated Server - Increase Pal Base Worker limit? :: Palworld / 幻獸帕魯 綜合討論 (steamcommunity.com)](https://steamcommunity.com/app/1623730/discussions/0/4132683013933660878/)

- **Dependencies used come from open-source internet resources; if needed, please find them on your own. Here, only those with known sources are listed.**
  
  - [Bunny83/SimpleJSON: A simple JSON parser in C# (github.com)](https://github.com/Bunny83/SimpleJSON)
  
  - **<u>Apologies for not being able to list the sources of dependencies; feel free to find them as needed.</u>**

> ##### LICENSE `GPL-3.0 license`
