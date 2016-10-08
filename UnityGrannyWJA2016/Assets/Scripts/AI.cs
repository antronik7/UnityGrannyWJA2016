using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

    private const float DRINK_TIME = 2f;

    private Animal thisAnimal;
    public Animal[] animalList;
    private bool isActive,
                 isEscaped;
    private enum states { drinking, wandering, chasing, beingCarried };
    private float moveSpeed;
    private int currentState;
    private GameObject prey;
    private Transform boat;
    private Vector2 ancientDirection,
                    direction;

	void Start () {
        thisAnimal = GetComponent<Animal>();
        thisAnimal.setId(); //TEMPORAIRE KJDBFOIUSBEGOIUBSGVIOBG
        boat = thisAnimal.transform.root;
        isActive = true;
        isEscaped = false;
        moveSpeed = 0.4f;
        currentState = (int)states.wandering;
        Vector2 startDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        ancientDirection = startDirection;
        direction = startDirection;
        StartCoroutine(AnimalStates());
	}
	
	void Update () {
        animalList = transform.root.GetComponentsInChildren<Animal>();

        if (currentState != (int)states.beingCarried && transform.parent.tag == "Player") {
            Debug.Log("BLEBLEBLE");
            currentState = (int)states.beingCarried;
        }

        switch (currentState) {
            case (int)states.wandering:
                direction = Wander();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                ancientDirection = direction;

                if (isEscaped)
                    foreach(Animal a in animalList) {
                        if (a.GetComponent<AI>().isEscaped && a.getId() != thisAnimal.getId() && thisAnimal.getType() == false && a.getType() == true) {
                            float distance = GetDistance(a.transform);
                            if (distance < 10) {
                                prey = a.gameObject;
                                currentState = (int)states.chasing;
                            }
                        }
                    }
                break;

            case (int)states.chasing:
                float distancePrey = GetDistance(prey.transform);
                if (distancePrey < 1.5f) {
                    currentState = (int)states.wandering;
                    Destroy(prey.gameObject);
                    prey = null;
                }
                else if (distancePrey < 10) {
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
                if (transform.parent == null) {
                    currentState = (int)states.wandering;
                    transform.parent = boat.transform;
                    Debug.Log("DROPPED!!");
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

    float GetDistance(Transform other) {
        return Mathf.Sqrt(Mathf.Pow(other.position.x - transform.position.x, 2) + Mathf.Pow(other.position.y - transform.position.y, 2));
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (/*Random.Range(40, 45)*/ 42 == 42 && !isEscaped && !thisAnimal.getCouple() && other.gameObject.tag == "Porte") {
            StartCoroutine(AnimalEscape());
            return;
        }

        if(other.gameObject.name == "MurGauche" || other.gameObject.name == "MurDroite")
            ancientDirection.x *= -1;
        if(other.gameObject.name == "MurHaut" || other.gameObject.name == "MurBas")
            ancientDirection.y *= -1;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.name == "La cage" && isEscaped) {
            transform.parent = other.gameObject.transform;
            isEscaped = false;
        }
    }

    IEnumerator AnimalStates() {
        while(isActive) {
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));

            if(Random.Range(0, 2) == 1 && currentState == (int)states.wandering) {
                currentState = (int)states.drinking;
                yield return new WaitForSeconds(DRINK_TIME);
                currentState = (int)states.wandering;
            }
        }
    }

    IEnumerator AnimalEscape() {
        thisAnimal.GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("Fuck off i'm out " + thisAnimal.getId());
        yield return new WaitForSeconds(1f);
        thisAnimal.transform.parent = transform.root;
        thisAnimal.GetComponent<BoxCollider2D>().enabled = true;
        isEscaped = true;
    }
}
