# Simple Unity UI Controller

Simple UI Controller is a basic UI system which helps to create UI screens in Unity decoupled from code, which allows Designers and Artists to build entire screen flows without needing code and easily allows developers to add functionality after this.


<p align="center">
    <a href="https://unity3d.com/get-unity/download">
        <img src="https://img.shields.io/badge/unity-tools-blue" alt="Unity Download Link"></a>
    <a href="https://github.com/dantasulisses/Simple-Unity-UI-Controller/blob/main/LICENSE.md">
        <img src="https://img.shields.io/badge/License-MIT-brightgreen.svg" alt="License MIT"></a>
</p>


## Table of Contents
- [How it works](#How-it-works)
- [Installation](#Installation)
- [Features](#Features)
- [Support](#Support)
- [Thanks](#Thanks)
- [License](#License)


## How it works

The Simple UI Controller is based on two components:
- The "UIPagesController": Which has references to individual UIPages and broadcasts to then which is the current UI state the game should be
- The "UIPage": Which represents an actual UI element group in your UI. The UIPage defines at which states this group should be on or off, and does the transitions properly when receives the messages by the UIPagesController

```csharp
    //Example 1: Let's say in your game you have a top header containing player currency 
    //and stats, and a bottom menu where the player can buy things when the shop is open

    You will have two GameObjects, each with an UIPage component.
    The first, the header, will be set to as shownOnPages: MAIN_MENU and BUY_MENU
    The second, the bottom buy menu, will be set as showOnPages: BUY_MENU

    In a third object, you will have the UIPagesController, which has references to both pages.

    In your game, you will send the "ChangePage" event to the UIPagesController(this can be done through the UIPage too), if the value of this
    call is "MAIN_MENU" if the Header object is "out" of screen, it will do its transitions and fire a UnityEvent saying it is now showing. The bottom menu will still be out of screen.
    When calling the ChangePage again, with the value "BUY_MENU", the header will stay on screen (and not fire any event) and the Bottom Menu will do it's enter transition and call the Enter event.

    When calling ChangePage again, with any other value, all the elements will quit the screen and trigger their "OnExit" events
```

Currently the "PAGES" property is a String, which allows for anyone create UI flows without opening scripts and allows for the package extension without editing the source code. But for usage and calls through code is recommended to create Constants to enforce correct entries in code.


## Installation

Simple UI Transitions can be installed directly through the git url
```
https://github.com/dantasulisses/Simple-Unity-UI-Controller
```

If you need more information about installing package from a Git URL, you can click [here](https://docs.unity3d.com/Manual/upm-ui-giturl.html). :slightly_smiling_face:


## Dependencies

Simple Unity UI Controller uses the Simple-UI-Transitions (https://github.com/dantasulisses/Simple-UI-Transitions)
The dependency is not explicetly set in the package.json because this allow anyone to create their own versions of both packages without having to deal with forced unnecessary package-hell-like dependencies

Simple UI Controller can also be used with the Uli-Extensions, which allows for global events communication, for further decoupling at architectural level

Simple UI Controller also has some useful In-Editor-Buttons for turning pages on and off, along with creating positions references; These are only available if you have OdinInspector (https://odininspector.com/) in your project


## Features

Currently, this is what Simple UI Controller does have

| Features                                                     |       Status      |
| ------------------------------------------------------------ | :----------------:|
| Allow creation of full animated UI flows without code        |         ✔️        |
| Provides easy to call functions for UI flow control          |         ✔️        |
| Provides callbacks to allow code to react to UI show or hide |         ✔️        |
| Can be used alone, multiple or listen to events              |         ✔️        |
| Control PageState through constant (enum) values             |         ❌        |



## Support
Please submit any queries, bugs or issues, to the [Issues](https://github.com/dantasulisses/Simple-Unity-UI-Controller/issues) page on this repository. All feedback is appreciated as it not just helps myself find problems I didn't otherwise see, but also helps improve the project.


## Thanks
My friends and family, and you for having come here!


## License
Copyright (c) 2021-present Ulisses Dantas and Contributors. Simple UI Transitions is free and open-source software licensed under the [MIT License](https://github.com/dantasulisses/Simple-Unity-UI-Controller/blob/main/LICENSE.md).