using System;
using UnityEngine;

namespace Ball
{
    public class FakeHeightObject : MonoBehaviour
    {
        public static Action<Vector2,bool> OnGroundHitEvent;

        public Transform transObj;
        public Transform transBody;
        public Transform transShadow;
        public BallCtrl ballCtrl;

        public float gravity = -9.81f;
        public Vector2 groundVelocity;
        public float verticalVelocity;
        private float _lastVerticalVelocity;
        private int _bounceIndex;

        [SerializeField] private float divisionFactorVertical;
        [SerializeField] private float divisionFactorGround;
    
        [SerializeField] private bool enBall;

        public static float speed;

        public bool isGrounded;

        public FakeHeightObject(float lastVerticalVelocity)
        {
            _lastVerticalVelocity = lastVerticalVelocity;
        }

        private void Update()
        {
            UpdatePosition();
            CheckGroundHit();
        }

        public void SetBounceIndex(int amount)
        {
            _bounceIndex = amount;
            enBall = false;
        }

        public void Initialized(Vector2 groundVelocity,float verticalVelocity)
        {
            isGrounded = false;
            this.groundVelocity = groundVelocity;
            this.verticalVelocity = verticalVelocity;
            _lastVerticalVelocity = verticalVelocity;
        }

        private void UpdatePosition()
        {
            if (!isGrounded)
            {
                verticalVelocity += gravity * speed * Time.deltaTime;
                transBody.position += new Vector3(0, verticalVelocity, 0) * speed * Time.deltaTime;
            }

            transObj.position += (Vector3)groundVelocity * speed * Time.deltaTime;
        }

        private void CheckGroundHit()
        {
            if (transBody.position.y < transObj.position.y && !isGrounded)
            {
                transBody.position = transObj.position;
                isGrounded = true;
                if (!enBall)
                    switch (_bounceIndex)
                    {
                        case 0:
                            if (transform.position.x < -4 || transform.position.x > 4 || transform.position.y < -4 ||
                                transform.position.y > 4)
                            {
                                enBall = true;
                                OnGroundHitEvent?.Invoke(transform.position, false);
                            }

                            break;
                        case 1:
                            enBall = true;
                            OnGroundHitEvent?.Invoke(transform.position, true);
                            break;
                    }

                if (_bounceIndex == 2)
                    Stick();

                Bounce();
                SlowDownGroundVelocity();
            }
        }

        public void Stick()
        {
            groundVelocity = Vector2.zero;
        }

        private void Bounce()
        {
            if (_bounceIndex >= 2)
                return;

            if (!BallCtrl.Instance.isStart)
            {
                Initialized(groundVelocity, _lastVerticalVelocity / divisionFactorVertical);
                _bounceIndex += 1;
            }
            else
                Initialized(groundVelocity, _lastVerticalVelocity);

        }

        public void SlowDownGroundVelocity()
        {
            groundVelocity = groundVelocity / divisionFactorGround;
        }

        public float GetHeight()
        {
            return Vector2.Distance(transBody.position, transShadow.position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Net"))
            {
                if (Vector2.Distance(transBody.position, transShadow.position) < 0.75f)
                {
                    isGrounded = false;
                    Initialized(-groundVelocity, verticalVelocity / 7.5f);
                    enBall = true;
                    OnGroundHitEvent?.Invoke(transform.position, false);
                }
            }
        }
    }
}
