using Mdb.Tatedrez.Data.Model;
using UnityEngine;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class BoardUiDisplay : MonoBehaviour
    {
        [SerializeField] private BoardCellUiDisplay[] cells;

        private IGameDataReader _gameDataReader;
        private IPiece _currentMovingPiece;

        public void Initialize(IGameDataReader gameDataReader)
        {
            _gameDataReader = gameDataReader;
        }

        public void SetMovablePiecesAsSelected()
        {
            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                {
                    cells[3 * row + column].SetState(_gameDataReader.CanMovePieceAt(row, column)
                        ? PieceHolderState.Selectable : PieceHolderState.Unselectable);
                }
        }

        public void TryPlacePiece(IPiece piece)
        {
            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                {
                    cells[3 * row + column].SetState(_gameDataReader.CanPlacePieceAt(row, column)
                        ? PieceHolderState.Selectable : PieceHolderState.Unselectable);
                }
        }
    }
}
