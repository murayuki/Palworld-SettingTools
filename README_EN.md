# Palworld Server Setting Editor

### Created for convenient modification of settings; some code may not be very user-friendly.

### Feel free to use it, but I cannot guarantee there are no errors. It has been tested and works fine for me.

### [Releases](https://github.com/murayuki/PalWorld-SettingTools/releases)

# Notes

> All translations with `_EN` in `json` are provided by `ChatGPT`.

- **A01:**
  
  - **For English comments, rename `Remark_EN.json` to `Remark.json`.**

- **A02:**
  
  - ~~**The program language is `Traditional Chinese`.**~~
  
  - ~~**If you need English, construct the `EXE executable file` yourself. Basic knowledge of C# and basic operations in Visual Studio (developed using 2022) is required.**~~
  
  - **Choose the language file `I18n.json`/`I18n_EN.json` yourself.**
  
  - **If not available, translate it yourself; just ensure the file name is `I18n.json`.**

# Features

* ##### **Interface-based modification of variable values.**

* ##### **Read and edit the `PalWorldSettings.ini` file.**

* ##### **Interface-based sorting of variables in the `PalWorldSettings.ini` file.**
  
* ##### **Annotate variables with remarks.**

  ![img03_EN](./Images/img03_EN.png)

* ##### Image Feature Overview
  
  - **(1) Numeric input box**
  
  - **(2) Load File (Simply select the directory of `PalWorldSettings.ini` to automatically retrieve it)**
  
  - **(3) File loaded status; if editing goes wrong, you can reload the original values.**
  
  - **(4) Close file (without saving)**
  
  - **(5) Save file and close**

# Image Overview

![img03_EN](./Images/img01_EN.png)

![img03_EN](./Images/img02_EN.png)

# Data Sources

#### **About the remarks in the `Remark.json` file:**

- [Palworld tech guide - Optimize game balance (palworldgame.com)](https://tech.palworldgame.com/optimize-game-balance)

- [【Discussion】Server parameter difficulty adjustment sharing @Palworld - Bahamut (gamer.com.tw)](https://forum.gamer.com.tw/C.php?bsn=71458&snA=227)
  
  - **Special thanks to [DANIEL6505](https://home.gamer.com.tw/daniel6505) for sharing, organizing, and testing on Bahamut Forum.**

- **Dependencies are sourced from open network resources; if needed, please find them yourself. Only sources I remember are listed here.**
  
  - [Bunny83/SimpleJSON: A simple JSON parser in C# (github.com)](https://github.com/Bunny83/SimpleJSON)
  
  - **<u>Apologies for not being able to list sources for dependencies I can't recall.</u>**

> ##### LICENSE `GPL-3.0 license`
