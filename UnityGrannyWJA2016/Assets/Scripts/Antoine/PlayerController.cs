using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public int SpeedPerso = 1;
    public LayerMask myLayerMask;
    public LayerMask myLayerMaskCage;
    public LayerMask myLayerMaskEntrepot;
    public GameObject cage;

    bool facingRight = true;
    GameObject objetPogner = null;
    Animator monAnimator;

    int prochainScore = 0;

    // Use this for initialization
    void Start () {
        monAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (objetPogner != null)
        {
            objetPogner.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, 0);
        }


        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0 || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 0)
        {
            monAnimator.SetBool("PersoBouge", true);
        }
        else
        {
            monAnimator.SetBool("PersoBouge", false);
        }


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
                if(objetPogner.tag == "Cage") //DEPOSE ENTREPOT
                {
                    RaycastHit2D hit = Physics2D.CircleCast(originCircleCast, 0.0005f, Vector2.right, 0F, myLayerMaskEntrepot);

                    if (hit.collider != null)
                    {
                        DeposeEntrepot(positionCircleCast);
                    }
                }
                else
                {
                    RaycastHit2D hit = Physics2D.CircleCast(originCircleCast, 0.0005f, Vector2.right, 0F, myLayerMaskCage);

                    if (hit.collider != null) //DEPOSE DANS UNE CAGE
                    {
                        if (hit.collider.gameObject.GetComponent<CageController>().couleurCage == objetPogner.GetComponent<Animal>().getColor())
                        {
                            DeposeCage(hit.collider.gameObject, positionCircleCast);
                        }
                    }
                    else //DEPOSE A TERRE
                    {
                        DeposeBateau(positionCircleCast);
                    }
                }
            }
            else
            {
                RaycastHit2D hit = Physics2D.CircleCast(originCircleCast, 0.0005f, Vector2.right, 0F, myLayerMask);

                if (hit.collider != null)// Prendre Objet
                {
                    monAnimator.SetTrigger("Porte");
                    objetPogner = hit.collider.gameObject;

                    FlipObjet(objetPogner);
                    
                    if (objetPogner.tag == "Animal")
                    {
                        PrendreAnimal(objetPogner, originCircleCast);
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

    void FlipObjet(GameObject objetPogner)
    {
        if (Mathf.Sign(transform.localScale.x) != Mathf.Sign(objetPogner.transform.localScale.x))
        {
            Vector3 theScale = objetPogner.transform.localScale;
            theScale.x *= -1;
            objetPogner.transform.localScale = theScale;
        }
    }

    void PrendreAnimal(GameObject objetPogner, Vector2 originCircleCast)
    {
        if (objetPogner.GetComponent<Animal>().getZone() == 2)
        {
            RaycastHit2D hitCage = Physics2D.CircleCast(originCircleCast, 0.0005f, Vector2.right, 0F, myLayerMaskCage);

            hitCage.collider.gameObject.GetComponent<CageController>().animalExitCage(objetPogner);
        }

        objetPogner.GetComponent<Animal>().setgrabed(true);

        objetPogner.transform.parent = transform;
        objetPogner.GetComponent<BoxCollider2D>().enabled = false;
        objetPogner.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, 0);
    }

    void DeposeBateau(float positionCircleCast)
    {
        monAnimator.SetTrigger("Depose");

        objetPogner.transform.position = new Vector3(transform.position.x + positionCircleCast, transform.position.y, 0);
        objetPogner.GetComponent<BoxCollider2D>().enabled = true;
        objetPogner.transform.parent = null;

        objetPogner.GetComponent<Animal>().setZone(1);
        objetPogner.GetComponent<Animal>().setgrabed(false);

        objetPogner = null;
    }

    void DeposeCage(GameObject cage, float positionCircleCast)
    {
        monAnimator.SetTrigger("Depose");

        objetPogner.GetComponent<Animal>().setZone(2);
        objetPogner.GetComponent<Animal>().setgrabed(false);


        objetPogner.transform.position = new Vector3(transform.position.x + positionCircleCast, transform.position.y, 0);
        objetPogner.GetComponent<BoxCollider2D>().enabled = true;
        objetPogner.transform.parent = null;

        if (cage.GetComponent<CageController>().setAnimalInCage(objetPogner))
        {
            objetPogner = Instantiate(cage);
            objetPogner.transform.parent = transform;
            objetPogner.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, 0);
            monAnimator.SetTrigger("Porte");
            prochainScore = cage.GetComponent<CageController>().calculateScore();
        }
        else
        {
            objetPogner = null;
        }
    }

    void DeposeEntrepot(float positionCircleCast)
    {
        monAnimator.SetTrigger("Depose");

        objetPogner.transform.position = new Vector3(transform.position.x + positionCircleCast, transform.position.y, 0);
        objetPogner.GetComponent<BoxCollider2D>().enabled = true;
        objetPogner.transform.parent = null;

        //AjouterScore
        Destroy(objetPogner);
        objetPogner = null;
    }
}
