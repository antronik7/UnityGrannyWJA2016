using UnityEngine;
using System.Collections;

public class SonPersonnage : MonoBehaviour {

    public AudioClip take;
    public AudioClip put;
    public AudioClip jailSucces;

    public void playTakeSound()
    {
        AudioSource.PlayClipAtPoint(take, transform.position, 0.75f);
    }

    public void playPutSound()
    {
        AudioSource.PlayClipAtPoint(put, transform.position, 0.75f);
    }

    public void playJailSuccesSound()
    {
        AudioSource.PlayClipAtPoint(jailSucces, transform.position, 100);

    }
}
