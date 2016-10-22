using UnityEngine;
using System.Collections;

public class gameOverChangescene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GameOver ()
    {
        Application.LoadLevel("Fin");
    }
}
