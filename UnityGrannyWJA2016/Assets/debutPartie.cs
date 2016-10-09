using UnityEngine;
using System.Collections;

public class debutPartie : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startGame()
    {
        GameManager.instance.CanPLay = true;
    }
}
