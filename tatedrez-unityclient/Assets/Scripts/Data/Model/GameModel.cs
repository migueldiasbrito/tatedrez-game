using System.Collections.Generic;
using System.Linq;

namespace Mdb.Tatedrez.Data.Model
{
    public class GameModel : IGameDataReader
    {
        public Piece?[][] BoardImplementation { get; set; }
        public IPiece?[][] Board => BoardImplementation;

        public List<Piece> UnplacedPiecesImplementation { get; set; }
        public IList<IPiece> UnplacedPieces => UnplacedPiecesImplementation.Cast<IPiece>().ToList();

        public Player CurrentPlayer { get; set; }
    }
}
