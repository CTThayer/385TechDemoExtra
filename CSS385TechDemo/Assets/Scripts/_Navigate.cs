using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class _Navigate : StateMachineBehaviour {

    Vector3 creaturePos;
    GameObject target = null;
    NavMeshAgent c_Agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("enter nav");
        // Get navmesh agent
        c_Agent = animator.GetComponent<NavMeshAgent>();

        // Get current creature position
        creaturePos = animator.transform.position;

        // Decide what to find and find it
        if (animator.GetInteger("Hunger") < 0 && !animator.GetBool("IsAtFood"))
        {
            target = FindClosestByTag("food");
        }
        else if (animator.GetInteger("Bladder") < -50 && !animator.GetBool("IsASafePlaceToGo"))
        {
            target = FindClosestByTag("relief");
        }
        else if (animator.GetInteger("Energy") < -60 && !animator.GetBool("IsAtBed"))
        {
            target = FindClosestByTag("bed");
        }
        else if (animator.GetBool("PlayerOfferingPetting") && !animator.GetBool("IsAtPlayer"))
        {
            target = FindClosestByTag("Player");
        }

        // give the navmesh agent the target
        if (target != null) {
            Debug.Log(target.transform.position);
			c_Agent.destination = target.transform.position;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("update nav");
        // Check if creature has arrived at target
        // If it has, set IsAtDestination to true, ending _Navigation
        if (!c_Agent.pathPending)
        {
            if (c_Agent.remainingDistance <= c_Agent.stoppingDistance)
            {
                if (!c_Agent.hasPath || c_Agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetBool("IsAtDestination", true);
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Updating other bools elsewhere using triggers. If that doesn't work, revert to here.
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // Borrowed basic function from Unity documentation and modified for my needs
    public GameObject FindClosestByTag(string s)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(s);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = creaturePos;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
