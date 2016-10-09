using UnityEngine;
using System.Collections;

public class FinController : MonoBehaviour {

    public AudioClip sonBobo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void chargerMaScene ()
    {
        Application.LoadLevel("Menu");
    }

    public void playBoboSound()
    {
        AudioSource.PlayClipAtPoint(sonBobo, Vector3.zero, 1f);
    }
}
