using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Services.Notifications;

namespace Mdb.Tatedrez.Services.Game.Notifications
{
    public readonly struct StartNewPlayerTurnNotification : INotification
    {
        public Player Player { get; }

        public StartNewPlayerTurnNotification(Player player)
        {
            Player = player;
        }
    }
}
