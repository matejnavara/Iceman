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
    private Image sunIcon;
    private RectTransform tempIcon;
    public GameObject gameoverPanel, gameUi;
    private Button meltAgainButton, mainmenuButton;

    //Game Logic Elements
    public bool gameOver;
    public bool paused;

	private bool bestDistance;
    private bool bestTime;
    private float timer;

    public PlayerLogic pl;
    public SplashLogic sl;
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
        //soundCheck();
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
        playerScale = 7f;
        playerSize = 100f;
        meltRate = 0.03f;

        nullCheck();
        gameoverPanel.SetActive(false);
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
            gameUi.SetActive(true);
            gameoverPanel.SetActive(false);
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
        else
        {
            
            gameUi.SetActive(false);
            gameoverPanel.SetActive(true);
            meltAgainButton.onClick.AddListener(delegate { Reset(); });
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
        if (pl.UnderSun)
        {
            if(tempIcon.rect.height < 400)
            {
                tempIcon.sizeDelta = new Vector2(tempIcon.sizeDelta.x, tempIcon.sizeDelta.y + 0.4f);

            }
            //playerSize -= meltRate * tempIcon.rect.height/100;
            sunIcon.color = Color.yellow;
        }
        else
        {
            if (tempIcon.rect.height > 100)
            {
                tempIcon.sizeDelta = new Vector2(tempIcon.sizeDelta.x, tempIcon.sizeDelta.y - 0.8f);

            }
            //playerSize -= (meltRate * 0.05f) * tempIcon.rect.height / 100;
            sunIcon.color = Color.black;
        }
        playerSize -= meltRate * tempIcon.rect.height / 100;
        player.transform.localScale = new Vector3(playerScale * (playerSize / 100), playerScale * (playerSize / 100), playerScale * (playerSize / 100));

        if((int)playerSize % 5 == 0)
        {
            print("Spawning Puddle. Sploosh!");
            sl.Fire();
            GameObject newPuddle = (GameObject)Instantiate(Resources.Load("Prefabs/Puddle"), player.transform.position, player.transform.rotation);
        }

    }


    //GAME OVER METHODS
    //Called upon gameover, disable Player/HUD and display game over panel with final score/play again button/main menu button
    void gameFinished()
    {
        gameOver = true;
        paused = true;
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        //gameUi.SetActive(false);
        //gameoverPanel.SetActive(true);

        //checkHighScore();

        //meltAgainButton.onClick.AddListener(delegate { Reset(); });
        //mainmenuButton.onClick.AddListener(delegate { MainMenu(); });

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
        gameUi.SetActive(true);
        gameoverPanel.SetActive(false);
        //audioBG = gameObject.GetComponent<AudioSource>();
      
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
        //print("Nullcheck");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            pl = player.GetComponent<PlayerLogic>();
            sl = player.GetComponentInChildren<SplashLogic>();
        }
        if (gameoverPanel == null)
        {
            gameoverPanel = GameObject.Find("Canvas/GameOverPanel");
        }
        if (meltAgainButton == null)
        {
            meltAgainButton = GameObject.Find("Canvas/GameOverPanel/MeltAgainButton").GetComponent<Button>();
        }
        if (gameUi == null)
        {
            gameUi = GameObject.Find("Canvas/GameUI");
        }
        if (timerText == null)
        {
            timerText = GameObject.Find("Canvas/GameUI/TimerText").GetComponent<Text>();
        }
        if (playerSizeText == null)
        {
            playerSizeText = GameObject.Find("Canvas/GameUI/PlayerSizeText").GetComponent<Text>();
        }
        if (sunIcon == null)
        {
            sunIcon = GameObject.Find("Canvas/GameUI/SunImage").GetComponent<Image>();
        }
        if (tempIcon == null)
        {
            tempIcon = GameObject.Find("Canvas/GameUI/Temperature").GetComponent<RectTransform>();
        }
    }

}
