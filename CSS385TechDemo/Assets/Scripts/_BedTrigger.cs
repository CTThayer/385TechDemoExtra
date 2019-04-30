using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BedTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        // On exit turn on IsAtBed
        other.GetComponent<Animator>().SetBool("IsAtBed", true);
    }

    private void OnTriggerExit(Collider other)
    {
        // On exit turn off IsAtBed
        other.GetComponent<Animator>().SetBool("IsAtBed", false);
    }
}
