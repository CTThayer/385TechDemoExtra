using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Eat : StateMachineBehaviour {
    // How much the Hunger need improves by per EatingInterval
    [SerializeField] int HungerModifier = 5;
    // Amount of time in seconds before re-checking needs
    [SerializeField] float EatingInterval = 6;

    float checkTime;
    Vector3 pos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        Debug.Log("enter eat");
        checkTime = Time.time;
        checkTime += EatingInterval;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        Debug.Log("update eat");
        // Check if eating interval has elapsed
        if (Time.time >= checkTime)
        {
            int hunger = animator.GetInteger("Hunger");
            int maxHunger = animator.transform.GetComponent<_Needs>().GetMAX();
            int newHunger = hunger += HungerModifier;
            if (newHunger < maxHunger)
                animator.SetInteger("Hunger", newHunger);
            else
                animator.SetInteger("Hunger", maxHunger);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If food objects are destroyed after consumption add here or in OnStateUpdate depending on how they are destroyed
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
