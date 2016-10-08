using UnityEngine;
using System.Collections;

public class AuraCageSpawner : MonoBehaviour {

    public Sprite[] lesAuras;
    public GameObject Aura;

    // Use this for initialization
    void Awake () {
        Aura.GetComponent<SpriteRenderer>().sprite = lesAuras[GetComponent<CageController>().couleurCage];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
