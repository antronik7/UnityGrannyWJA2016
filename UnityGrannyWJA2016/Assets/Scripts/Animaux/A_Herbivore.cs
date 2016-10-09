using UnityEngine;
using System.Collections;

public class A_Herbivore : Animal{
    public void intialiseEntry(string n)
    {
        name = n;
        goingOutChance = 1; //herbivore
        isEating = false;
        couple = false;
        type = true;
        grabed = false;
        zone = 0;
    }

    public void toggleAnimatorDead()
    {
        gameObject.GetComponent<Animator>().SetBool("dead", true);
    }
}
