using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFile : MonoBehaviour {

    int maxAugment = 38;
    int current = 0;

    bool growth = true;

    bool textChanged = false;//si le text a ete changer
    int nbAnimaux = 0;

    [SerializeField] GameObject nbOver;//texte over du nombre d'animaux dans la file
    [SerializeField] GameObject nbUnder;//texte under du nombre d'animaux dans la file


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
        if(textChanged)
        {
            if(growth)
            {
                nbOver.GetComponent<Text>().fontSize += 2;
                nbUnder.GetComponent<Text>().fontSize += 2;
                current += 2;
                if (current == maxAugment)
                {
                    growth = false;
                }
            }
            else
            {
                nbOver.GetComponent<Text>().fontSize -= 2;
                nbUnder.GetComponent<Text>().fontSize -= 2;
                current -= 2;
                if (current == 0)
                {
                    textChanged = false;
                }
            }
        }
	
	}

    public void incrementAnimaux()
    {
        if (nbAnimaux < 20)
        {
            nbAnimaux++;
        }
        changeText();
    }

    public void decrementAnimaux()
    {
        if (nbAnimaux > 0)
        {
            nbAnimaux--;
        }
        changeText();
    }

    void changeText()
    {
        nbOver.GetComponent<Text>().text = nbAnimaux.ToString();
        nbUnder.GetComponent<Text>().text = nbAnimaux.ToString();
        textChanged = true;
        growth = true;
    }
}
