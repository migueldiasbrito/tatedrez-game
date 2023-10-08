using System.Collections.Generic;

namespace Mdb.Tatedrez.Data.Model
{
    public interface IGameDataReader : IDataReader
    {
        IPiece?[][] Board { get; }
        IList<IPiece> UnplacedPieces { get; }
        Player CurrentPlayer { get; }
    }
}