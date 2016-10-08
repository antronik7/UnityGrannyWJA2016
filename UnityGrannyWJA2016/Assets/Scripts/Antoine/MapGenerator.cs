using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public GameObject[] listTypeCage;
    public List<GameObject> positionsCage;
    public List<int> couleurCageSpawner;

    void Awake()
    {
        while (couleurCageSpawner.Count >= 1)
        {
            int randomIndexCouleur = Random.Range(0, couleurCageSpawner.Count);
            int randomIndexPosition = Random.Range(0, positionsCage.Count);
            int randomIndexTypeCage = Random.Range(0, listTypeCage.Length);

            GameObject myCage = Instantiate(listTypeCage[randomIndexTypeCage], positionsCage[randomIndexPosition].transform.position, Quaternion.identity) as GameObject;

            myCage.GetComponent<CageController>().couleurCage = couleurCageSpawner[randomIndexCouleur];
            couleurCageSpawner.RemoveAt(randomIndexCouleur);
            positionsCage.RemoveAt(randomIndexPosition);
        }
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
