using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _RelieveSelf : StateMachineBehaviour {

    public GameObject droppings;

    [SerializeField] int BladderModifier = 100;

    float endTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        Debug.Log("enter relief");
        endTime = Time.time + 15f;
        animator.SetBool("StateComplete", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("update relief");
        if (Time.time > endTime)
        {
            Instantiate(droppings, animator.transform.parent.transform.position, Quaternion.identity);
            animator.SetBool("StateComplete", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int temp = animator.GetInteger("Bladder") + BladderModifier;
        if (temp < BladderModifier)
            animator.SetInteger("Bladder", temp);
        else
            animator.SetInteger("Bladder", BladderModifier);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
