using JetBrains.Annotations;

namespace mdb.tatedrez.presentation.maingamescreen
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
