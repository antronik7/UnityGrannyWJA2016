using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class calculatrice : MonoBehaviour {

	public InputField Nb1;
	public InputField Nb2;
	public InputField NbResultat;

    float resultat;

	/**********************************************
	Pour convertir un string en float (ou int) on utilise float.Parse("100") ou int.Parse("100"): "float = float.Parse(monString);"
	Pour convertir un float en string, on utilise .ToString(): "string = monFloat.ToString();"
	 **********************************************/

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void btn_Plus() {

        if(verification())
        {
            resultat = float.Parse(Nb1.text) + float.Parse(Nb2.text);
            NbResultat.text = resultat.ToString();
        }
		
	}
	public void btn_Minus() {
		
	}
	public void btn_Multiply() {
		
	}
	public void btn_Division() {
		
	}

    bool verification()
    {
        if (Nb1.text == "")
        {
            return false;
            Debug.Log("Il manque un nombre dans un des choix");
        }
        else if (Nb2.text == "")
        {
            return false;
            Debug.Log("Il manque un nombre dans un des choix");
        }
        return true;
    }

}
