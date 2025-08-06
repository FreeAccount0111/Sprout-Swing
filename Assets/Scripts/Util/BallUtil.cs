using UnityEngine;
using UnityEngine.Serialization;

namespace Util
{
    public enum BallType
    {
        Default = 0,
        Water = 1,
        Land = 2,
    }
    
    [CreateAssetMenu(menuName = "DataSo/Utils/BallUtil")]
    public class BallUtil : ScriptableObject
    {
        public float speedGround;
        public float heightTrunk;
    }
}
