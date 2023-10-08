using JetBrains.Annotations;
using System;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class UnplacedPieceHolderUiDisplay : PieceHolderUiDisplay
    {
        private Action<UnplacedPieceHolderUiDisplay> _onSelect;

        public void Initialize(Action<UnplacedPieceHolderUiDisplay> onSelect)
        {
            _onSelect = onSelect;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        [UsedImplicitly]
        public void Select()
        {
            if (_state != PieceHolderState.Selectable) return;

            _onSelect.Invoke(this);
        }
    }
}