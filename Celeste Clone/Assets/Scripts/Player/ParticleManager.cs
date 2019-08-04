using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem[] Effects;
    public void playEffect () {
        foreach (ParticleSystem item in Effects)
        {
            item.Play();
        }
    }
}
