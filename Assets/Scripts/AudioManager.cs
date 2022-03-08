using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource headShotAudio;
    // Start is called before the first frame update
    public void PlayHeadShotAudio()
    {
        if (!headShotAudio.isPlaying)
        {
            headShotAudio.Play();
        }
    }
}
