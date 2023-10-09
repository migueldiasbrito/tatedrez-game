# Tatedrez-game

The game was developed using a Model Service View architecture.

The game data, such as the current placement of pieces in the board, is implemented under the Data folder. The IGameDataReader also includes some helper methods that don't alter the data, such as GetPossibleMovesForPieceAt().

The service layer is implemented under the Services layer. The IGameService holds methods to be called by the Presentation layer which valitade operations and changes the state of the game accordingly. There's also a INotificationService which purpose is for other Services to inform subscribers in the Presentation layer of changes in the game state so they can act upon them.

To help presentation layer scripts to get data readers and services, a DataReaders and ServiceLocator static clases were developed. This are all configured by the Bootstrap script in its Awake method. Keep in mind that Data and Services scripts are independent from Unity scripts, so it's safe to set them up in Awake. The Awake, OnEnable and Start methods are not used otherwise, except in the UiSystem.

The presentation folder holds all the actual implemented MonoBehaviours in the project. A UiSystem script is used to control initialization and changes in the presentation layer. There's a splash screen, a main game screen and an end screen.

A couple of future improvements are listed here:

- Cache the results of the Board property and GetPossibleMovesForPieceAt() method in GameModel
- Usage of the IDisposable interface to deal with notification unsubscription instead of Unity OnDestroy method
- Add an Animator to PieceHolderUiDisplay and PieceUiDisplay
- Give an actual effort to the game UI
- Add Save and Load feature (although the architecture is ready for that!)
