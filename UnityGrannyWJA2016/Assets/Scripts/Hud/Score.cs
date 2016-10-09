using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    bool scoreAdded = false;

    int current;
    int objectif;
    int jump = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
	
        if(scoreAdded)
        {
            current += jump;
            gameObject.GetComponent<Text>().text = current.ToString();
            if(current == objectif)
            {
                scoreAdded = false;
            }
        }
	}

    public void addScore(int i)
    {
        current = int.Parse(gameObject.GetComponent<Text>().text);
        objectif = current + i;
        scoreAdded = true;
    }
}
