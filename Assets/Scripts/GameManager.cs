using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int index;

    //Control Elements
    public GameObject player;
    public GameObject pauseButton;

    //UI Elements
    private Text timerText, playerSizeText, finalText, bestDistanceText, bestTimeText;
    private GameObject gameoverPanel;
    private Button playagainButton, mainmenuButton;

    //Game Logic Elements
    public bool gameOver;
    public bool paused;
	private bool bestDistance;
    private bool bestTime;
    private float timer;

    public float playerScale;
    public float playerSize;
    public float meltRate;

    //Game Audio
    private bool music;
    public bool sfx;

    public AudioSource audioBG;
    public AudioClip soundBG;

    private static GameManager manager;

    public static GameManager Manager
    {
        get { return manager; }
    }

    void Awake()
    {
        GetThisGameManager();
        soundCheck();
    }

    void GetThisGameManager()
    {
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            manager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        playerScale = 5.0f;
        playerSize = 100f;
        meltRate = 0.05f;

        nullCheck();
        Reset();
        print("START again");
    }


    // Update is called once per frame
    void Update()
    {
        nullCheck();

        //if (music)
        //{
        //    if (!audioBG.isPlaying) { audioBG.PlayOneShot(soundBG); }
        //} else if (!music)
        //{
        //    audioBG.Stop();
        //}

        //Counts down from defined "timer" to reach Game Over.
        if (!gameOver && !paused)
        {
            meltPlayer();
            timer += Time.deltaTime;

            timerText.text = timer.ToString("F2");
            playerSizeText.text = playerSize.ToString("F0") + "%";
            

            if (playerSize <= 0)
            {
                gameFinished();
                print("GAME OVER");
            }
        }
        
    }

    void soundCheck()
    {
        string m = PlayerPrefs.GetString("Music");
        string s = PlayerPrefs.GetString("Sfx");

        if (m == null || m == "on"){ music = true; } else { music = false; }
        if (s == null || s == "on") { sfx = true; } else { sfx = false; }

    }

    public void soundOn(string x)
    {
        if(x == "music") { music = true; }
        if (x == "sfx") { sfx = true; }
    }

    public void soundOff(string x)
    {
        if (x == "music") { music = false; }
        if (x == "sfx") { sfx = false; }
    }

    //MELTING METHODS
    //Logic behind the melting man

    void meltPlayer()
    {
        playerSize -= meltRate;
        print("Player scale = " + playerScale.ToString());
        player.transform.localScale = new Vector3(playerScale *(playerSize / 100), playerScale * (playerSize / 100), playerScale * (playerSize / 100));
    }


    //GAME OVER METHODS
    //Called upon gameover, disable Player/HUD and display game over panel with final score/play again button/main menu button
    void gameFinished()
    {
        gameOver = true;
        paused = true;
        playerSizeText.enabled = false;
        timerText.enabled = false;
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        gameoverPanel.SetActive(true);

        checkHighScore();

        playagainButton.onClick.AddListener(delegate { Reset(); });
        mainmenuButton.onClick.AddListener(delegate { MainMenu(); });

    }

    void checkHighScore()
    {

        if (bestDistance)
        {
            bestDistanceText.text = "NEW HIGHSCORE!";
        }
        else {
			bestDistanceText.text = "High Score: XXX";
        }
        finalText.text = "You survived for XXX seconds!";
    }
    
    //Public bool to check for gameover condition
    public bool isGameOver()
    {
        return gameOver;
    }

    //Resets the game loop upon pressing play again
    public void Reset()
    {
        gameOver = false;
        paused = false;
        bestDistance = false;
        bestTime = false;

        //gameoverPanel.SetActive(false);

        audioBG = gameObject.GetComponent<AudioSource>();
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Returns to main menu upon pressing main menu
    public void MainMenu()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }

    //Checks initilization of UI elements
    void nullCheck()
    {
        print("Nullcheck");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (timerText == null)
        {
            timerText = GameObject.Find("Canvas/TimerText").GetComponent<Text>();
        }
        if (playerSizeText == null)
        {
            playerSizeText = GameObject.Find("Canvas/PlayerSizeText").GetComponent<Text>();
        }
    }

}
