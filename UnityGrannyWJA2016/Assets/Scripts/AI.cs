using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

    private const float DRINK_TIME = 2f;

    private Animal animal;
    public Animal[] animalList;
    private bool isActive,
                 isEscaped;
    private enum states { drinking, moving, chasing };
    private float moveSpeed;
    private int currentState;
    private GameObject prey;
    private Vector2 ancientDirection,
                    direction;

	void Start () {
        animal = GetComponent<Animal>();
        animalList = transform.root.GetComponentsInChildren<Animal>();
        isActive = true;
        isEscaped = false;
        moveSpeed = 3f;
        currentState = (int)states.moving;
        ancientDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        StartCoroutine(AnimalStates());
	}
	
	void Update () {
        switch(currentState) {
            case (int)states.moving:
                direction = Wander();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                ancientDirection = direction;

                foreach(Animal a in animalList) {
                    float distance = GetDistance(a.transform);
                    if (distance > 1 && distance < 10 && a.GetComponent<AI>().isEscaped) {
                        prey = a.gameObject;
                        currentState = (int)states.chasing;
                    }
                }

                break;

            case (int)states.chasing:
                Debug.Log("BLEBLEBLE");
                float distancePrey = GetDistance(prey.transform);
                if (distancePrey < 10)
                    Seek(prey.transform.position);
                else if(distancePrey < 1)
                    currentState = (int)states.moving;
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
        movingTo.x = Mathf.Abs(pos.x - transform.position.x);
        movingTo.y = Mathf.Abs(pos.y - transform.position.y);

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

    void OnTriggerEnter2D(Collider2D other) {
        if (Random.Range(40, 45) == 42 && !isEscaped && other.name == "Porte") {
            Debug.Log("Fuck off i'm out");
            isEscaped = true;
            return;
        }
        ancientDirection.x *= -1;
        ancientDirection.y *= -1;
    }

    IEnumerator AnimalStates() {
        while(isActive) {
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));

            switch(Random.Range(0, 2)) {
                case 0:
                    currentState = (int)states.moving;
                    break;
                case 1:
                    currentState = (int)states.drinking;
                    yield return new WaitForSeconds(DRINK_TIME);
                    currentState = (int)states.moving;
                    break;
            }
        }
    }
}
