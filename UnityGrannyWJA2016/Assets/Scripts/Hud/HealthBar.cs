using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

    bool amourGain = false;
    bool amourLoss = false;

    float result = 0;
    float toChange;
    float waitTime = 0.3f;
    float current;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {

        if(amourGain)
        {
            current -= toChange / waitTime * Time.deltaTime;
            gameObject.GetComponent<Image>().fillAmount += toChange / waitTime * Time.deltaTime;
            if(current <= 0)
            {
                amourGain = false;
                gameObject.GetComponent<Image>().fillAmount = result;
            }
        }
        if (amourLoss)
        {
            current -= toChange / waitTime * Time.deltaTime;
            gameObject.GetComponent<Image>().fillAmount -= toChange / waitTime * Time.deltaTime;
            if (current <= 0)
            {
                amourLoss = false;
                gameObject.GetComponent<Image>().fillAmount = result;
            }
        }

    }

    public void loseAmourDeDieu(float l)
    {
        toChange = l/100;
        current = toChange;
        amourLoss = true;
        result = gameObject.GetComponent<Image>().fillAmount - toChange;
    }

    public void gainAmourDeDieu(float l)
    {
        
        toChange = l / 100;
        current = toChange;
        amourGain = true;
        result = gameObject.GetComponent<Image>().fillAmount + toChange;
    }
}
