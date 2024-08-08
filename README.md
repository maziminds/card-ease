# CardEase Documentation

## Table of Contents
1. [Introduction](#introduction)
2. [Installation](#installation)
3. [Getting Started](#getting-started)
4. [How to](#how-to)
   - [Create Prefabs](#create-prefabs)
   - [Adjust Spacing](#adjust-spacing)
   - [Handling Selection](#handle-selection)
5. [Classes](#classes)
   - [CardModel](#card-model)
   - [CardZoneManager](#card-zone-manager)
   - [CardGroupManager](#card-group-manager)
   - [CardManager](#card-manager)
   - [EventManager](#event-manager)

## Introduction
**CardEase** is a 2D Unity asset that provides simple drag-and-drop functionality with a high level of customization. It allows users to implement basic drag-and-drop features for cards, organize cards into groups, adjust spacing between cards, and customize prefabs to there likings. CardEase removes the complexity of creating drag-and-drop systems from scratch while giving you total control over each game object.

## Demo
- you can try the demo of asset on itch [here](https://maziminds.itch.io/card-ease)

## Installation
1. Download the CardEase package from the [Unity Asset Store](https://assetstore.unity.com/packages/2d/gui/cardease-drag-drop-card-system-284789).
2. Open your Unity project.
3. Import the CardEase package into your project(it will be imported in `Maziminds/CardEase` folder).

## Support
1. if you found any bug/issue in package or wants to request any update then please open an issue from [here](https://github.com/maziminds/card-ease/issues)
2. if you like this project and wants to support it by donation then you can do it from [here](https://github.com/sponsors/mazimez)

## Getting Started

- The package mainly has 2 folders: `Demo` and `src`. The folder contains some demo scenes and prefabs that you can use to test the functionality and get familiar with the package, you can change anything in that folder to test the functionalities. this folder has 3 folders `V1` ,`V2` and `V3` in it, `V1` folder shows a basic demo of package with default settings and `V2` folder shows a bit customized demo with some customizations examples and `V3` shows the latest update with Event system.
- The `src` folder contains the source code for the package, it contains all core scripts for drag-drop and other features. if you want to change anything in that folder, you can do that(only if you know what you are doing).
- This package works with [RectTransform](https://docs.unity3d.com/ScriptReference/RectTransform.html) and [Horizontal Layout Group](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/script-HorizontalLayoutGroup.html) to show an organized deck of cards that you drag around in the scene. The package simply provides you with some abstract classes(scripts) that you can extends and customize. by default it gives basic drag-drop functionality with events on different actions like card pick/drop or select/deselect and you can add your own customization by extending it.
- The package mainly contains 3 things: 
   - `CardZone` : this represent the place or area where cards can be placed. it's mostly a rectangular `GameObject`.
   - `CardGroup` : this is a child of `CardZone`, that has a group of cards in it. 1 `CardZone` can have multiple `CardGroup` but 1 `CardGroup` can only be a child of 1 `CardZone`.
   - `Card` : This is a child of `CardGroup`, this represents a single card that can be dragged around in between different `CardGroup` and `CardZone`.

- once you are familiar with the package, you can delete the `Demo` folder and start using the package in your game.


## How to

### Create Prefabs
- You will mainly need 3 prefabs, one for each `CardZone`, one for each `CardGroup`, and one for each `Card`. there will be 1 more Prefab called  `CardPlaceholder` that will be used as a placeholder for cards when they are being dragged.
   - `CardZone`: this prefab needs to have [RectTransform](https://docs.unity3d.com/ScriptReference/RectTransform.html) and [Horizontal Layout Group](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/script-HorizontalLayoutGroup.html) attached to it. plus 1 `Image` element to show the Card Zone in game. 
      - this prefab will also need a [CardZoneManager](src/Managers/CardZoneManager.cs) script attached to it(you have to first extend it on another class).
      - it's important to note that you can't add any child to this element, since it has a `HorizontalLayoutGroup` attached to it, only child it should have is `CardGroup`.
      -  You can checkout `Demo` folder for more detailed info.
   - `CardGroup`: this prefab also needs to have [RectTransform](https://docs.unity3d.com/ScriptReference/RectTransform.html) and [Horizontal Layout Group](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/script-HorizontalLayoutGroup.html) attached to it. plus 1 `Image` element to show the Card Group in game.
      - This prefab will also need a [CardGroupManager](src/Managers/CardGroupManager.cs) script attached to it(you have to first extend it on another class).
      - it's important to note that you can't add any child to this element, since it has a `HorizontalLayoutGroup` attached to it, only child it should have is `Card`.
      -  You can checkout `Demo` folder for more detailed info.
   - `Card`: this prefab also needs to have [RectTransform](https://docs.unity3d.com/ScriptReference/RectTransform.html) and [Layout Element](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/script-LayoutElement.html), [Canvas Group](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/class-CanvasGroup.html) attached to it. plus 1 `Image` element to show the Card in game.
      - This prefab will also need a [CardManager](src/Managers/CardManager.cs) script attached to it(you have to first extend it on another class).
      - this is the last child so you can add any child on this `GameObject`. you can customize it as you want.
      -  You can checkout `Demo` folder for more detailed info.
### Adjust Spacing
   - `CardZone` and `CardGroup` both's scripts have a `Min Spacing` and `Max Spacing` properties that you can use to adjust the spacing between cards and cardGroups.
   - this values will be used by the [CardZoneManager](src/Managers/CardZoneManager.cs) and [CardGroupManager](src/Managers/CardGroupManager.cs) scripts in functions `RefreshCardZone`,`RefreshCardGroup` to adjust the spacing between cards and cardGroups.
### Handle Selection
   - By default the card select feature is not enabled, but you can do that by first adding some `Button` or `click listener` to the `Card` prefab. that can call the `UpdateSelection` method in the [CardManager](src/Managers/CardManager.cs) script. from there you can decide how card should look while it's selected.
   - you can also use `GroupSelectedCards` method from [CardZoneManager](src/Managers/CardZoneManager.cs) to create a new group of selected cards.

## Classes

### Card Model
- This [CardModel](src/Models/CardModel.cs) is an Abstract class that you can extend with your own custom script. this class serves as a `DataContainer` that you can use to store any custom values you want to be applied to the card. for example you can have property `name` or `value`,`points` that shows how many points or value does any card have.
- Once Extended, you can use this class in other classes like [CardManager](src/Managers/CardManager.cs), [CardZoneManager](src/Managers/CardZoneManager.cs) and [CardGroupManager](src/Managers/CardGroupManager.cs).

### Card Zone Manager
- This [CardZoneManager](src/Managers/CardZoneManager.cs) is an Abstract class that you can extend with your own custom script. that extended script should be attached to a `CardZone` prefab. this Abstract class implements classes like `MonoBehaviour`, `IPointerEnterHandler` to handle some drag-drop functions.
- while extending, pass the [CardModel's](src/Models/CardModel.cs) extended class. something like `V1CardZoneManager: CardEase.CardZoneManager<V1CardModel>`. here the `V1CardModel` is the extended class for [CardModel](src/Models/CardModel.cs) and `V1CardZoneManager` is the extended class for [CardZoneManager](src/Managers/CardZoneManager.cs).
- Once Extended, make sure you `override` the `Awake` and called the `base.Awake()` before adding your own custom logic. also update the values like `cardGroupPrefab` and `minSpacing`,`maxSpacing` according to your needs. there are some methods in this class that you can use.
   - `AddGroup`: This method creates a new empty group in the card zone. If a list of cards is provided, those cards will be added to the new group as well
   - `GetAllCards`: This method will get the list of all cards in that `CardZone`. no matter how many `CardGroup` are in that zone, it will take of the cards in them.
   - `GroupSelectedCards`: This method will make a new group of selected cards and return the group. make sure that your `Card` prefab has some `click listener` attached to it.
   - `RefreshCardZone`: This method can arrange the elements(cardGroups) in it so it will cover the screen area properly

### Card Group Manager
- This [CardGroupManager](src/Managers/CardGroupManager.cs) is an Abstract class that you can extend with your own custom script. that extended script should be attached to a `CardGroup` prefab.  this Abstract class implements classes like `MonoBehaviour`, `IPointerEnterHandler` to handle some drag-drop functions.
- while extending, pass the [CardModel's](src/Models/CardModel.cs) extended class.
- Once Extended, make sure you `override` the `Awake` and called the `base.Awake()` before adding your own custom logic. also update the values like `cardPrefab` and `minSpacing`,`maxSpacing` according to your needs. there are some methods in this class that you can use.
   - `AddCard`: This method creates a new card in that `CardGroup`. it takes a class that extend [CardModel](src/Models/CardModel.cs) model that contains any custom values you want to be applied to the card.
   - `RefreshCardGroup`: This method can arrange the elements(cards) in it so it will cover the screen area properly\

### Card Manager
- This [CardManager](src/Managers/CardManager.cs) is also an Abstract class that you can extend with your own custom script. that extended script should be attached to a `Card` prefab. this Abstract class implements classes like `MonoBehaviour`, `IBeginDragHandler`,`IDragHandler`,`IEndDragHandler` to handle some drag-drop functions.
- while extending, pass the [CardModel's](src/Models/CardModel.cs) extended class.
- Once Extended, you need to override 2 methods `SetData` and `UpdateSelection`. you can add any other custom element to the `Card` prefab to suites your requirements.
   - `SetData`: This method will set the data of the card. it takes a class that extend [CardModel](src/Models/CardModel.cs) model that contains any custom values you want to be applied to the card. then you can use this values to render your card. you can see it's example in [V2CardManager](Demo/V2/Scripts/Managers/V2CardManager.cs)
   - `UpdateSelection`: This method will update the card selection. it takes a Boolean value that indicates if the card is selected or not. while overriding this method, make sure you update `isSelected` property by `this.isSelected = isSelected;` then you can add any other custom code that change card's look while it's selected. you can see it's example in [V2CardManager](Demo/V2/Scripts/Managers/V2CardManager.cs)

### Event Manager
- This [EventManager](src/Managers/EventManager.cs) is a Generic type class that contains a list of events that can be invoked and listened by any script/class in whole game. 
- this events are supposed to be used to make the UI/GAME interact while the player is performing some action on cards. for example, if you want to do some action whenever the player pick/drop a card and you can listen to it's event. similarly there is an event for card select/deselect as well.
- by default it wont fire any events but you can decide which event to fire/invoke and which event to listen, for example:
   - `SELECT/DESELECT events`: take a look at [V3CardManager](Demo/V3/Scripts/Managers/V3CardManager.cs), in `UpdateSelection` method we are invoking events like `CARD_SELECTED` and `CARD_DESELECTED` whenever the card is selected/deselected.
      - this events are the listened by [V3Controller](Demo/V3/Scripts/Controllers/V3Controller.cs) with `AddListener` method and then we can perform some tasks on UI like showing the list of all selected cards in the UI.
   - `PICK/DROP events`: in  [V3CardManager](Demo/V3/Scripts/Managers/V3CardManager.cs), we override 2 more methods `CardPicked` and `CardDropped` that's called when any card is picked/dropped and then we invoked events like `CARD_PICKED` and `CARD_DROPPED` from there.
      - this events also listened by [V3Controller](Demo/V3/Scripts/Controllers/V3Controller.cs) with `AddListener` method and then we can perform some tasks on UI like highlighting the group of card by it's color or any other logic you want to perform.