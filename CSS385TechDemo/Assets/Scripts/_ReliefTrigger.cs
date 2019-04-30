using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ReliefTrigger : MonoBehaviour {

    // On exit turn on IsAtReliefArea
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Animator>().SetBool("IsAtReliefArea", true);
    }

    // On exit turn off IsAtReliefArea
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Animator>().SetBool("IsAtReliefArea", false);
    }
}
