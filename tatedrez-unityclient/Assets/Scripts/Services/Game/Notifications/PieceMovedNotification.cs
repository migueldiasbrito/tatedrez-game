using Mdb.Tatedrez.Services.Notifications;

namespace Mdb.Tatedrez.Services.Game.Notifications
{
    public readonly struct PieceMovedNotification : INotification
    {
        public int FromRow { get; }
        public int FromColumn { get; }
        public int ToRow { get; }
        public int ToColumn { get; }
        
        public PieceMovedNotification(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            FromRow = fromRow;
            FromColumn = fromColumn;
            ToRow = toRow;
            ToColumn = toColumn;
        }
    }
}
