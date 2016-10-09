using UnityEngine;
using System.Collections;

public class FileAttente : MonoBehaviour {

    int animalCount = 0;
    [SerializeField] int maxNbrAnimals = 19;
    [SerializeField] int idSpawn;
    public float damage;

    [SerializeField] GameObject[] arrPosition;//array de positions 0 = debut et lengt = fin
    GameObject[] arrAnimals;//array d'animaux 0 = debut et lengt = fin

	// Use this for initialization
	void Start () { 
        arrAnimals = new GameObject[maxNbrAnimals]; //initialisation array animals
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

    public void spawnAnimal(GameObject animal)
    {
        
        if(maxNbrAnimals == animalCount)//si la file est pleine
        {
            //prendre un degat
            GameManager.instance.damageToHud(damage);
            Destroy(animal);
        }
        else//sinon
        {
            //ajouter le nouvel animal
            animalCount++;
            arrAnimals[0] = animal;
            arrAnimals[0].transform.position = arrPosition[0].transform.position;
            arrPosition[0].GetComponent<PositionFile>().setIsOccupied(true);
        }
    }

    public void takeAnimal()
    {
        //enlever un animal quand NOE YER GAY
        if (animalCount > 0 && arrPosition[maxNbrAnimals - 1].GetComponent<PositionFile>().getIsOccupied())
        {
            arrAnimals[maxNbrAnimals - 1] = null;
            animalCount--;
            arrPosition[maxNbrAnimals - 1].GetComponent<PositionFile>().setIsOccupied(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.instance.setSpawnActive(idSpawn);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.deSetSpawnActive();
        }
    }

    //Fonction appler dans le start d'un spawner pour lui setter son id
    public void setId(int id)
    {
        idSpawn = id;
    }
}
