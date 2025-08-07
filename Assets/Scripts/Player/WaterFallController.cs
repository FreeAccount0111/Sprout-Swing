using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class WaterFallController : MonoBehaviour
    {
        [SerializeField] private Transform transObj;
        [SerializeField] private Transform transBody;

        public void SetupFall(Vector3 posObj,float heightBody)
        {
            transObj.position = posObj;
            transBody.localPosition = new Vector3(0, heightBody, 0);
           transBody.DOLocalMove(Vector3.zero,5).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(WaterGround);
        }

        private void WaterGround()
        {
            var cell = MapController.Instance.GetCellByWorldPosition(transObj.position);
            if (cell != null)
                cell.AddWater();
            ObjectPool.Instance.Return(gameObject, true);
        }
    }
}
