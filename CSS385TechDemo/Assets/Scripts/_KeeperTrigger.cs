using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _KeeperTrigger : MonoBehaviour {

    // On exit turn on IsAtPlayer
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Animator>().SetBool("IsAtPlayer", true);
    }

    // On exit turn off IsAtPlayer
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Animator>().SetBool("IsAtPlayer", false);
    }
}
