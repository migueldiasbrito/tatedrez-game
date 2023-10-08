using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Services.Game;
using Mdb.Tatedrez.Services.Game.Notifications;
using Mdb.Tatedrez.Services.Notifications;
using System;
using UnityEngine;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class BoardUiDisplay : MonoBehaviour
    {
        [SerializeField] private BoardCellUiDisplay[] cells;

        private IGameDataReader _gameDataReader;
        private IGameService _gameService;
        private INotificationService _notificationService;

        private IPiece _currentPieceToPlace;
        private int _currentRowSelected = -1;
        private int _currentColumnSelected = -1;

        public void Initialize(
            IGameDataReader gameDataReader,
            IGameService gameService,
            INotificationService notificationService)
        {
            _gameDataReader = gameDataReader;
            _gameService = gameService;

            _notificationService = notificationService;
            _notificationService.Subscribe<PiecePlacedNotification>(OnPiecePlaced);

            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                    cells[3 * row + column].Initialize(row, column, OnCellSelected, OnCellDeselect);
        }

        private void OnPiecePlaced(PiecePlacedNotification notification)
        {
            int row = notification.Row;
            int column = notification.Column;

            cells[3 * row + column].Populate(_gameDataReader.Board[row][column]);
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

        public void PreparePiecePlacement(IPiece piece)
        {
            _currentPieceToPlace = piece;

            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                {
                    cells[3 * row + column].SetState(_gameDataReader.CanPlacePieceAt(row, column)
                        ? PieceHolderState.Selectable : PieceHolderState.Unselectable);
                }
        }

        public void CancelPiecePlacement()
        {
            _currentPieceToPlace = null;

            SetMovablePiecesAsSelected();
        }

        private void OnCellSelected(BoardCellUiDisplay boardCellUiDisplay)
        {
            if (_currentPieceToPlace != null)
            {
                _gameService.TryPlacePieceAt(_currentPieceToPlace, boardCellUiDisplay.Row, boardCellUiDisplay.Column);
            }
            else if (boardCellUiDisplay.Piece != null)
            {
                PreparePieceMove(boardCellUiDisplay.Row, boardCellUiDisplay.Column);
            }
        }

        private void OnCellDeselect()
        {
            _currentRowSelected = -1;
            _currentColumnSelected = -1;

            SetMovablePiecesAsSelected();
        }

        public void PreparePieceMove(int selectedRow, int selectedColumn)
        {
            _currentRowSelected = selectedRow;
            _currentColumnSelected = selectedColumn;

            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                {
                    cells[3 * row + column].SetState(
                        _gameDataReader.CanMovePiece(selectedRow, selectedColumn, row, column)
                        ? PieceHolderState.Selectable : PieceHolderState.Unselectable);
                }
        }

        private void OnDestroy()
        {
            if (_notificationService == null) return;

            _notificationService.Unsubscribe<PiecePlacedNotification>(OnPiecePlaced);
        }
    }
}
