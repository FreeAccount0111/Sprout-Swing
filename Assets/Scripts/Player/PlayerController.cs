using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerType playerType;
        [SerializeField] private BaseState currentState;

        [SerializeField] private PlayerStickState playerStickState;
        [SerializeField] private PlayerSelectState playerSelectState;

        private void OnEnable()
        {
            GameEvent.OnChangeType += ChangeState;
            GameEvent.OnSelectBall += SelectBallType;
        }

        private void OnDisable()
        {
            GameEvent.OnChangeType -= ChangeState;
            GameEvent.OnSelectBall -= SelectBallType;
        }

        private void ChangeState(PlayerType playerType)
        {
            this.playerType = playerType;
            currentState.ExitState();

            switch (this.playerType)
            {
                case PlayerType.Stick:
                    currentState = playerStickState;
                    break;
                case PlayerType.Select:
                    currentState = playerSelectState;
                    break;
            }
            
            currentState.EnterState();
        }

        private void SelectBallType(BallType ballType)
        {
            playerSelectState.SetBallType(ballType);
        }
        
        private void Update()
        {
            currentState.UpdateState();
        }
    }
}
