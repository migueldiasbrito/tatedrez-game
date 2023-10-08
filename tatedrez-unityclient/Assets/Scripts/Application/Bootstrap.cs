using Mdb.Tatedrez.Data;
using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Services;
using Mdb.Tatedrez.Services.Game;
using Mdb.Tatedrez.Services.Notifications;
using UnityEngine;

namespace Mdb.Tatedrez.Application
{
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            NotificationService notificationService = new NotificationService();
            ServiceLocator.Bind<INotificationService>(notificationService);

            GameModel game = new GameModel();
            DataReaders.Bind<IGameDataReader>(game);

            GameService gameService = new GameService(game, notificationService);
            ServiceLocator.Bind<IGameService>(gameService);
        }
    }
}
