using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    [SerializeField] Camera cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = cam.transform.position;

	}
}
