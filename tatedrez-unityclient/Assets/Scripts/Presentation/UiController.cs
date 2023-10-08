using UnityEngine;

namespace mdb.tatedrez.presentation
{
    public class UiController : MonoBehaviour
    {
        protected UiSystem _uiSystem;

        public void Register(UiSystem uiSystem)
        {
            _uiSystem = uiSystem;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
