using UnityEngine;
using System.Collections;

public class Nouriture : MonoBehaviour {


    [SerializeField] Sprite[] listeSprite; 
    [SerializeField] int pvMax = 10;
    int pointDeVie;

	// Use this for initialization
	void Start () {
        pointDeVie = pvMax;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void prendreUneBouchee()
    {
        pointDeVie--;
        if (pointDeVie <= pvMax * 0.80)
        {
            if (pointDeVie <= pvMax * 0.60) 
            {
                if (pointDeVie <= pvMax * 0.40)
                {
                    if (pointDeVie <= pvMax * 0.20)
                    {
                        if (pointDeVie == 0)
                        {
                            Destroy(gameObject);//Mort pu de point de vie
                        }
                        else
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = listeSprite[3];//qcinquieme palier
                        }
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = listeSprite[2];//quatrieme palier
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = listeSprite[1];//troisieme palier
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = listeSprite[0];//deuxieme palier
            }
        }
    }


}
