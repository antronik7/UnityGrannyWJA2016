using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    private const float DRINK_TIME = 2f;
    private bool isActive;
    private enum states { drinking, moving };
    private float moveSpeed;
    private int currentState;
    private Vector2 ancientDirection,
                    direction;

	void Start () {
        isActive = true;
        moveSpeed = 3f;
        currentState = (int)states.moving;
        ancientDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        StartCoroutine(AnimalStates());
	}
	
	void Update () {
        if (currentState == (int)states.moving) {
            direction = Wander();
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            ancientDirection = direction;
        }
	}

    Vector2 Wander() {
        direction.x = ancientDirection.x + Random.Range(-0.1f, 0.1f);
        direction.y = ancientDirection.y + Random.Range(-0.1f, 0.1f);

        return Normalize(direction);
    }

    Vector2 Normalize(Vector2 v) {
        float length = Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2));
        Vector2 temp;

        temp.x = v.x / length;
        temp.y = v.y / length;

        return temp;
    }

    void OnTriggerExit2D(Collider2D other) {
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
