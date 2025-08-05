using System;
using System.Collections;
using DG.Tweening;
using Gameplay;
using UnityEngine;
using Util;

namespace Player
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private BallUtil ballUtil;

        private const float Gravity = -9.81f;
        
        [SerializeField] private Transform transObj;
        [SerializeField] private Transform transBody;
        [SerializeField] private Transform transShadow;

        [SerializeField] private Transform mask;
        
        public float verticalVelocity;
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
            var posTarget =
                MapController.Instance.GetPosCellByPosWorld((Vector2)transform.position -
                                                            (force.magnitude + 1) * force.normalized);
            _coroutine = StartCoroutine(ThrowCoroutine(posTarget));
        }

        IEnumerator ThrowCoroutine(Vector3 posTarget)
        {
            Vector2 posOrigin = transform.position;
            Vector2 direction = ((Vector2)posTarget - posOrigin).normalized;

            float distance = Vector2.Distance(posOrigin, posTarget);
            float timeToReach = distance / ballUtil.speedGround;
            verticalVelocity = distance;
            float speed = -verticalVelocity / (Gravity * (timeToReach / 2));

            while (verticalVelocity > -distance)
            {
                verticalVelocity += Gravity * speed * Time.deltaTime;
                transBody.position += new Vector3(0, verticalVelocity, 0) * speed * Time.deltaTime;
                //transObj.position += (Vector3)direction * ballUtil.speedGround * Time.deltaTime;
                
                transObj.position = Vector3.MoveTowards(
                    transObj.position,
                    transObj.position + (Vector3)direction,
                    ballUtil.speedGround * Time.deltaTime
                );
                
                if (OutMap())
                {
                    posTarget = MapController.Instance.GetPosCellByPosWorld((Vector2)transform.position -
                                                                            (distance - Vector2.Distance(transform.position, posOrigin)) * direction);
                    direction = (posTarget - transObj.position).normalized;
                }
                
                yield return null;
            }

            transBody.transform.position = transform.position;
            transObj.position = posTarget;
            if (MapController.Instance.CheckHole(transform.position))
            {
               Win();
            }
        }

        private void Win()
        {
            var newFlower = ObjectPool.Instance.Get(ObjectPool.Instance.flower);
            newFlower.transform.position = transObj.position;

            transBody.DOLocalMove(new Vector3(0, 0.75f, 0), 0.5f).SetEase(Ease.Linear);
        }

        private bool OutMap()
        {
            if (transObj.position.x > 13 || transObj.position.x < -13)
                return true;
            if (transObj.position.y > 9 || transObj.position.y < -9)
                return true;
            return false;
        }
    }
}
