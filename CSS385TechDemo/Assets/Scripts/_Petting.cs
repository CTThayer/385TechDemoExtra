using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Petting : StateMachineBehaviour {
    // How much the Happiness need improves by per EatingInterval
    [SerializeField] int HappinessModifier = 5;
    // Amount of time in seconds before re-checking priorities
    [SerializeField] float PettingInterval = 6;

    float checkTime;
    Vector3 pos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("enter pet");
        checkTime = Time.time;
        checkTime += PettingInterval;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("update pet");
        int happiness = animator.GetInteger("Happiness");
        int maxHappiness = animator.transform.GetComponent<_Needs>().GetMAX();
        int newHappiness = happiness += HappinessModifier;
        if (newHappiness < maxHappiness)
            animator.SetInteger("Happiness", newHappiness);
        else
        {
            animator.SetInteger("Happines", maxHappiness);
            animator.SetBool("StateComplete", true);
            animator.SetBool("PlayerOfferingPetting", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
