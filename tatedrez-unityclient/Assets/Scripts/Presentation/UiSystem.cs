using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace mdb.tatedrez.presentation
{
    public class UiSystem : MonoBehaviour
    {
        [SerializeField] private UiState _initialState = UiState.Splash;
        [SerializeField, SerializedDictionary("UiState", "UiController")]
        public SerializedDictionary<UiState, UiController> _uiControllers = new();

        private UiState _currentState;

        private void Start()
        {
            foreach ((UiState state, UiController controller) in _uiControllers)
            {
                controller.Register(this);

                if (state == _initialState) continue;
                controller.Hide();
            }

            _uiControllers[_initialState].Show();
            _currentState = _initialState;
        }

        public void LoadState(UiState state)
        {
            if (state == _currentState) return;

            _uiControllers[_currentState].Hide();
            _uiControllers[state].Show();
            _currentState = state;
        }
    }
}
