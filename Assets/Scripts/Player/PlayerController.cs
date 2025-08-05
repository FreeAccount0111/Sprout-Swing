using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject handleObj;
        [SerializeField] private LineRenderer line;

        private bool _isDragging;

        public static event Action<Vector2> OnStick;
        
        private void Update()
        {
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
                OnStick?.Invoke(line.GetPosition(1));
                
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
    }
}
