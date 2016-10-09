using UnityEngine;
using System.Collections;

public class rondCage : MonoBehaviour {

    [SerializeField]
    SpriteRenderer leRond;

    [SerializeField]
    Sprite[] ronds;//array des differents ronds de couleur differentes

    [SerializeField]
    TextMesh nbAnimaux;//text du nombre d'animaux dans la cage

    [SerializeField]
    TextMesh nbAnimauxMax;// text du nombre d'animaux que peut contenir la cage

    int current = 0;//le nombre d'animal present dans la cage en ce moment

    // Use this for initialization
    void Start () {

        nbAnimaux.GetComponent<MeshRenderer>().sortingLayerName = "Rond";
        nbAnimaux.GetComponent<MeshRenderer>().sortingOrder = 1;
        nbAnimauxMax.GetComponent<MeshRenderer>().sortingLayerName = "Rond";
        nbAnimauxMax.GetComponent<MeshRenderer>().sortingOrder = 1;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addAnimal()
    {
        current++;
        nbAnimaux.text = current.ToString();
        if(current == 4)
        {
            nbAnimaux.color = new Color(255, 255, 255);
            nbAnimauxMax.color = new Color(255, 255, 255);
        }
    }

    public void removeAnimal()
    {
        current--;
        nbAnimaux.text = current.ToString();
        if (current == 3)
        {
            nbAnimaux.color = new Color(69, 40, 60);
            nbAnimauxMax.color = new Color(69, 40, 60);
        }
    }

    public void setRond(int i)
    {
        leRond.sprite = ronds[i];
    }
}
