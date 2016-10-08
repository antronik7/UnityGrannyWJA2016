using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoolDownController : MonoBehaviour {

    public float CoolDownTime;
    public GameObject monText;
    public GameObject monCircle;

    float TimeCooldown;

    void Awake ()
    {
        TimeCooldown = CoolDownTime;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TimeCooldown -= Time.deltaTime;

        if(TimeCooldown <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnGUI()
    {
        monText.GetComponent<Text>().text = TimeCooldown.ToString("0");
        monCircle.GetComponent<Image>().fillAmount = (TimeCooldown / CoolDownTime);
    }
}
