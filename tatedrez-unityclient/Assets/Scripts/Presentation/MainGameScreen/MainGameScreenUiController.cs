using Mdb.Tatedrez.Data;
using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays;
using Mdb.Tatedrez.Services;
using Mdb.Tatedrez.Services.Game;
using Mdb.Tatedrez.Services.Game.Notifications;
using Mdb.Tatedrez.Services.Notifications;
using System.Linq;
using UnityEngine;

namespace Mdb.Tatedrez.Presentation.MainGameScreen
{
    public class MainGameScreenUiController : UiController
    {
        [SerializeField] private BoardUiDisplay _board = default;
        [SerializeField] private UnplacedPiecesHoldersUiDisplay _unplacedPiecesHolders = default;

        private IGameDataReader _gameDataReader;
        private IGameService _gameService;
        private INotificationService _notificationService;

        public override void Initialize(UiSystem uiSystem)
        {
            base.Initialize(uiSystem);

            _gameDataReader = DataReaders.Get<IGameDataReader>();

            _gameService = ServiceLocator.Get<IGameService>();
            _notificationService = ServiceLocator.Get<INotificationService>();

            _notificationService.Subscribe<StartNewPlayerTurnNotification>(OnStartNewPlayerTurn);

            _board.Initialize(_gameDataReader, _gameService, _notificationService);
            _unplacedPiecesHolders.Initialize(OnUnplacedPieceSelected, OnUnplacedPieceDeselected);
        }

        private void OnStartNewPlayerTurn(StartNewPlayerTurnNotification notification)
        {
            PopulateNewTurn();
        }

        private void PopulateNewTurn()
        {
            _board.SetMovablePiecesAsSelected();
            _unplacedPiecesHolders.Populate(_gameDataReader.UnplacedPieces
                .Where(x => x.Player == _gameDataReader.CurrentPlayer).ToList());
        }

        private void OnUnplacedPieceSelected(IPiece piece)
        {
            _board.PreparePiecePlacement(piece);
        }
        private void OnUnplacedPieceDeselected()
        {
            _board.CancelPiecePlacement();
        }

        public override void Show()
        {
            _gameService.StartNewGame();

            PopulateNewTurn();

            base.Show();
        }

        private void OnDestroy()
        {
            if (_notificationService == null) return;

            _notificationService.Unsubscribe<StartNewPlayerTurnNotification>(OnStartNewPlayerTurn);
        }
    }
}
