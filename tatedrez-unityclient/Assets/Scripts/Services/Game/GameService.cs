using Mdb.Tatedrez.Data.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Mdb.Tatedrez.Services.Game
{
    public class GameService : IGameService
    {
        private GameModel _model;

        public GameService(GameModel game)
        {
            _model = game;
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
    }
}
