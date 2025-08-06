using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using Util;

namespace Ball
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private BallUtil ballUtil;
        [SerializeField] private BallController ballController;

        private const float Gravity = -9.81f;
        
        [SerializeField] private Transform transObj;
        [SerializeField] private Transform transBody;
        [SerializeField] private Transform transShadow;

        [SerializeField] private Transform mask;
        
        public float verticalVelocity;
        public bool onTrunk;
        public bool isThrowBack;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            PlayerController.OnStick += StickBall;
        }

        private void OnDisable()
        {
            PlayerController.OnStick -= StickBall;
        }

        private void StickBall(Vector2 force)
        {
            var posTarget = MapController.Instance.GetPosCellByPosWorld((Vector2)transform.position - (force.magnitude + 1) * force.normalized);
            if (Vector2.Distance(transObj.position, posTarget) < 0.5f)
                return;

            _coroutine = StartCoroutine(Vector2.Distance(transObj.position, posTarget) / 4f > ballUtil.heightTrunk ? ThrowNewCoroutine(Vector2.Distance(transObj.position, posTarget) / 4f, posTarget) : ThrowNewCoroutine(1, posTarget));
        }

        IEnumerator ThrowNewCoroutine(float c, Vector3 posTarget)
        {
            Vector2 posOrigin = transform.position;
            float distance = Vector2.Distance(posOrigin, posTarget);
            float height = onTrunk ? ballUtil.heightTrunk : 0;
            
            float k = Mathf.Sqrt((c - height) / c);
            float xa = onTrunk ? (distance * k) / (1 - k) : distance / 2f;
            float xb = onTrunk ? distance / (1 - k) : distance / 2f;
            
            float x = 0;
            float y = 0;

            float amount = 0;
            while (amount < 1)
            {
                amount += (ballUtil.speedGround / distance) * Time.deltaTime;
                x = Mathf.Lerp(-xa, xb, amount);
                y = ((height - c) / (xa * xa)) * x * x + c;
                transBody.localPosition = new Vector3(0, y, 0);
                transObj.position = Vector3.Lerp(posOrigin, posTarget, amount);

                if (isThrowBack && !OutMap())
                    isThrowBack = false;
                if ( !isThrowBack && OutMap())
                {
                    var value = MapController.Instance.GetSymmetryPoint(posOrigin, posTarget);
                    posOrigin = value.Item1;
                    posTarget = value.Item2;

                    isThrowBack = true;
                }
                yield return null;
            }

            x = xb;
            y = ((height - c) / (xa * xa)) * x * x + c;
            transBody.localPosition = new Vector3(0, y, 0);
            transObj.position = posTarget;
            
            if (MapController.Instance.CheckHole(transform.position) || onTrunk)
            {
                onTrunk = true;
                yield return Win();
            }
            
            isThrowBack = false;
        }
        private bool OutMap()
        {
            if (transObj.position.x > 13 || transObj.position.x < -13)
                return true;
            if (transObj.position.y > 9 || transObj.position.y < -9)
                return true;
            return false;
        }
        IEnumerator Win()
        {
            var newFlower = ObjectPool.Instance.Get(ObjectPool.Instance.flower);
            newFlower.transform.position = transObj.position;

            transBody.DOLocalMove(new Vector3(0, ballUtil.heightTrunk, 0), 0.5f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.5f);
        }

        public void GrowUp()
        {
            onTrunk = true;
            transBody.localPosition = new Vector3(0, ballUtil.heightTrunk, 0);
            
            transBody.localScale = Vector3.zero;
            transBody.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
        }
    }
}
