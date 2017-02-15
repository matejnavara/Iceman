using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaLogic : MonoBehaviour {

    private float flux;
    private float waveRate;
    private bool tide;

    void Start()
    {
        flux = 1.0f;
        waveRate = 0.01f;
        tide = false;
    }

	void Update () {

        if(tide)
        {
            transform.Translate(0, 0, waveRate);
            flux += waveRate;
            if (flux > 1)
            {
                tide = false;
            }
        }
        else
        {
            transform.Translate(0, 0, -waveRate);
            flux -= waveRate;
            if (flux < 0)
            {
                tide = true;
            }
        }
        		
	}
}
