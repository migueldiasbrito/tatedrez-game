using JetBrains.Annotations;
using System;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class UnplacedPieceHolderUiDisplay : PieceHolderUiDisplay
    {
        private Action<UnplacedPieceHolderUiDisplay> _onSelect;
        private Action _onDeselect;

        public void Initialize(Action<UnplacedPieceHolderUiDisplay> onSelect, Action onDeselect)
        {
            _onSelect = onSelect;
            _onDeselect = onDeselect;
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
            switch (_state)
            {
                case PieceHolderState.Selectable:
                    _onSelect.Invoke(this);
                    break;
                case PieceHolderState.Selected:
                    _onDeselect.Invoke();
                    SetState(PieceHolderState.Selectable);
                    break;
            }
        }
    }
}