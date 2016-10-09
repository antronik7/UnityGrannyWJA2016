using UnityEngine;
using System.Collections;

public class AnimationMenu : MonoBehaviour {

    [SerializeField]
    GameObject noe;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setPlay()
    {
        noe.GetComponent<Animator>().SetBool("replay", false);
        noe.GetComponent<Animator>().SetBool("play", true);    
    }

    public void rePlay()
    {
        noe.GetComponent<Animator>().SetBool("replay", true);
    }
}
