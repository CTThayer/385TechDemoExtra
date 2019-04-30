using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sleep : StateMachineBehaviour {
    
    // How much the Energy levels recover per interval
    [SerializeField] int EnergyModifier = 10;
    // Amount of time in seconds before re-checking priorities
    [SerializeField] float SleepInterval = 60;

    float checkTime;
    Vector3 pos;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("enter sleep");
        checkTime = Time.time;
        checkTime += SleepInterval;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("update sleep");
        // Breaks FSM logic for creature falling asleep while being pet if energy is too low
        //// Bypass sleep for petting
        //if (animator.GetBool("PlayerOfferingPetting"))
        //{
        //    animator.SetBool("StateComplete", true);
        //}
        //else
        //{
        int energy = animator.GetInteger("Energy");
            int maxEnergy = animator.transform.GetComponent<_Needs>().GetMAX();
            int newEnergy = energy += EnergyModifier;
            if (newEnergy < maxEnergy)
                animator.SetInteger("Energy", newEnergy);
            else
            {
                animator.SetInteger("Energy", maxEnergy);
                animator.SetBool("StateComplete", true);
            }
        //}
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
