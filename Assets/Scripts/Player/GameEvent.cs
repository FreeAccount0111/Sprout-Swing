using System;
using UnityEngine;
using Util;

namespace Player
{
    public class GameEvent : MonoBehaviour
    {
        public static event Action<PlayerType> OnChangeType;
        public static event Action<Vector2> OnStick;
        public static event Action<BallType> OnSelectBall;

        public static void RaiseChangeType(PlayerType playerType) => OnChangeType?.Invoke(playerType);
        public static void RaiseStick(Vector2 force) => OnStick?.Invoke(force);
        public static void RaiseSelectBall(BallType ballType) => OnSelectBall?.Invoke(ballType);
    }
}
