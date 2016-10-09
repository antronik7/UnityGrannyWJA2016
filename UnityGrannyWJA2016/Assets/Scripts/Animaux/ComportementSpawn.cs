using UnityEngine;
using System.Collections;

public class ComportementSpawn : MonoBehaviour {

    bool isMoving = false;
    Vector2 seekPosition;

    public void setIsMoving(bool i, Vector2 p)
    {
        seekPosition = p;
        isMoving = i;
        GetComponent<Animal>().toggleAnimationWalkOn();
    }

    public bool getIsMoving(){ return isMoving; }
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, seekPosition, 5f * Time.deltaTime);
        }
        if(transform.position.x == seekPosition.x && transform.position.y == seekPosition.y)
        {
            isMoving = false;
            GetComponent<Animal>().toggleAnimationWalkOff();
        }
    }
}
