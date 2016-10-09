using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoolDownController : MonoBehaviour {

    public float CoolDownTime;
    public GameObject monText;
    public GameObject monCircle;
    public GameObject Cage;

    public Quaternion laRotation;
    public int laCouleur;

    [SerializeField]
    GameObject rondASpawner;

    float TimeCooldown;

    void Awake ()
    {
        TimeCooldown = CoolDownTime;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TimeCooldown -= Time.deltaTime;

        if(TimeCooldown <= 0)
        {
            //spaw la cage
            GameObject maCage = Instantiate(Cage, transform.position, laRotation) as GameObject;
            maCage.name = "MaCage";
            maCage.transform.parent = GameObject.Find("Bateau").transform;
            maCage.transform.rotation = laRotation;
            maCage.GetComponent<CageController>().couleurCage = laCouleur;
            maCage.GetComponent<AuraCageSpawner>().SetAura();
            //spawn le rond dla cage
            GameObject temp = Instantiate(rondASpawner, maCage.transform.position + new Vector3(-0.49f, 0.49f, 0), Quaternion.identity) as GameObject;
            temp.GetComponent<rondCage>().setRond(laCouleur);
            temp.transform.parent = maCage.transform;
            maCage.GetComponent<CageController>().setRond(temp);

            Destroy(gameObject);
        }
	}

    void OnGUI()
    {
        monText.GetComponent<Text>().text = TimeCooldown.ToString("0");
        monCircle.GetComponent<Image>().fillAmount = (TimeCooldown / CoolDownTime);
    }
}
