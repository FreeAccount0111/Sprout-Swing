using UnityEngine;

namespace Ball
{
    public abstract class BaseBall : MonoBehaviour
    {
        public Sprite icon;
        public abstract void LandOnGrow();
    }
}
