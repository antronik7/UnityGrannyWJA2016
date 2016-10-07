using UnityEngine;
using System.Collections;

public class A_Carnivore : Animal {
    public void intialiseEntry(string n)
    {
        name = n;
        goingOutChance = 0; //carnivore
        isEating = false;
        couple = false;
        type = false;
        grabed = false;
        zone = 0;
    }
	
}
