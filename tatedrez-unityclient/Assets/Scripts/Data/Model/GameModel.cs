using System.Collections.Generic;
using System.Linq;

namespace Mdb.Tatedrez.Data.Model
{
    public class GameModel : IGameDataReader
    {
        public Piece[][] BoardImplementation { get; set; }
        public IPiece[][] Board => BoardImplementation;

        public List<Piece> UnplacedPiecesImplementation { get; set; }
        public IList<IPiece> UnplacedPieces => UnplacedPiecesImplementation.Cast<IPiece>().ToList();

        public Player CurrentPlayer { get; set; }

        public bool CanMovePieceAt(int row, int column)
        {
            Piece piece = BoardImplementation[row][column];
            if (piece == null) return false;

            if (UnplacedPiecesImplementation.Any(x => x.Player == piece.Player))
                return false;

            // TODO Move validation
            return true;
        }

        public bool CanPlacePieceAt(int row, int column)
        {
            return BoardImplementation[row][column] == null;
        }
    }
}
