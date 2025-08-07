using DG.Tweening;
using Player;
using UnityEngine;
using Util;

namespace Ball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private BallType curType;
        [SerializeField] private BaseBall currentBall;

        [SerializeField] private DefaultBall defaultBall;
        [SerializeField] private WaterBall waterBall;
        [SerializeField] private LandBall landBall;

        [SerializeField] private SpriteRenderer model;
        
        public Transform transObj;
        public Transform transBody;
        
        public bool onTrunk;
        public BallUtil ballUtil;

        public BaseBall CurrentBall => currentBall;

        public void ChangBall(BallType ballType)
        {
            curType = ballType;
            switch (curType)
            {
                case BallType.Default:
                    currentBall = defaultBall;
                    break;
                case BallType.Water:
                    currentBall = waterBall;
                    break;
                case BallType.Land:
                    currentBall = landBall;
                    break;
            }

            model.sprite = currentBall.icon;
        }
        
        public void GrowUp()
        {
            if (CheckOverlapBall())
            {
                ObjectPool.Instance.Return(gameObject,true);
                return;
            }
            
            onTrunk = true;
            transBody.localPosition = new Vector3(0, ballUtil.heightTrunk, 0);
            
            transBody.localScale = Vector3.zero;
            transBody.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
        }

        public bool CheckOverlapBall()
        {
            var cell = MapController.Instance.GetCellByWorldPosition(transObj.position);
            return cell.ball != null;
        }
    }
}
