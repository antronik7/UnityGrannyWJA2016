﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

    private const float DRINK_TIME = 2f;

    private Animal thisAnimal;
    public Animal[] animalList;
    public bool isActive,
                 isEscaped,
                 wantToEscape;
    private enum states { inCage, drinking, wandering, chasing, beingCarried };
    [SerializeField] private GameObject cageDoor;
    [SerializeField]private GameObject prey;
    private float moveSpeed;
    [SerializeField]private int currentState,
                                previousState;
    private Transform boat,
                      player;
    private Vector2 ancientDirection;
    [SerializeField] private Vector2 direction;

	void Start () {
        thisAnimal = GetComponent<Animal>();

        isActive = true;
        isEscaped = true;
        wantToEscape = false;

        moveSpeed = 0.4f;
        currentState = (int)states.inCage;
        boat = GameObject.Find("Bateau").transform;
        player = transform.parent;

        Vector2 startDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        ancientDirection = startDirection;
        direction = startDirection;

        StartCoroutine(AnimalStates());
	}
	
	void Update () {
        animalList = boat.GetComponentsInChildren<Animal>();

        if (Mathf.Sign(ancientDirection.x) == -1)
            thisAnimal.GetComponent<SpriteRenderer>().flipX = true;
        if (Mathf.Sign(ancientDirection.x) == 1)
            thisAnimal.GetComponent<SpriteRenderer>().flipX = false;

        if (thisAnimal.getCouple() == true)
            wantToEscape = false;

        if (currentState != (int)states.beingCarried && transform.parent.tag == "Player") {
            transform.parent = player;
            currentState = (int)states.beingCarried;
        }

        switch (currentState) {
            case (int)states.inCage:
                moveSpeed = 0.4f;
                if (wantToEscape && !isEscaped) {
                    direction = Seek(cageDoor.transform.position);
                    ancientDirection = direction;
                    StartCoroutine(AnimalEscape());
                }
                else
                    direction = Wander();

                transform.Translate(direction * moveSpeed * Time.deltaTime);
                ancientDirection = direction;

                if(thisAnimal.getCouple() == false)
                    foreach (Animal a in animalList) {
                        if (!a.GetComponent<AI>().isEscaped && a.getId() != thisAnimal.getId() && thisAnimal.getType() == false && a.getType() == true) {
                            float distance = GetDistance(a.transform.position);
                            if (distance < 2 && prey == null) {
                                prey = a.gameObject;
                                previousState = currentState;
                                currentState = (int)states.chasing;
                            }
                        }
                    }

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
                            if (distance < 2) {
                                prey = a.gameObject;
                                previousState = currentState;
                                currentState = (int)states.chasing;
                            }
                        }
                    }

                break;

            case (int)states.chasing:
                if(prey == null) 
                    currentState = (int)states.wandering;
                else {
                    moveSpeed = 0.5f;
                    float distancePrey = GetDistance(prey.transform.position);
                    if (distancePrey < 0.2f) {
                        currentState = previousState;
                        if(thisAnimal.getZone() == 2)
                            thisAnimal.GetComponentInParent<CageController>().animalExitCage(prey);
                        Debug.Log("DESTROY");
                        Destroy(prey.gameObject);
                        GameManager.instance.damageToHud(3);
                        prey = null;
                    }
                    else if (distancePrey < 2) {
                        direction = Seek(prey.transform.position);
                        transform.Translate(direction * moveSpeed * Time.deltaTime);
                        ancientDirection = direction;
                    }
                    else {
                        currentState = previousState;
                        prey = null;
                    }
                }
                  
                break;

            case (int)states.beingCarried:
                moveSpeed = 0;
                thisAnimal.GetComponent<SpriteRenderer>().flipX = false;

                if (thisAnimal.getZone() == 1 && !thisAnimal.getgrabed()) {
                    currentState = (int)states.wandering;
                    transform.parent = boat.transform;
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
        movingTo.x = (pos.x - transform.position.x) * Mathf.Sign(transform.localScale.x);
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
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "MurGauche" || other.gameObject.name == "MurDroite")
            ancientDirection.x *= -1;
        if(other.gameObject.name == "MurHaut" || other.gameObject.name == "MurBas")
            ancientDirection.y *= -1;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        if(other.name == "MaCage") {
            ancientDirection = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-1.0f, 1.0f));
            transform.parent = other.gameObject.transform;
            thisAnimal.setZone(2);
            currentState = (int)states.inCage;
            isEscaped = false;

            foreach (BoxCollider2D g in transform.parent.GetComponentsInChildren<BoxCollider2D>())
                if (g.tag == "Porte")
                    cageDoor = g.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.name == "MaCage" && !isEscaped) {
            transform.parent = other.gameObject.transform;
            isEscaped = true;
            wantToEscape = false;
        }
    }

    IEnumerator AnimalStates() {
        while(isActive) {
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
            int rand = Random.Range(0, 10);

            if (rand > 7 && !isEscaped && thisAnimal.getCouple() == false)
                wantToEscape = true;

            else if (rand < 5 && (currentState == (int)states.wandering || currentState == (int)states.inCage)) {
                previousState = currentState;
                currentState = (int)states.drinking;
                thisAnimal.toggleAnimationWalkOff();
                yield return new WaitForSeconds(DRINK_TIME);
                thisAnimal.toggleAnimationWalkOn();
                currentState = previousState;
            } 
        }
    }

    IEnumerator AnimalEscape() {
        wantToEscape = false;
        thisAnimal.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitUntil(() => isEscaped);
        thisAnimal.GetComponentInParent<CageController>().animalExitCage(this.gameObject);
        Debug.Log("Fuck off i'm out " + thisAnimal.getId());
        currentState = (int)states.wandering;
        thisAnimal.transform.parent = transform.root;
        thisAnimal.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}