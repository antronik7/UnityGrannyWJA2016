using UnityEngine;
using System.Collections;

public class FbScore : MonoBehaviour {

    bool textSet = false;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (textSet)
        {
            transform.position += new Vector3(0, 0.03f, 0);
            if (GetComponent<TextMesh>().color.a > 0)
                GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, GetComponent<TextMesh>().color.a - Time.deltaTime);
            else
                Destroy(gameObject);
        }

    }

    public void setText(float s)
    {
        GetComponent<TextMesh>().text = "+" + s.ToString();
        textSet = true;
    }
}
