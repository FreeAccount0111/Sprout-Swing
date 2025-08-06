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

        public BaseBall CurrentBall => currentBall;

        public void ChangBall(BallType ballType)
        {
            switch (ballType)
            {
                case BallType.Default:
                    break;
                case BallType.Water:
                    break;
                case BallType.Land:
                    break;
            }
        }
    }
}
