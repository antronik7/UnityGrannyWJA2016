using UnityEngine;
using System.Collections;

public class GestionnaireSonBad : MonoBehaviour {

    public AudioClip bad;

    public void playBadSound()
    {
        AudioSource.PlayClipAtPoint(bad, Vector3.zero, 1f);
    }
}
