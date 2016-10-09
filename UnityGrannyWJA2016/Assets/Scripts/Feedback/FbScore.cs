using UnityEngine;
using System.Collections;

public class FbScore : MonoBehaviour {

    bool textSet = false;
    [SerializeField]
    TextMesh textOver;
    [SerializeField]
    TextMesh textUnder;
    [SerializeField]
    SpriteRenderer symbol;

    // Use this for initialization
    void Start () {

        textOver.GetComponent<MeshRenderer>().sortingLayerName = "UI";
        textOver.GetComponent<MeshRenderer>().sortingOrder = 10;
        textUnder.GetComponent<MeshRenderer>().sortingLayerName = "UI";
        textUnder.GetComponent<MeshRenderer>().sortingOrder = 9;


    }
	
	// Update is called once per frame
	void Update () {

        if (textSet)
        {
            transform.position += new Vector3(0, 0.005f, 0);
            if (textUnder.color.a > 0)
            {
                textUnder.color = new Color(0, 0, 0, textUnder.color.a - (Time.deltaTime / 2));
                textOver.color = new Color(textOver.color.r, textOver.color.g, textOver.color.b, textOver.color.a - (Time.deltaTime / 2));
                symbol.color = new Color(255, 255, 255, symbol.color.a - (Time.deltaTime / 2));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void setTextScore(float s)
    {
        textUnder.text = "+" + s.ToString();
        textOver.text = "+" + s.ToString();
        textSet = true;
    }

    public void setTextAmour(float s)
    {
        textUnder.text = "-" + s.ToString();
        textOver.text = "-" + s.ToString();
        textSet = true;
    }

}
