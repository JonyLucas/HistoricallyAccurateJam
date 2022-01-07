using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource bowShot;

    public AudioSource gunShot;

    public void PlayBowShot()
    {
        bowShot.Play();
    }

    public void PlayGunShot()
    {
        gunShot.Play();
    }
}
