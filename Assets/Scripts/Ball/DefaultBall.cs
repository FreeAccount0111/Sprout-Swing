using DG.Tweening;
using Player;
using UnityEngine;

namespace Ball
{
    public class DefaultBall : BaseBall
    {
        public override void OnFlying()
        {
            
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
            
            var cell = MapController.Instance.GetCellByWorldPosition(ballController.transObj.position);
            if (cell.Flower == null)
            {
                var newFlower = ObjectPool.Instance.Get(ObjectPool.Instance.flower);
                newFlower.transform.position = ballController.transObj.position;
                cell.Flower = newFlower.GetComponent<FlowerController>();

                ballController.transBody.DOLocalMove(new Vector3(0, ballController.ballUtil.heightTrunk, 0), 0.5f).SetEase(Ease.Linear);
                ballController.onTrunk = true;
            }
        }
    }
}
