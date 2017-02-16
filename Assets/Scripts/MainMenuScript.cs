using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    private GameObject highscore;

	// Use this for initialization
	void Start () {
        highscore = GameObject.Find("Canvas/highscore");
        checkHighscore();
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("bestDistance");
        checkHighscore();
    }

    void checkHighscore()
    {
        if (PlayerPrefs.HasKey("bestDistance"))
        {
            highscore.GetComponent<Text>().text = "Best distance: " + PlayerPrefs.GetInt("bestDistance") + "m";
        }
        else
        {
            highscore.SetActive(false);
        }
    }
}
