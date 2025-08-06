using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GroupSelect : MonoBehaviour
    {
        private ButtonSelect[] _btns;

        private void Awake()
        {
            _btns = transform.GetComponentsInChildren<ButtonSelect>();
        }

        public void SelectButton(ButtonSelect btn)
        {
            foreach (var b in _btns)
            {
                b.ActiveSelect(b == btn);
            }
        }
    }
}
