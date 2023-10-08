using Mdb.Tatedrez.Services.Notifications;

namespace Mdb.Tatedrez.Services.Game.Notifications
{
    public readonly struct PiecePlacedNotification : INotification
    {
        public int Row { get; }
        public int Column { get; }

        public PiecePlacedNotification(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}