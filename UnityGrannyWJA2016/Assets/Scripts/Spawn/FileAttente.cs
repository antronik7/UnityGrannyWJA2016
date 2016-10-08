using UnityEngine;
using System.Collections;

public class FileAttente : MonoBehaviour {

    const int maxNbrAnimals = 9;

    [SerializeField]
    GameObject lion;

    [SerializeField] GameObject[] arrPosition;//array de positions 0 = debut et lengt = fin
    GameObject[] arrAnimals;//array d'animaux 0 = debut et lengt = fin

	// Use this for initialization
	void Start () {

        arrAnimals = new GameObject[maxNbrAnimals]; //initialisation array animals
        spawnAnimal();
	}
	
	// Update is called once per frame
	void Update () {
        moveAnimals();
	}

    void moveAnimals()
    {
        for(int i = arrPosition.Length - 2; i >= 0; i--)//pour toute les position sauf la fin
        {
            if (arrPosition[i].GetComponent<PositionFile>().getIsOccupied())//si la case est occuper par un animal
            {
                
                if (!arrPosition[i + 1].GetComponent<PositionFile>().getIsOccupied())//si sa porchaine case n'est pas occuper
                {
                    if (!arrAnimals[i].GetComponent<ComportementSpawn>().getIsMoving())
                    {
                        arrPosition[i].GetComponent<PositionFile>().setIsOccupied(false);//ma position actuel n'est plus occuper
                        arrPosition[i + 1].GetComponent<PositionFile>().setIsOccupied(true);//la prochaine position est occuper
                        arrAnimals[i].GetComponent<ComportementSpawn>().setIsMoving(true, arrPosition[i + 1].transform.position);//faire bouger l'animal a la prochaine position
                        arrAnimals[i + 1] = arrAnimals[i];//bouger l'animal dans l'array
                        arrAnimals[i] = null;//enlever l'animal puisqu'il a bouger
                    }
                }
            }
            
        }
    }

    public void spawnAnimal()
    {
        if(arrPosition[maxNbrAnimals-1].GetComponent<PositionFile>().getIsOccupied())//si la file est pleine
        {

        }
        else//sinon
        {
            arrPosition[0].GetComponent<PositionFile>().setIsOccupied(true);//ajouter le nouvel animal
            arrAnimals[0] = (GameObject)Instantiate(lion, arrPosition[0].transform.position, Quaternion.identity);//TEMP
            //arrAnimals[maxNbrAnimals - 1] = ; CALLER LA FONCTION A DOMO
        }
    }
}
