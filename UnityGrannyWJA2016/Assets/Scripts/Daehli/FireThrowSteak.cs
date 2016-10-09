using UnityEngine;
using System.Collections;

public class FireThrowSteak : MonoBehaviour {

	public Vector2 direction;
	public GameObject steak;
	public GameObject instObject;
	public Vector2 force;
	private Rigidbody2D rb;
	private float minimaldistance = 1f;

	// Use this for initialization
	void Start () {
		direction = transform.position;
		StartCoroutine (throwSteak ());
	
	}
	
	// Update is called once per frame
	void Update () {

		var vel = rb.velocity=force;
		var speed = vel.magnitude;
		//Vector2 vector = new Vector2(2,-8.5f);
		if (Vector2.Distance(instObject.transform.position, instObject.GetComponent<viandeManager>().initPosition) > speed) {
			force = new Vector2 (0, 0);
			rb.velocity = force;
			rb.gravityScale=0;
		}
	}

	IEnumerator throwSteak(){
		for (int i = 0; i < 100; i++) {
			if (i % 2 == 0) {
				instObject  = (GameObject)Instantiate (steak, direction, Quaternion.identity);
				instObject.GetComponent<viandeManager> ().initPosition = direction;
				instObject.transform.SetParent (transform);
				rb = instObject.GetComponent<Rigidbody2D> ();
				force = new Vector2 (getRandon(), getRandon());
				yield return new WaitForSeconds (3);
			}
		}
	}

	static float getRandonNegative()
	{
		float randon = Random.Range (-4.0f, -2.0f);
		return randon;
	}

	static float getRandonPositif()
	{
		float randon = Random.Range (2f, 4f);
		return randon;
	}

	static float getRandon(){

		int random = Random.Range (0, 100);
		if (random % 2 == 0) {
			return getRandonPositif ();
		} else {
			return getRandonNegative ();
		}
	}


}
