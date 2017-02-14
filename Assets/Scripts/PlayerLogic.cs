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
        Vector3 sunDir = sun.transform.forward;
        sunDir.Normalize();
        sunDir *= 100;

        int inSun = 0;
        int outSun = 0;

        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {

            if (!Physics.Raycast(child.position, child.position - sunDir, 10))
            {
                Debug.DrawLine(child.position, child.position - sunDir, Color.red);
                inSun++;
            }
            else
            {
                Debug.DrawLine(child.position, child.position - sunDir, Color.green);
                outSun++;
            }
        }
        print("In sun: " + inSun + " VS Out sun: " + outSun);
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
