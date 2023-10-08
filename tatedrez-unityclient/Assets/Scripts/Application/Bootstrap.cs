using Mdb.Tatedrez.Data;
using Mdb.Tatedrez.Data.Model;
using Mdb.Tatedrez.Services;
using Mdb.Tatedrez.Services.Game;
using UnityEngine;

namespace Mdb.Tatedrez.Application
{
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            GameModel game = new GameModel();
            DataReaders.Bind<IGameDataReader>(game);

            GameService gameService = new GameService(game);
            ServiceLocator.Bind<IGameService>(gameService);
        }
    }
}
