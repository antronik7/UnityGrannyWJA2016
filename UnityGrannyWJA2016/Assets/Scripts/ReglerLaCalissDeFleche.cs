using UnityEngine;
using System.Collections;

public class ReglerLaCalissDeFleche : MonoBehaviour {

    public GameObject ArrowControler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Entrepot")
        {
            ArrowControler.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Entrepot")
        {
            ArrowControler.SetActive(true);
        }
    }
}
