using UnityEngine;
using System.Collections;

public class PositionFile : MonoBehaviour {

    //[SerializeField]
    //GameObject nextPostion;//prochaine position dans la file

    bool isOccupied = false;// 1 = deja un animal a cette position

    public void setIsOccupied(bool i) { isOccupied = i; }

    public bool getIsOccupied() { return isOccupied; }
    public Vector2 getPosition() { return gameObject.transform.position; }
    //public Vector2 getNextPosition() { return nextPostion.transform.position; }

    // Use this for initialization
    void Start()
    {
        isOccupied = false;
    }

}
