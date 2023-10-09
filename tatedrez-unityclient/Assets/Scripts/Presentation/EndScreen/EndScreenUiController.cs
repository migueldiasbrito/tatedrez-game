using JetBrains.Annotations;
using Mdb.Tatedrez.Data;
using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays;
using System.Collections.Generic;
using UnityEngine;

namespace Mdb.Tatedrez.Presentation.EndScreen
{
    public class EndScreenUiController : UiController
    {
        [SerializeField] private List<PieceHolderUiDisplay> _board = default;

        private IGameDataReader _gameDataReader;

        public override void Initialize(UiSystem uiSystem)
        {
            base.Initialize(uiSystem);

            _gameDataReader = DataReaders.Get<IGameDataReader>();
        }

        public override void Show()
        {
            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                {
                    IPiece piece = _gameDataReader.Board[row][column];
                    PieceHolderUiDisplay pieceHolder = _board[row * 3 + column];

                    pieceHolder.Populate(piece);
                    pieceHolder.SetState(piece != null && piece.Player == _gameDataReader.CurrentPlayer
                        ? PieceHolderState.Selectable : PieceHolderState.Unselectable);
                }

            base.Show();
        }

        [UsedImplicitly]
        public void ResetGame()
        {
            _uiSystem.LoadState(UiState.MainGame);
        }
    }
}
