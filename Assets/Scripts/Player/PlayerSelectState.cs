using Ball;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

namespace Player
{
    public class PlayerSelectState : BaseState
    {
        [SerializeField] private GameObject handleObj;
        [SerializeField] private BallType ballType;
        [SerializeField] private LayerMask ballMask;
        
        private bool _isDragging;
        
        public override void EnterState()
        {
            base.EnterState();
        }

        public void SetBallType(BallType ballType)
        {
            this.ballType = ballType;
            switch (ballType)
            {
                case BallType.Default:
                    handleObj.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                    break;
                case BallType.Water:
                    handleObj.GetComponent<SpriteRenderer>().color = new Color32(66, 175, 255, 255);
                    break;
                case BallType.Land:
                    handleObj.GetComponent<SpriteRenderer>().color = new Color32(213, 99, 71, 255);
                    break;
            }
        }

        public override void UpdateState()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var newPos = new Vector3(point.x, point.y, 0);

                handleObj.SetActive(true);
                handleObj.transform.position = newPos;

                _isDragging = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                handleObj.SetActive(false);
                _isDragging = false;
            }

            if (_isDragging)
            {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var newPos = new Vector3(point.x, point.y, 0);

                handleObj.transform.position = newPos;
                Collider2D col = Physics2D.OverlapCircle(newPos,0.4f, ballMask);
                if (col != null)
                {
                    col.GetComponent<BallController>().ChangBall(ballType);
                }
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
