using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerStickState : BaseState
    {
        [SerializeField] private GameObject handleObj;
        [SerializeField] private LineRenderer line;

        private bool _isDragging;
        
        public override void EnterState()
        {
            base.EnterState();
            _isDragging = false;
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
                GameEvent.RaiseStick(line.GetPosition(1));
                
                handleObj.SetActive(false);
                line.SetPosition(1, Vector3.zero);
                _isDragging = false;
            }

            if (_isDragging)
            {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                line.SetPosition(1,
                    new Vector3(point.x - handleObj.transform.position.x, point.y - handleObj.transform.position.y, 0));
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
