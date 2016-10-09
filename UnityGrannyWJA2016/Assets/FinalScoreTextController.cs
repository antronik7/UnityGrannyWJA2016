using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScoreTextController : MonoBehaviour {

    void Awake()
    {
        int scoreFinal = GameManager.instance.playerScore;
        GetComponent<Text>().text = scoreFinal.ToString();
        GameManager.instance.DestroyGameManager();

    }


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
