using UnityEngine;
using System.Collections;

public class A_Herbivore : Animal{
    public void intialiseEntry(string n)
    {
        name = n;
        goingOutChance = 1; //herbivore
        isEating = false;
        couple = false;
        type = false;
        grabed = false;
        zone = 0;
    }
}
