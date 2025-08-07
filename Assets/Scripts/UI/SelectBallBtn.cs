using System;
using Player;
using UnityEngine;
using Util;

namespace UI
{
    public class SelectBallBtn : ButtonSelect
    {
        [SerializeField] private PlayerType playerType;
        [SerializeField] private BallType typeBall;

        private void Start()
        {
            btn.onClick.AddListener(() =>
            {
                GameEvent.RaiseChangeType(playerType);
                GameEvent.RaiseSelectBall(typeBall);
            });
        }
    }
}
