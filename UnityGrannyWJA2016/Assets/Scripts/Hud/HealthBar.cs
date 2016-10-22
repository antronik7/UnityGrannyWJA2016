using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

    float waitTime = 0.3f;
    float difference = 0;
    float current;
    float pv = 1;
    float toChange;

    bool asChanged = false;

	// Use this for initialization
	void Awake () {

        current = pv;

    }
	
	// Update is called once per frame
	void OnGUI () {

        if (asChanged)
        {

            if (current < pv)
            {
                toChange = difference / waitTime * Time.deltaTime;
                pv -= toChange;
                gameObject.GetComponent<Image>().fillAmount -= toChange;
                if(pv < current)
                {
                    pv = current;
                    gameObject.GetComponent<Image>().fillAmount = current;
                    asChanged = false;
                }
            }
            else if (current > pv)
            {
                toChange = difference / waitTime * Time.deltaTime;
                pv += toChange;
                gameObject.GetComponent<Image>().fillAmount += toChange;
                if (pv > current)
                {
                    pv = current;
                    gameObject.GetComponent<Image>().fillAmount = current;
                    asChanged = false;
                }
                if(pv > 1)
                {
                    pv = 1;
                    asChanged = false;
                }
            }
            else
            {
                asChanged = false;
            }
        }

    }

    public void loseAmourDeDieu(float l)
    {
        current -= l/100;
        difference = pv - current;
        asChanged = true;
    }

    public void gainAmourDeDieu(float l)
    {
        current += l/100;
        if(current > 1)
        {
            current = 1;
        }
        difference = (pv - current) * -1;
        asChanged = true;
    }
}
