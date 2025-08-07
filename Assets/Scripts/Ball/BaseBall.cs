using Player;
using UnityEngine;

namespace Ball
{
    public abstract class BaseBall : MonoBehaviour
    {
        [SerializeField] protected BallController ballController;
        public Sprite icon;
        public virtual void StartStick()
        {
            var cell = MapController.Instance.GetCellByWorldPosition(ballController.transObj.position);
            if (cell.Flower != null && cell.Flower.IndexWater == 2)
            {
                ObjectPool.Instance.Return(cell.Flower.gameObject, true);
                cell.Flower = null;
            }

            cell.ball = null;
        }
        public abstract void OnFlying();

        public virtual void LandOnGrow()
        {
   
        }
    }
}
