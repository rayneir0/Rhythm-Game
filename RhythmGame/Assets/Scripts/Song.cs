using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public int BPM;

    public void SetBPM(int bpm)
    {
        BPM = bpm;
    }

    public int GetBPM()
    {
        return BPM;
    }
}
