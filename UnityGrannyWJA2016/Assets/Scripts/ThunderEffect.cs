using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThunderEffect : MonoBehaviour {

	 public CanvasGroup myCG;
     private bool flash = false;

     private float cptFlash;
     
     void Start(){
     	//CanvasGroup = GetComponent<CanvasGroup>();
     	cptFlash = Random.Range(15,30);
     }

     void Update ()
     {
     	if(cptFlash>0){
     		cptFlash-=Time.deltaTime;
     	}else{
     		MineHit();
     	}
        if (flash)
        {
             myCG.alpha = myCG.alpha - Time.deltaTime;
             if (myCG.alpha <= 0)
             {
                 myCG.alpha = 0;
                 flash = false;
             }
         }
     }
     
     public void MineHit ()
     {
     	GetComponent<AudioSource>().Play();
         flash = true;
         myCG.alpha = 1;
        cptFlash = Random.Range(15,30);
     }
 }
