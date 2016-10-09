using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float SpeedPerso = 1;
    public LayerMask myLayerMask;
    public LayerMask myLayerMaskCage;
    public LayerMask myLayerMaskEntrepot;
    public LayerMask myLayerMaskEntree;
    public GameObject cageVictoire;
    public GameObject PositionStorage;
    public GameObject ArrowRotator;
    public GameObject ArrowSprite;

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
        Quaternion rotation = Quaternion.LookRotation (PositionStorage.transform.position - ArrowRotator.transform.position, ArrowRotator.transform.TransformDirection(Vector3.up));
        ArrowRotator.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

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
                Debug.Log(Input.GetAxis("Vertical"));
                if (Input.GetAxis("Vertical") > 0.4f)
                {
                    positionCircleCast = 0.4f;
                    originCircleCast = new Vector2(transform.position.x, transform.position.y + positionCircleCast);
                }
                else if (Input.GetAxis("Vertical") < -0.4f)
                {
                    positionCircleCast = -0.3f;
                    originCircleCast = new Vector2(transform.position.x, transform.position.y + positionCircleCast);
                }

                if (objetPogner.tag == "Cage") //DEPOSE ENTREPOT
                {

                    RaycastHit2D hit = Physics2D.CircleCast(originCircleCast, 0.05f, Vector2.right, 0F, myLayerMaskEntrepot);

                    if (hit.collider != null)
                    {
                        DeposeEntrepot(originCircleCast);
                    }
                }
                else
                {
                    RaycastHit2D hit = Physics2D.CircleCast(originCircleCast, 0.0005f, Vector2.right, 0F, myLayerMaskCage);
                    RaycastHit2D hitEntree = Physics2D.CircleCast(originCircleCast, 0.0005f, Vector2.right, 0F, myLayerMaskCage);

                    if (hit.collider != null) //DEPOSE DANS UNE CAGE
                    {
                        if (hit.collider.gameObject.GetComponent<CageController>().couleurCage == objetPogner.GetComponent<Animal>().getColor())
                        {
                            if(hit.collider.gameObject.GetComponent<CageController>().getNbrAnimalInCage() < 4)
                            {
                                DeposeCage(hit.collider.gameObject, originCircleCast);
                            }
                            
                        }
                    }
                    else if (hitEntree.collider == null)
                    {
                        DeposeBateau(originCircleCast);
                    }
                    /*else //DEPOSE A TERRE
                    {
                        DeposeBateau(originCircleCast);
                    }*/
                }
            }
            else
            {
                RaycastHit2D hit = Physics2D.CircleCast(originCircleCast, 0.05f, Vector2.right, 0F, myLayerMask);

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
            RaycastHit2D hitCage = Physics2D.CircleCast(originCircleCast, 0.05f, Vector2.right, 0F, myLayerMaskCage);

            hitCage.collider.gameObject.GetComponent<CageController>().animalExitCage(objetPogner);
        }

        objetPogner.GetComponent<Animal>().setgrabed(true);

        objetPogner.transform.parent = transform;
        objetPogner.GetComponent<BoxCollider2D>().enabled = false;
        objetPogner.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, 0);
    }

    void DeposeBateau(Vector3 originCircleCast)
    {
        monAnimator.SetTrigger("Depose");

        objetPogner.transform.position = originCircleCast;
        objetPogner.GetComponent<BoxCollider2D>().enabled = true;
        objetPogner.transform.parent = null;

        objetPogner.GetComponent<Animal>().setZone(1);
        objetPogner.GetComponent<Animal>().setgrabed(false);

        objetPogner = null;
    }

    void DeposeCage(GameObject cage, Vector3 originCircleCast)
    {
        monAnimator.SetTrigger("Depose");

        objetPogner.GetComponent<Animal>().setZone(2);
        objetPogner.GetComponent<Animal>().setgrabed(false);


        objetPogner.transform.position = originCircleCast;
        objetPogner.GetComponent<BoxCollider2D>().enabled = true;
        objetPogner.transform.parent = null;

<<<<<<< HEAD
        //DERNIER ANIMAL
        if (cage.GetComponent<CageController>().setAnimalInCage(objetPogner))
=======
        if (cage.GetComponent<CageController>().setAnimalInCage(objetPogner))//Quand on full une cage avec le dernier animal
>>>>>>> a52bbf5cd6bf7c5d55b5d5d07f3ca045b4b896f4
        {
            objetPogner = Instantiate(cageVictoire);
            objetPogner.transform.parent = transform;
            objetPogner.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, 0);
            monAnimator.SetTrigger("Porte");
            prochainScore = cage.GetComponent<CageController>().calculateScore();
            ArrowSprite.GetComponent<SpriteRenderer>().enabled = true;

            cage.GetComponent<CageController>().startCoolDown();
        }
        else
        {
            objetPogner = null;
        }
    }

    void DeposeEntrepot(Vector3 originCircleCast)
    {
        monAnimator.SetTrigger("Depose");

        objetPogner.transform.position = originCircleCast;
        objetPogner.GetComponent<BoxCollider2D>().enabled = true;
        objetPogner.transform.parent = null;

        GameManager.instance.addScore(prochainScore);

        //AjouterScore
        Destroy(objetPogner);
        objetPogner = null;

        ArrowSprite.GetComponent<SpriteRenderer>().enabled = false;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), 0.05f);
    }
}
