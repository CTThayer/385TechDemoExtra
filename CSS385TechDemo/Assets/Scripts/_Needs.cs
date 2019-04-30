using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Needs : MonoBehaviour {
    // Serial max values for needs
    [SerializeField] int MAX = 100;
    [SerializeField] int MIN = -100;

    // Booleans tracking what activity the creature is currently doing
    bool isRelievingSelf = false;
    bool isEating = false;
    bool isSleeping = false;
    bool isBeingPet = false;
    bool isWalking = false;

    // Reference to the Animator object which is this creature's FSM
    Animator CreatureFSM;
    
    // temp for storing values from the FSM Animator parameters
    int temp = 0;


    // Initializes Needs System
    // Gets a reference to this creature's FSM
    // Starts/uses InvokeReapeating for a fixed interval of calls to the
    // updateNeeds() function. (Set to occur every 30 seconds.)

    void Start () {
        CreatureFSM = GetComponent<Animator>();
        InvokeRepeating("updateNeeds", 0.0f, 5f);
	}
	
	// Updates the needs counters for the creature
    void updateNeeds()
    {
        if (!isEating)
        {
            temp = CreatureFSM.GetInteger("Hunger");
            if (temp > (MIN - 1))
                temp-= 5;
            CreatureFSM.SetInteger("Hunger", temp);

            // Set slider value too
            if (GameObject.FindGameObjectWithTag ("hungerSlider")) {
            	Slider hungerSlider = GameObject.FindGameObjectWithTag("hungerSlider").GetComponent<Slider>();
            	hungerSlider.value = (float) temp;
        	}
        }
        if (!isRelievingSelf)
        {
            temp = CreatureFSM.GetInteger("Bladder");
            if (temp > (MIN - 1))
                temp-= 3;
            CreatureFSM.SetInteger("Bladder", temp);

            // Set slider value too
            if (GameObject.FindGameObjectWithTag ("bladderSlider")) {
            	Slider bladderSlider = GameObject.FindGameObjectWithTag("bladderSlider").GetComponent<Slider>();
            	bladderSlider.value = (float) temp;
        	}
        }
        if (!isBeingPet)
        {
            temp = CreatureFSM.GetInteger("Happiness");
            if (temp > (MIN - 1))
                temp-= 2;
            CreatureFSM.SetInteger("Happiness", temp);

            // Set slider value too
            if (GameObject.FindGameObjectWithTag ("happinessSlider")) {
            	Slider happinessSlider = GameObject.FindGameObjectWithTag("happinessSlider").GetComponent<Slider>();
            	happinessSlider.value = (float) temp;
        	}
        }
        if (!isSleeping)
        {
            temp = CreatureFSM.GetInteger("Energy");
            if (temp > (MIN - 1))
                temp-= 2;
            CreatureFSM.SetInteger("Energy", temp);

            // Set slider value too
            if (GameObject.FindGameObjectWithTag ("energySlider")) {
            	Slider energySlider = GameObject.FindGameObjectWithTag("energySlider").GetComponent<Slider>();
            	energySlider.value = (float) temp;
        	}
        }
        // Note: Makes additional energy deduction if creature is moving
        if (isWalking)
        {
            temp = CreatureFSM.GetInteger("Energy");
            if (temp > (MIN - 1))
                temp--;
            CreatureFSM.SetInteger("Energy", temp);
        }
    }

    public int GetMAX()
    {
        return MAX;
    }

    public int GetMIN()
    {
        return MIN;
    }

    public void petCreature() {
		CreatureFSM.SetBool("PlayerOfferingPetting", true);
	}
}
