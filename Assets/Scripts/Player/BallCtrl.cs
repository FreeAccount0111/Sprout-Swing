using System;
using UnityEngine;

namespace Player
{
    public class BallCtrl : MonoBehaviour
    {
        public static BallCtrl Instance;
        public Vector2 groundDispenseVelocity;
        public Vector2 verticalDispenseVelocity;
        public bool isStart;

        [SerializeField] private FakeHeightObject fakeHeightObject;
        public static Action attackBall;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SpawnBall();
        }

        public void SpawnBall()
        {
            fakeHeightObject.SetBounceIndex(0);
            isStart = true;
            fakeHeightObject.Initialized(Vector2.zero, 4);
        }

        public void AddForceBall(GameObject obj)
        {
            if (isStart)
            {
                fakeHeightObject.SetBounceIndex(0);
                Vector3 ballPos = transform.position;
                Vector3 racketPos = Vector3.zero;
                
                racketPos = new Vector3(-3f, 4.03999996f, -0.5f);

                float racketWitdh = obj.GetComponent<Collider2D>().bounds.size.x;

                float positionY;
                if (obj.gameObject.name == "Player")
                {
                    positionY = 1;
                }
                else
                    positionY = -1;
                float positionX = (ballPos.x - racketPos.x) / racketWitdh;

                Vector2 dir = new Vector2(positionX, positionY);
                fakeHeightObject.Initialized(dir.normalized * 4.75f, 5.75f);

                isStart = false;
                attackBall?.Invoke();
            }
            else if (fakeHeightObject.GetHeight() < 0.75f)
            {
                fakeHeightObject.SetBounceIndex(0);
                Vector3 ballPos = transform.position;
                Vector3 racketPos =  obj.transform.position;
                float racketWidh = obj.GetComponent<Collider2D>().bounds.size.x;

                float positionY;
                if (obj.gameObject.name == "Player")
                {
                    positionY = 1;
                }
                else
                    positionY = -1;
                float positionX = (ballPos.x - racketPos.x) / racketWidh;
                
                Vector2 dir = new Vector2(positionX, positionY);
                fakeHeightObject.Initialized(dir.normalized * UnityEngine.Random.Range(groundDispenseVelocity.x, groundDispenseVelocity.y), UnityEngine.Random.Range(verticalDispenseVelocity.x, verticalDispenseVelocity.y));

                isStart = false;
                attackBall?.Invoke();
            }
        }
    }
}
