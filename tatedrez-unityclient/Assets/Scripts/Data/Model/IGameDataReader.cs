using System.Collections.Generic;

namespace Mdb.Tatedrez.Data.Model
{
    public interface IGameDataReader : IDataReader
    {
        IPiece[][] Board { get; }
        IList<IPiece> UnplacedPieces { get; }
        Player CurrentPlayer { get; }

        bool CanMovePieceAt(int row, int column);
        bool CanPlacePieceAt(int row, int column);
    }
}