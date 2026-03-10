using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public AudioSource crowd; // Single AudioSource for simplicity
    public AudioClip normalCheer;
    public AudioClip hypeCheer;
    public AudioClip booing;
    private bool hasBooed = false;


    public void PlayCheer(int combo)
    {
        if (combo == 20 && hypeCheer != null)
        {
            crowd.PlayOneShot(hypeCheer);
        }
        else if (combo == 10 && normalCheer != null)
        {
            crowd.PlayOneShot(normalCheer);
        }
    }
    public void PlayBoo(int combo)
    {
        if (combo == 0 && !hasBooed && booing != null)
        {
            crowd.PlayOneShot(booing);
            hasBooed = true;
        }
        else if (combo > 0)
        {
            // Reset the flag when player starts hitting notes again
            hasBooed = false;
        }
    
    }

}
