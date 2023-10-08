using Mdb.Tatedrez.Data.Model;

namespace Mdb.Tatedrez.Services.Game
{
    public interface IGameService : IService
    {
        void StartNewGame();
        void TryPlacePieceAt(IPiece currentPieceToPlace, int row, int column);
    }
}
