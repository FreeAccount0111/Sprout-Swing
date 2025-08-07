using System;
using Player;
using UnityEngine;
using Util;

namespace UI
{
    public class MoveBallBtn : ButtonSelect
    {
        [SerializeField] private PlayerType playerType;

        private void Start()
        {
            btn.onClick.AddListener(() =>
            {
                GameEvent.RaiseChangeType(playerType);
            });
        }
    }
}
