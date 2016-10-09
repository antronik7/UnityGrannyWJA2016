using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    //Tableau des spawner
    [SerializeField] GameObject[] allSpawner;

    public GameObject Hud;

    //L'amour de dieu
    public float playerLife;

    //Score du joueur
    public int playerScore;


    //Vriable qui permet de savoir dans quel spawn le joueur est. -1 = Aucun, 0 = Mauve, 1 = Orange, 2 = Vert.
    int spawnActif = -1;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
     
        //If instance already exists and it's not this:
        else if (instance != this)

        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    //Fonction appler par le personnage quand il ramasse un animal
    public void playerTakeAnimalInSpawn()
    {
        allSpawner[spawnActif].GetComponent<FileAttente>().takeAnimal();
        switch (spawnActif)//updater le hud selon la file
        {
            case 0:
                GameManager.instance.Hud.GetComponent<HudManager>().FileMauve.GetComponent<TextFile>().decrementAnimaux();
                break;
            case 1:
                GameManager.instance.Hud.GetComponent<HudManager>().FileOrange.GetComponent<TextFile>().decrementAnimaux();
                break;
            case 2:
                GameManager.instance.Hud.GetComponent<HudManager>().FileVerte.GetComponent<TextFile>().decrementAnimaux();
                break;
        }
    }

    //Quand le joueur entre dans le trigger box du spawn, le spawn appel cette fonction pour setter son spawn comme spawn actif
    public void setSpawnActive(int spawnRecu)
    {
        spawnActif = spawnRecu;
    }

    public void deSetSpawnActive()
    {
        spawnActif = -1;
    }

}

       