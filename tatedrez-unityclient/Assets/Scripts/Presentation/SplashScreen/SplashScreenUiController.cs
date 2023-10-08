using JetBrains.Annotations;

namespace Mdb.Tatedrez.Presentation.SplashScreen
{
    public class SplashScreenUiController : UiController
    {
        [UsedImplicitly]
        public void StartGame()
        {
            _uiSystem.LoadState(UiState.MainGame);
        }
    }
}
