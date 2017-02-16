using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLogic : MonoBehaviour {

    ParticleSystem[] effects;

	// Use this for initialization
	void Start () {

        effects = gameObject.GetComponentsInChildren<ParticleSystem>();
		
	}
	
	public void PlayEffects()
    {
        foreach(ParticleSystem effect in effects)
        {
            if (!effect.isPlaying)
            {
                effect.Play();
            }
        }
    }

    public void StopEffects()
    {
        foreach (ParticleSystem effect in effects)
        {
            if (effect.isPlaying)
            {
                effect.Stop();
            }
        }
    }
}
