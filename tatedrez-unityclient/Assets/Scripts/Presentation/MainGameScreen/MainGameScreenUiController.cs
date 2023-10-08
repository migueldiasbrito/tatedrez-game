using Mdb.Tatedrez.Data;
using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays;
using Mdb.Tatedrez.Services;
using Mdb.Tatedrez.Services.Game;
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

        public override void Initialize(UiSystem uiSystem)
        {
            base.Initialize(uiSystem);

            _gameDataReader = DataReaders.Get<IGameDataReader>();

            _gameService = ServiceLocator.Get<IGameService>();

            _board.Initialize(_gameDataReader);
            _unplacedPiecesHolders.Initialize(OnUnplacedPieceSelected);
        }

        private void OnUnplacedPieceSelected(IPiece piece)
        {
            _board.TryPlacePiece(piece);
        }

        public override void Show()
        {
            _gameService.StartNewGame();

            _board.SetMovablePiecesAsSelected();
            _unplacedPiecesHolders.Populate(_gameDataReader.UnplacedPieces
                .Where(x => x.Player == _gameDataReader.CurrentPlayer).ToList());

            base.Show();
        }
    }
}
