using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonSelect : MonoBehaviour
    {
        [SerializeField] private GroupSelect groupSelect;
        [SerializeField] private Sprite iconOn, iconOff;
        [SerializeField] protected Button btn;

        protected void Awake()
        {
            btn.onClick.AddListener(() =>
            {
                if (groupSelect != null)
                {
                    groupSelect.SelectButton(this);
                }
            });
        }

        public void ActiveSelect(bool en)
        {
            btn.image.sprite = en ? iconOn : iconOff;
        }
    }
}
