using UnityEngine;

namespace Player
{
    public class HoleController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void PlayAnimationWin()
        {
            animator.CrossFade("Win", 0);
        }
    }
}
