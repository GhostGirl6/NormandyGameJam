using UnityEngine;

namespace MalbersAnimations
{
    public class IsKinematicB : StateMachineBehaviour
    {
        public enum OnEnterOnExit { OnEnter, OnExit}
        public OnEnterOnExit SetKinematic;
        public bool isKinematic;

        Rigidbody rb;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            rb = animator.GetComponent<Rigidbody>();
            if (SetKinematic == OnEnterOnExit.OnEnter)
            {
                rb.isKinematic = isKinematic;
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (SetKinematic == OnEnterOnExit.OnExit)
            {
                rb.isKinematic = isKinematic;
            }
        }
    }
}