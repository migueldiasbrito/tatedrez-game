using Mdb.Tatedrez.Data.Model;

namespace Mdb.Tatedrez.Services.Game
{
    public interface IGameService : IService
    {
        void StartNewGame();
        void TryPlacePieceAt(IPiece piece, int row, int column);
        void TryMovePiece(int fromRow, int fromColumn, int toRow, int toColumn);
    }
}
