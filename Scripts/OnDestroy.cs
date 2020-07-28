using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyFruits : MonoBehaviour {
    // Start is called before the first frame update
    public ParticleSystem deathEffect;

    void OnDestroy () {
        deathEffect.Play ();

    }
}