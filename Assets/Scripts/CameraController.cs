using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private GameManager gm;
    private Vector3 offset;
    private float zoomLimit;


	// Use this for initialization
	void Start () {
        nullCheck();
        offset = transform.position - player.transform.position;
        zoomLimit = 30.0f;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        nullCheck();
        if(gm.playerSize > zoomLimit)
        {
            transform.position = player.transform.position + (offset * (gm.playerSize / 100));
        }
        else
        {
            transform.position = player.transform.position + (offset * (zoomLimit / 100));
        }
        
	
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
