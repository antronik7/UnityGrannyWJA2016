using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScoreTextController : MonoBehaviour {


	// Use this for initialization
	void Start () {
        int scoreFinal = 0;
        GetComponent<Text>().text = scoreFinal.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
