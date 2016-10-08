using UnityEngine;
using System.Collections;

public class AuraSpawner : MonoBehaviour {

    public GameObject[] lesAuras;

    void Awake ()
    {
        
    }

	// Use this for initialization
	void Start () {
        Debug.Log(transform.parent.gameObject.GetComponent<Animal>().getColor());
        GameObject monAura = Instantiate(lesAuras[transform.parent.gameObject.GetComponent<Animal>().getColor()], transform.parent.transform.position, Quaternion.identity) as GameObject;
        monAura.transform.parent = transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
