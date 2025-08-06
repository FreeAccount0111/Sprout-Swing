using System;
using System.Collections.Generic;
using Ball;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class FlowerController : MonoBehaviour
    {
        [SerializeField] private int indexWater;

        [SerializeField] private SpriteRenderer flowerRenderer;
        [SerializeField] private Transform trunk;

        [SerializeField] private List<Sprite> flowers = new List<Sprite>();

        private void OnEnable()
        {
            Initialized();
        }

        private void Initialized()
        {
            indexWater = 0;
            trunk.localScale = new Vector3(1, 0, 1);
            
            flowerRenderer.sprite = flowers[indexWater];
            trunk.DOScale(Vector3.one, 0.35f).SetEase(Ease.Linear);
        }

        [ContextMenu("AddWater")]
        public void AddWater()
        {
            indexWater += 1;
            flowerRenderer.sprite = flowers[indexWater];
            
            if (indexWater == 2)
            {
                var newBall = ObjectPool.Instance.Get(ObjectPool.Instance.ball).GetComponent<BallMovement>();
                newBall.transform.position = transform.position;
                newBall.GrowUp();
            }
        }
    }
}
