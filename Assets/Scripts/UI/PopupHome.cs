using Loading;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopupHome : BasePopup
    {
        [SerializeField] private Button btnPlay, btnGuid;

        private void Awake()
        {
            btnPlay.onClick.AddListener(() =>
            {
                HideImmediately(true);
                PopupCtrl.Instance.GetPopupByType<PopupLevel>().ShowImmediately(false);
            });

            btnGuid.onClick.AddListener(() =>
            {
                PopupCtrl.Instance.GetPopupByType<PopupGuid>().ShowImmediately(false);
            });
        }
    }
}
