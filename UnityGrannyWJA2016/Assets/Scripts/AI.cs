using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

    private const float DRINK_TIME = 2f;

    private Animal thisAnimal;
    private Animal[] animalList;
    private bool isActive,
                 isEscaped,
                 wantToEscape;
    private enum states { inCage, drinking, wandering, chasing, beingCarried };
    private GameObject cageDoor,
                       prey;
    private float moveSpeed; //faster when chasing
    private int currentState;
    private Transform boat;
    private Vector2 ancientDirection,
                    direction;

	void Start () {
        thisAnimal = GetComponent<Animal>();
<<<<<<< HEAD
        boat = thisAnimal.transform.root;
=======
        thisAnimal.setId(); //TEMPORAIRE KJDBFOIUSBEGOIUBSGVIOBG
>>>>>>> 2f75ec05b3eadcd1ceea0edfe76658b6f904ccef
        isActive = true;
        isEscaped = false;
        wantToEscape = false;

        foreach (BoxCollider2D g in transform.parent.GetComponentsInChildren<BoxCollider2D>())
            if (g.tag == "Porte")
                cageDoor = g.gameObject;

        moveSpeed = 0.4f;
        currentState = (int)states.inCage;
        boat = thisAnimal.transform.root;

        Vector2 startDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        ancientDirection = startDirection;
        direction = startDirection;

        StartCoroutine(AnimalStates());
	}
	
	void Update () {
        animalList = transform.root.GetComponentsInChildren<Animal>();

        if (currentState != (int)states.beingCarried && transform.parent.tag == "Player") {
            Debug.Log("PICKED ANIMAL");
            currentState = (int)states.beingCarried;
        }

        switch (currentState) {
            case (int)states.inCage:
                moveSpeed = 0.4f;

                if (wantToEscape && !isEscaped) {
                    direction = Seek(cageDoor.transform.position);
                    StartCoroutine(AnimalEscape());
                }
                else
                    direction = Wander();

                transform.Translate(direction * moveSpeed * Time.deltaTime);
                ancientDirection = direction;

                break;

            case (int)states.wandering:
                moveSpeed = 0.4f;
                direction = Wander();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                ancientDirection = direction;

                if (isEscaped)
                    foreach(Animal a in animalList) {
                        if (a.GetComponent<AI>().isEscaped && a.getId() != thisAnimal.getId() && thisAnimal.getType() == false && a.getType() == true) {
                            float distance = GetDistance(a.transform.position);
                            if (distance < 1) {
                                prey = a.gameObject;
                                currentState = (int)states.chasing;
                            }
                        }
                    }

                break;

            case (int)states.chasing:
                moveSpeed = 0.5f;
                float distancePrey = GetDistance(prey.transform.position);

                if (distancePrey < 0.15f) {
                    currentState = (int)states.wandering;
                    Debug.Log("DESTROY");
                    Destroy(prey.gameObject);
                    prey = null;
                }
                else if (distancePrey < 1) {
                    direction = Seek(prey.transform.position);
                    transform.Translate(direction * moveSpeed * Time.deltaTime);
                    ancientDirection = direction;
                }
                else {
                    currentState = (int)states.wandering;
                    prey = null;
                }
                    
                break;

            case (int)states.beingCarried:
                moveSpeed = 0;

                if (transform.parent == null) {
                    currentState = (int)states.wandering;
                    transform.parent = boat.transform;
                    Debug.Log("DROPPED ANIMAL");
                } 

                break;
        }
    }

    Vector2 Wander() {
        direction.x = ancientDirection.x + Random.Range(-0.1f, 0.1f);
        direction.y = ancientDirection.y + Random.Range(-0.1f, 0.1f);

        return Normalize(direction);
    }

    Vector2 Seek(Vector2 pos) {
        Vector2 movingTo;
        movingTo.x = pos.x - transform.position.x;
        movingTo.y = pos.y - transform.position.y;
        return Normalize(movingTo);
    }

    Vector2 Normalize(Vector2 v) {
        float length = Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2));
        Vector2 temp;

        temp.x = v.x / length;
        temp.y = v.y / length;

        return temp;
    }

    float GetDistance(Vector2 other) {
        return Mathf.Sqrt(Mathf.Pow(other.x - transform.position.x, 2) + Mathf.Pow(other.y - transform.position.y, 2));
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "MurGauche" || other.gameObject.name == "MurDroite")
            ancientDirection.x *= -1;
        if(other.gameObject.name == "MurHaut" || other.gameObject.name == "MurBas")
            ancientDirection.y *= -1;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.name == "La cage" && !isEscaped) {
            transform.parent = other.gameObject.transform;
            isEscaped = true;
        }
    }

    IEnumerator AnimalStates() {
        while(isActive) {
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
            int rand = Random.Range(0, 10);

            if (rand < 5 && currentState == (int)states.wandering) {
                currentState = (int)states.drinking;
                yield return new WaitForSeconds(DRINK_TIME);
                currentState = (int)states.wandering;
            }
            else if (rand > 7 && !isEscaped)
                wantToEscape = true;
        }
    }

    IEnumerator AnimalEscape() {
        wantToEscape = false;
        thisAnimal.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitUntil(() => isEscaped);
        Debug.Log("Fuck off i'm out " + thisAnimal.getId());
        currentState = (int)states.wandering;
        thisAnimal.transform.parent = transform.root;
        thisAnimal.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
