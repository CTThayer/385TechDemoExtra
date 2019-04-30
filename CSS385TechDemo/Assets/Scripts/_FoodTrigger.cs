using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _FoodTrigger : MonoBehaviour {

    // On exit turn on IsAtFood
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Animator>().SetBool("IsAtFood", true);
    }

    // On exit turn off IsAtFood
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Animator>().SetBool("IsAtFood", false);
    }
}
