using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

    //Tableau contenant les gameObject animal a spawner
    [SerializeField] GameObject[] animals;

    //Si 0 : Mauve, Si 1 : Orange, Si 2 : Vert 
    [SerializeField] int typeOfSpawner;
  
    //Garder le nombre random genere;
    int randTemp = 0;

    //Garder le nbr d'animal spawner
    int nbrOfSpawnAnimal = 0;

    GameObject animalSpawner = null;

    //Time before spawn
    [SerializeField] float timeBeforeSpwan = 0;

    //Max temps avant spawn
    [SerializeField] float maxTimeBeforeSpawn;

    //Min temps avant spawn
    [SerializeField] float minTimeBeforeSpawn;

    [SerializeField] GameObject file;

    // Use this for initialization
    void Start () {
        setNewTimerToSpawn(minTimeBeforeSpawn, maxTimeBeforeSpawn);

        file.GetComponent<FileAttente>().setId(typeOfSpawner);
    }
	
	// Update is called once per frame
	void Update () {

        timeBeforeSpwan = timeBeforeSpwan - Time.deltaTime;

        if(timeBeforeSpwan <= 0)
        {
            randTemp = Random.Range(0, animals.Length);

            //Instancier l'animal
            animalSpawner = (GameObject)(Instantiate(animals[randTemp], new Vector3(1000,1000,1000), Quaternion.identity));

            //Associer le id a l'animal
            animalSpawner.GetComponent<Animal>().setId();

            //Associer la couleur a l'animal spawner
            animalSpawner.GetComponent<Animal>().setColor(typeOfSpawner);

            file.GetComponent<FileAttente>().spawnAnimal(animalSpawner);

            setNewTimerToSpawn(minTimeBeforeSpawn, maxTimeBeforeSpawn);
        }
    }

    //Fonction qui set le temps du timer
    void setNewTimerToSpawn(float min, float max)
    {
       timeBeforeSpwan = Random.Range(min, max);
    }

    float getTimeBeforeSpawn()
    {
        return timeBeforeSpwan;
    }
}
