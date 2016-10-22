using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public List<int> couleurCageSpawner;
    public List<GameObject> listeCage;

    [SerializeField]
    GameObject rondASpawner;

    //Tableau des spawner
    [SerializeField] GameObject[] allSpawner;

    [SerializeField]
    GameObject[] GoduHands;

    [SerializeField]
    GameObject FeedbackScore;

    [SerializeField]
    GameObject FeedbackAmour;

    [SerializeField]
    GameObject Hud;

    [SerializeField]
    GameObject player;

    //L'amour de dieu
    public float playerLife = 100;

    //Score du joueur
    public int playerScore = 0;
    float Timer = 0;
    public int difficulte = 1;
    public bool CanPLay = false;

    [SerializeField]
    Animator startAnimator;
    [SerializeField]
    Animator gameOverAnimator;

    //Vriable qui permet de savoir dans quel spawn le joueur est. -1 = Aucun, 0 = Mauve, 1 = Orange, 2 = Vert.
    [SerializeField]
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

        int compteur = 0;

        while (couleurCageSpawner.Count >= 1)
        {
            int randomIndexCouleur = Random.Range(0, couleurCageSpawner.Count);

            listeCage[compteur].GetComponent<CageController>().couleurCage = couleurCageSpawner[randomIndexCouleur];
            listeCage[compteur].GetComponent<AuraCageSpawner>().SetAura();

            GameObject temp = Instantiate(rondASpawner, listeCage[compteur].transform.position + new Vector3(-0.49f, 0.49f, 0), Quaternion.identity) as GameObject;
            temp.GetComponent<rondCage>().setRond(couleurCageSpawner[randomIndexCouleur]);
            temp.transform.parent = listeCage[compteur].transform;
            listeCage[compteur].GetComponent<CageController>().setRond(temp);

            couleurCageSpawner.RemoveAt(randomIndexCouleur);

            compteur++;
        }
    }

    void Start ()
    {
        startAnimator.SetBool("play", true);
    }

    void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= 0 && Timer < 59)
        {
            difficulte = 1;
        }
        else if ((Timer >= 60 && Timer < 119))
        {
            difficulte = 2;
        }
        else
        {
            difficulte = 3;
        }

        if(playerLife <= 0)
        {
            CanPLay = false;
            gameOverAnimator.SetBool("play", true);
        }
    }

    //Fonction appler par le personnage quand il ramasse un animal
    public void playerTakeAnimalInSpawn()
    {
        allSpawner[spawnActif].GetComponent<FileAttente>().takeAnimal();
        switch (spawnActif)//updater le hud selon la file
        {
            case 0:
                Hud.GetComponent<HudManager>().FileMauve.GetComponent<TextFile>().decrementAnimaux();
                break;
            case 1:
                Hud.GetComponent<HudManager>().FileOrange.GetComponent<TextFile>().decrementAnimaux();
                break;
            case 2:
                Hud.GetComponent<HudManager>().FileVerte.GetComponent<TextFile>().decrementAnimaux();
                break;
        }
    }

    //ajouter un au compteur d'animaux de la file recu en parametre
    public void addAnimalInFileHud(int file)
    {
        switch (file)//updater le hud selon la file
        {
            case 0:
                Hud.GetComponent<HudManager>().FileMauve.GetComponent<TextFile>().incrementAnimaux();
                break;
            case 1:
                Hud.GetComponent<HudManager>().FileOrange.GetComponent<TextFile>().incrementAnimaux();
                break;
            case 2:
                Hud.GetComponent<HudManager>().FileVerte.GetComponent<TextFile>().incrementAnimaux();
                break;
        }
    }

    public void damageToHud(float damage)
    {
        //lose life
        playerLife -= damage;
        Hud.GetComponent<HudManager>().HealthBar.GetComponent<HealthBar>().loseAmourDeDieu(damage);
        GoduHands[0].GetComponent<Animator>().SetBool("play", true);//feedback god
        GameObject temp = Instantiate(FeedbackAmour, player.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
        temp.GetComponent<FbScore>().setTextAmour(damage);
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

    public void addScore(int s)
    {
        s *= 100;
        //add life
        playerLife += 5;
        Hud.GetComponent<HudManager>().HealthBar.GetComponent<HealthBar>().gainAmourDeDieu(5);
        GoduHands[1].GetComponent<Animator>().SetBool("play", true);//feedback god

        //add score
        playerScore += s;
        Hud.GetComponent<HudManager>().Score.GetComponent<Score>().addScore(s);
        GameObject temp = Instantiate(FeedbackScore, player.transform.position + new Vector3(0,0.25f,0) , Quaternion.identity) as GameObject;
        temp.GetComponent<FbScore>().setTextScore(s);
       
    }
    
    public void DestroyGameManager()
    {
        Destroy(gameObject);
    }

}

       