using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    public GameObject sun;
    public Vector3 sunDir;

    private Transform[] allChildren;
    private bool underSun;

    // Use this for initialization
    void Start () {
     
        sun = GameObject.FindGameObjectWithTag("Light");
        sunDir = sun.transform.forward;
        sunDir.Normalize();
    
        allChildren = GetComponentsInChildren<Transform>();
        UnderSun = false;

    }
	
	// Update is called once per frame
	void Update () {

        checkExposure();

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

    private void checkExposure()
    {
        
        int inSun = 0;
        int outSun = 0;

        foreach (Transform child in allChildren)
        {

            if (!Physics.Raycast(child.position, -sunDir))
            {
                Debug.DrawRay(child.position, -sunDir, Color.red);
                inSun++;
            }
            else
            {
                Debug.DrawRay(child.position, -sunDir, Color.green);
                
                outSun++;
            }
        }
        //print("In sun: " + inSun + " VS Out sun: " + outSun);
        if (inSun > outSun)
        {
            UnderSun = true;
        }
        else
        {
            UnderSun = false;
        }
    }


}
