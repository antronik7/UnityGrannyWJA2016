using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int SpeedPerso = 1;
    public LayerMask myLayerMask;

    bool facingRight = true;
    GameObject objetPogner = null;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Jump"))
        {
            float positionCircleCast;

            if (facingRight)
            {
                positionCircleCast = 0.3f;
            }
            else
            {
                positionCircleCast = -0.3f;
            }

            Vector2 originCircleCast = new Vector2(transform.position.x + positionCircleCast, transform.position.y);

            if (objetPogner != null)
            {
                //Caller fonction domo

                objetPogner.transform.position = new Vector3(transform.position.x + positionCircleCast, transform.position.y, 0);
                objetPogner.GetComponent<BoxCollider2D>().enabled = true;
                objetPogner.transform.parent = null;

                objetPogner = null;
            }
            else
            {
                RaycastHit2D hit = Physics2D.CircleCast(originCircleCast, 0.2f, Vector2.right, 0.3F, myLayerMask);

                if (hit.collider != null)
                {

                    objetPogner = hit.collider.gameObject;

                    if (objetPogner.tag == "Animal")
                    {
                        //Caller fonction domo
                        objetPogner.transform.parent = transform;
                        objetPogner.GetComponent<BoxCollider2D>().enabled = false;
                        objetPogner.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, 0);
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 vectorDirection = new Vector3(horizontalAxis, verticalAxis, 0);
        vectorDirection = vectorDirection.normalized;

        GetComponent<Rigidbody2D>().velocity = vectorDirection * SpeedPerso;

        if(objetPogner != null)
        {
            Debug.Log("allo");
            objetPogner.GetComponent<Rigidbody2D>().velocity = vectorDirection * SpeedPerso;
        }

        if (horizontalAxis > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalAxis < 0 && facingRight)
        {
            Flip();
        }


    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
