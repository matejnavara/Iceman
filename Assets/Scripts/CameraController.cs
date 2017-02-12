﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private GameManager gm;
    private Vector3 offset;


	// Use this for initialization
	void Start () {
        nullCheck();
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        nullCheck();
        transform.position = player.transform.position + (offset * (gm.playerSize/100));
	
	}

    void nullCheck()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

}