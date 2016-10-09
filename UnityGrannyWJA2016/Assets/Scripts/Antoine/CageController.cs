using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 


public class CageController : MonoBehaviour {

    public int couleurCage;

    [SerializeField] List<GameObject> animalNotInCombo = new List<GameObject>();
    [SerializeField] List<GameObject> animalInCombo1 = new List<GameObject>();
    [SerializeField] List<GameObject> animalInCombo2 = new List<GameObject>();

    GameObject monRond;

    GameObject[] food;

    [SerializeField] int nbrOfAnimalInCage = 0;

    public GameObject CageCoolDown;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Fonction applle par le joueur quand il set un ennemie dans la cage
    public bool setAnimalInCage(GameObject animal)
    {

        if(!newAnimalIsInPair(animal))
        {
            if(!newAnimalIsInTrioOrQuad(animal))
            {
                animalNotInCombo.Add(animal);
            }  
        }

        nbrOfAnimalInCage++;
        monRond.GetComponent<rondCage>().addAnimal();

        return cageComplet();
    }

    //Fonction appelle par setAnimalInCage qui verifie si la cage est complete
    bool cageComplet()
    {
        if (nbrOfAnimalInCage == 4)
        {
            if (animalInCombo1.Count > 1 || animalInCombo2.Count > 1)
            {
                return true;
            }
        }

        return false;
    }

    public bool newAnimalIsInPair(GameObject animal)
    {
        for (int i = 0; i < animalNotInCombo.Count; i++)
        {
            if (animalNotInCombo[i].GetComponent<Animal>().getName() == animal.GetComponent<Animal>().getName())
            {
                //Creer une nouvelle paire
                if (animalInCombo1.Count < 1)
                {
                    animalInCombo1.Add(animal);
                    animalInCombo1.Add(animalNotInCombo[i]);
                }
                else
                {
                    if (animalInCombo2.Count < 1)
                    {
                        animalInCombo2.Add(animal);
                        animalInCombo2.Add(animalNotInCombo[i]);
                    }
                }

                //Indiquer qu'ils sont en couple
                animalNotInCombo[i].GetComponent<Animal>().setCouple(true);
                animal.GetComponent<Animal>().setCouple(true);

                //Enlever l'animal de la liste des animaux pas en combo
                animalNotInCombo.RemoveAt(i);

                return true;

            }
        }

        return false;
    }

    public bool newAnimalIsInTrioOrQuad(GameObject animal)
    {
        bool animalInCombo = false;

        //Pour trio ou 4 pareil
        for (int i = 0; i < animalInCombo1.Count; i++)
        {
            if (animalInCombo1[i].GetComponent<Animal>().getName() == animal.GetComponent<Animal>().getName())
            {
                animalInCombo1.Add(animal);
                animal.GetComponent<Animal>().setCouple(true);
                return true;
            }
        }

        if (!animalInCombo)
        {
            for (int i = 0; i < animalInCombo2.Count; i++)
            {
                if (animalInCombo2[i].GetComponent<Animal>().getName() == animal.GetComponent<Animal>().getName())
                {
                    animalInCombo2.Add(animal);
                    animal.GetComponent<Animal>().setCouple(true);
                    return true;
                }
            }
        }

        return false;
    }

    public int getNbrAnimalInCage()
    {
        return nbrOfAnimalInCage;
    }

    public void animalExitCage(GameObject animal)
    {
        int indexe = 0;
        bool animalAlreadyRemoveFromCombo = false;

        animal.GetComponent<Animal>().setZone(1);

        //Verifier si l'animal est dans un combo
        if(animal.GetComponent<Animal>().getCouple() == true)
        {
            //Trouver dans quel combo il est 
            for (int i = 0; i < animalInCombo1.Count; i++)
            {
                if (animalInCombo1[i].GetComponent<Animal>().getId() == animal.GetComponent<Animal>().getId())
                {
                    //Enlever l'animal du combo
                    animalInCombo1[i].GetComponent<Animal>().setCouple(false);
                    animalInCombo1.RemoveAt(i);

                    //Verifier si le combo est scraper ou pas
                    if (animalInCombo1.Count <= 1)
                    {
                        //Le combo n'existe pu
                        animalNotInCombo.Add(animalInCombo1[0]);
                        animalInCombo1[0].GetComponent<Animal>().setCouple(false);
                        animalInCombo1.RemoveAt(0);
                    }

                    i = animalInCombo1.Count + 1;
                    animalAlreadyRemoveFromCombo = true;
                }
            }

            if (!animalAlreadyRemoveFromCombo)
            {
                for (int i = 0; i < animalInCombo2.Count; i++)
                {
                    if (animalInCombo2[i].GetComponent<Animal>().getId() == animal.GetComponent<Animal>().getId())
                    {
                        //Enlever l'animal du combo
                        animalInCombo2[i].GetComponent<Animal>().setCouple(false);
                        animalInCombo2.RemoveAt(i);

                        //Verifier si le combo est scraper ou pas
                        if (animalInCombo2.Count <= 1)
                        {
                            //Le combo n'existe pu
                            animalNotInCombo.Add(animalInCombo2[0]);
                            animalInCombo2[0].GetComponent<Animal>().setCouple(false);
                            animalInCombo2.RemoveAt(0);
                        }

                        i = animalInCombo2.Count + 1;
                    }
                }
            }
        }
        else
        {
            //Trouver l'indexe de l'animal qui est sortie
            for (int i = 0; i < animalNotInCombo.Count; i++)
                if (animal.GetComponent<Animal>().getId() == animalNotInCombo[i].GetComponent<Animal>().getId()) {
                    animalNotInCombo.RemoveAt(i);
                    i = animalNotInCombo.Count + 42;
                }  
        }

        nbrOfAnimalInCage--;
        monRond.GetComponent<rondCage>().removeAnimal();

    }

    //Fonction qui calcul combien une cage va donner de points
    public int calculateScore()
    {
        //Max de point si le joueur a 4 animaux identique
        if(animalInCombo1.Count == 4 || animalInCombo2.Count == 4)
        {
            return 4;
        }

        //3 animaux identique et un random (3 points pour Gryffondor) 
        if (animalInCombo1.Count == 3 || animalInCombo2.Count == 3)
        {
            return 3;
        }

        //Si 2 couple d'animaux different
        if(animalInCombo1.Count == 2 && animalInCombo2.Count == 2)
        {
            return 2;
        }

        //Si 1 couple et 2 random
        if(animalInCombo1.Count == 2 || animalInCombo2.Count == 2)
        {
            return 1;
        }

        //Un probleme
        return -1;
    }

    public void startCoolDown()
    {
        GameObject monCoolDown = (GameObject)Instantiate(CageCoolDown, transform.position, Quaternion.identity);
        CoolDownController leCoolDownController = monCoolDown.GetComponent<CoolDownController>();
        leCoolDownController.laRotation = gameObject.transform.rotation;
        leCoolDownController.laCouleur = couleurCage;
        Destroy(gameObject);
    }

    public void setRond(GameObject  r)
    {
        monRond = r;
    }
}
