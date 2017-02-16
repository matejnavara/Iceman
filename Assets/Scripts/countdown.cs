using UnityEngine;
using System.Collections;

public class countdown : MonoBehaviour {

    private GameManager gm;

    public void setCountdownDone()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        print("COUNTDOWN DONE!");
        gm.countdownComplete();
    }
}
