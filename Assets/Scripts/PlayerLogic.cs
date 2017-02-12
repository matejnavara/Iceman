using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    public GameObject sun;

    private RaycastHit hit;
    private bool underSun;
    private LayerMask shelter;


	// Use this for initialization
	void Start () {
     
        sun = GameObject.FindGameObjectWithTag("Light");
        underSun = false;
        shelter = 8;



    }
	
	// Update is called once per frame
	void Update () {

        underSun = false;
        Vector3 sunDir = sun.transform.forward;
        sunDir.Normalize();
        sunDir *= 100;

        if (!Physics.Raycast(transform.position, transform.position - sunDir, 30, shelter.value))
        {

            Debug.DrawLine(transform.position, transform.position - sunDir, Color.red);
            underSun = true;

        }
        else
        {
            Debug.DrawLine(transform.position, transform.position - sunDir, Color.green);
        }

    }
}
