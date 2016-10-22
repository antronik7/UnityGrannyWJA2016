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

    [SerializeField]
    Color couleurText;

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
            nbAnimaux.color = new Color(1, 1, 1);
            nbAnimauxMax.color = new Color(1, 1, 1);
        }
    }

    public void removeAnimal()
    {
        current--;
        nbAnimaux.text = current.ToString();
        nbAnimaux.color = couleurText;
        nbAnimauxMax.color = couleurText;

    }

    public void setRond(int i)
    {
        leRond.sprite = ronds[i];
    }
}
