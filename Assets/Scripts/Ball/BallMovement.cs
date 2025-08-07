using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using Util;

namespace Ball
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private BallController ballController;

        private const float Gravity = -9.81f;
        
        public bool isThrowBack;
        private bool _isMoving;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            ResetState();
            GameEvent.OnStick += StickBall;
        }

        private void OnDisable()
        {
            GameEvent.OnStick -= StickBall;
        }

        private void ResetState()
        {
            _isMoving = false;
            isThrowBack = false;
        }

        private void StickBall(Vector2 force)
        {
            if (_isMoving)
                return;
            
            var posTarget = MapController.Instance.GetPosCellByPosWorld((Vector2)transform.position - (force.magnitude + 1) * force.normalized);
            if (Vector2.Distance(ballController.transObj.position, posTarget) < 0.5f)
                return;

            _isMoving = true;
            ballController.CurrentBall.StartStick();
            _coroutine = StartCoroutine(Vector2.Distance(ballController.transObj.position, posTarget) / 4f > ballController.ballUtil.heightTrunk ? ThrowNewCoroutine(Vector2.Distance(ballController.transObj.position, posTarget) / 4f, posTarget) : ThrowNewCoroutine(1, posTarget));
        }

        IEnumerator ThrowNewCoroutine(float c, Vector3 posTarget)
        {
            Vector2 posOrigin = transform.position;
            float distance = Vector2.Distance(posOrigin, posTarget);
            float height = ballController.onTrunk ? ballController.ballUtil.heightTrunk : 0;
            
            Vector3 posF = OutMap(posTarget) ? MapController.Instance.GetSymmetryPoint(posOrigin, posTarget).Item2 : posTarget;
            float heightOut = MapController.Instance.GetCellByWorldPosition(posF).Flower == null ? 0 : ballController.ballUtil.heightTrunk;
            
            float k = Mathf.Sqrt((c - height) / c);
            float xa = ballController.onTrunk ? (distance * k) / (1 - k) : distance / 2f;
            float xb = ballController.onTrunk ? distance / (1 - k) : distance / 2f;

            

            if (!Mathf.Approximately(height, heightOut))
            {
                xa = (distance * k) / (1 - k);
                xb = distance / (1 - k);
            }
            else if(ballController.onTrunk)
            {
                c *= 2;
                k = Mathf.Sqrt((c - height) / c);
                xa = (distance * k) / (1 - k);
                xb = (distance * k) / (1 - k);
            }
            else
            {
                xa = distance / 2f;
                xb = distance / 2f;
            }
            
            float x = 0;
            float y = 0;

            float amount = 0;
            while (amount < 1)
            {
                amount += (ballController.ballUtil.speedGround / distance) * Time.deltaTime;
                x = Mathf.Lerp(-xa, xb, amount);
                y = ((height - c) / (xa * xa)) * x * x + c;
                ballController.transBody.localPosition = new Vector3(0, y, 0);
                ballController.transObj.position = Vector3.Lerp(posOrigin, posTarget, amount);

                if (isThrowBack && !OutMap(ballController.transObj.position))
                    isThrowBack = false;
                
                if ( !isThrowBack && OutMap(ballController.transObj.position))
                {
                    var value = MapController.Instance.GetSymmetryPoint(posOrigin, posTarget);
                    posOrigin = value.Item1;
                    posTarget = value.Item2;

                    isThrowBack = true;
                }

                ballController.CurrentBall.OnFlying();
                yield return null;
            }

            x = xb;
            y = ((height - c) / (xa * xa)) * x * x + c;
            ballController.transBody.localPosition = new Vector3(0, y, 0);
            ballController.transObj.position = posTarget;
            
            ballController.CurrentBall.LandOnGrow();
            yield return new WaitForSeconds(0.5f);
            
            isThrowBack = false;
            _isMoving = false;
        }
        private bool OutMap(Vector3 pos)
        {
            if (pos.x > 13 || pos.x < -13)
                return true;
            if (pos.y > 9 || pos.y < -9)
                return true;
            return false;
        }
    }
}
