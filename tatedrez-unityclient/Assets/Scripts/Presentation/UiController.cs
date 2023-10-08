using UnityEngine;

namespace Mdb.Tatedrez.Presentation
{
    public class UiController : MonoBehaviour
    {
        protected UiSystem _uiSystem;

        public virtual void Initialize(UiSystem uiSystem)
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
