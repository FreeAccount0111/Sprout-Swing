using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Ball
{
    public class WaterBall : BaseBall
    {
        [SerializeField] private float speedFall;
        [SerializeField] private float timeSpawn;

        private float _currentTime;
        public override void OnFlying()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0)
            {
                var newWater = ObjectPool.Instance.Get(ObjectPool.Instance.water).GetComponent<WaterFallController>();
                newWater.SetupFall(ballController.transObj.position,ballController.transBody.localPosition.y);
                
                _currentTime = timeSpawn;
            }
        }

        public override void LandOnGrow()
        {
            if (ballController.CheckOverlapBall())
            {
                ObjectPool.Instance.Return(ballController.gameObject,true);
                return;
            }
            else
            {
                MapController.Instance.GetCellByWorldPosition(ballController.transObj.position).ball = ballController;
            }
        }
    }
}
