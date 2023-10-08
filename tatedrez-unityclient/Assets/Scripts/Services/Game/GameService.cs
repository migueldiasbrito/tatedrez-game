using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Services.Game.Notifications;
using Mdb.Tatedrez.Services.Notifications;
using System.Collections.Generic;
using UnityEngine;

namespace Mdb.Tatedrez.Services.Game
{
    public class GameService : IGameService
    {
        private GameModel _model;

        private INotificationService _notificationService;

        public GameService(GameModel game, INotificationService notificationService)
        {
            _model = game;

            _notificationService = notificationService;
        }

        public void StartNewGame()
        {
            _model.BoardImplementation = new Piece?[][] { new Piece?[3], new Piece?[3], new Piece?[3] };

            _model.UnplacedPiecesImplementation = new List<Piece>
            {
                new Piece(PieceType.Rook, Player.White),
                new Piece(PieceType.Bishop, Player.White),
                new Piece(PieceType.Knight, Player.White),
                new Piece(PieceType.Rook, Player.Black),
                new Piece(PieceType.Bishop, Player.Black),
                new Piece(PieceType.Knight, Player.Black)
            };

            _model.CurrentPlayer = Random.Range(0, 2) == 0 ? Player.White : Player.Black;
        }

        public void TryPlacePieceAt(IPiece piece, int row, int column)
        {
            if (!_model.CanPlacePieceAt(row, column)) return;

            if (piece.Player != _model.CurrentPlayer) return;

            if (!_model.UnplacedPiecesImplementation.Contains((Piece) piece)) return;

            _model.BoardImplementation[row][column] = (Piece) piece;
            _model.UnplacedPiecesImplementation.Remove((Piece) piece);

            _notificationService.Publish(new PiecePlacedNotification(row, column));

            EndPlay();
        }

        private void EndPlay()
        {
            // TODO Check victory condition

            _model.CurrentPlayer = _model.CurrentPlayer == Player.White ? Player.Black : Player.White;

            _notificationService.Publish(new StartNewPlayerTurnNotification(_model.CurrentPlayer));
        }
    }
}
