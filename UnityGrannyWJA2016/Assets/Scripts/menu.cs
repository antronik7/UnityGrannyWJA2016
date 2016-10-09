using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tuto");
    }

    public void exit()
    {
        Application.Quit();
    }


}
