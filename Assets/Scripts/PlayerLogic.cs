using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    public GameObject sun;

    private bool underSun;

    // Use this for initialization
    void Start () {
     
        sun = GameObject.FindGameObjectWithTag("Light");
        UnderSun = false;

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 sunDir = sun.transform.forward;
        sunDir.Normalize();
        sunDir *= 100;

        if (!Physics.Raycast(transform.position, transform.position - sunDir, 30))
        {
            Debug.DrawLine(transform.position, transform.position - sunDir, Color.red);
            UnderSun = true;

        }
        else
        {
            Debug.DrawLine(transform.position, transform.position - sunDir, Color.green);
            UnderSun = false;
        }

    }

    public bool UnderSun
    {
        get
        {
            return underSun;
        }

        set
        {
            underSun = value;
        }
    }


}
