using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Services.Game;
using Mdb.Tatedrez.Services.Game.Notifications;
using Mdb.Tatedrez.Services.Notifications;
using System;
using System.Collections.Generic;
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
            _notificationService.Subscribe<PieceMovedNotification>(OnPieceMoved);

            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                    cells[3 * row + column].Initialize(row, column, OnCellSelected, OnCellDeselect);
        }

        private void OnPiecePlaced(PiecePlacedNotification notification)
        {
            int row = notification.Row;
            int column = notification.Column;

            cells[3 * row + column].Populate(_gameDataReader.Board[row][column]);

            _currentPieceToPlace = null;
        }

        private void OnPieceMoved(PieceMovedNotification notification)
        {
            int row = notification.FromRow;
            int column = notification.FromColumn;
            cells[3 * row + column].Populate(_gameDataReader.Board[row][column]);

            row = notification.ToRow;
            column = notification.ToColumn;
            cells[3 * row + column].Populate(_gameDataReader.Board[row][column]);

            _currentRowSelected = -1;
            _currentColumnSelected = -1;
        }

        public void Clear()
        {
            foreach (BoardCellUiDisplay cell in cells)
                cell.Populate(null);
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
                return;
            }

            if (_currentRowSelected >= 0 && _currentColumnSelected >= 0)
            {
                _gameService.TryMovePiece(_currentRowSelected, _currentColumnSelected, boardCellUiDisplay.Row,
                    boardCellUiDisplay.Column);
                return;
            }
            
            if (boardCellUiDisplay.Piece != null)
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

            IList<(int, int)> movableCells = _gameDataReader.GetPossibleMovesForPieceAt(selectedRow, selectedColumn);

            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                    if (row == _currentRowSelected && column == _currentColumnSelected)
                        cells[3 * row + column].SetState(PieceHolderState.Selected);
                    else
                        cells[3 * row + column].SetState(movableCells.Contains((row, column))
                            ? PieceHolderState.Selectable : PieceHolderState.Unselectable);
        }

        private void OnDestroy()
        {
            if (_notificationService == null) return;

            _notificationService.Unsubscribe<PiecePlacedNotification>(OnPiecePlaced);
            _notificationService.Unsubscribe<PieceMovedNotification>(OnPieceMoved);
        }
    }
}
