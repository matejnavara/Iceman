using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleLogic : MonoBehaviour {

    GameObject player;
    float puddleSize;
    float evaporateRate;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            puddleSize = 1.0f;
            evaporateRate = 0.005f;
            transform.localScale = new Vector3(player.transform.localScale.x / 10, transform.localScale.y, player.transform.localScale.z / 10);
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        puddleSize -= evaporateRate;
        transform.localScale = new Vector3(transform.localScale.x * puddleSize, transform.localScale.y, transform.localScale.z * puddleSize);

        if(puddleSize <= 0.1f)
        {
            print("Destroying: " + this.name);  
            Destroy(this.gameObject);
        }

    }
}
