using UnityEngine;
using System.Collections;

public class AuraSpawner : MonoBehaviour {

    public GameObject[] lesAuras;

    void Awake ()
    {
        
    }

	// Use this for initialization
	void Start () {

        string parentName = "";

        GameObject monAura = Instantiate(lesAuras[transform.parent.gameObject.GetComponent<Animal>().getColor()], transform.parent.transform.position, Quaternion.identity) as GameObject;
        monAura.transform.parent = transform.parent;

        parentName = monAura.transform.parent.GetComponent<Animal>().getName();

        if (parentName == "Crocodile")
        {
            monAura.transform.localPosition = new Vector2(0, -0.12f);
        }
        else if (parentName == "Elephant")
        {
            monAura.transform.localPosition = new Vector2(0, -0.125f);
        }
        else if (parentName == "Lion")
        {
            monAura.transform.localPosition = new Vector2(-0.02f, -0.13f);
        }
        else if (parentName == "Pig")
        {
            monAura.transform.localPosition = new Vector2(0, -0.12f);
        }
        else if (parentName == "Shark")
        {
            monAura.transform.localPosition = new Vector2(0, -0.13f);
        }
        else if (parentName == "Sheep")
        {
            monAura.transform.localPosition = new Vector2(0, -0.125f);
        }
        else if (parentName == "Turtle")
        {
            monAura.transform.localPosition = new Vector2(-0.02f, -0.125f);
        }
        else if (parentName == "Wolf")
        {
            monAura.transform.localPosition = new Vector2(-0.02f, -0.13f);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
