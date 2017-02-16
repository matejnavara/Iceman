using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Control Elements
    public GameObject player;
    public GameObject camera;

    //UI Elements
    private Text distanceText, playerSizeText, finalText, bestDistanceText, bestTimeText;
    private Image sunIcon;
    private RectTransform tempIcon;
    public GameObject gameoverPanel, gameUi;
    private Button meltAgainButton, mainmenuButton;

    //Game Logic Elements
    public bool gameOver;
	private bool bestDistance;
    private int distance;
    private float timer;

    private GameObject respawn;
    private Vector3 cameraRespawn;
    private Vector2 tempRespawn;

    public PlayerLogic pl;
    public SplashLogic sl;
    public Renderer playerMat;
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
        nullCheck();
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
        playerSize = 100f;
        playerScale = 15f;
        meltRate = 0.02f;

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
        if (!gameOver)
        {
            gameUi.SetActive(true);
            gameoverPanel.SetActive(false);


            meltPlayer();

            timer += Time.deltaTime;

            //timerText.text = timer.ToString("F2");
            distanceText.text = checkDistance().ToString() + "m";
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
            if(tempIcon.rect.height < 200)
            {
                tempIcon.sizeDelta = new Vector2(tempIcon.sizeDelta.x, tempIcon.sizeDelta.y + 0.3f);

            }
            //playerSize -= meltRate * tempIcon.rect.height/100;
            sl.PlayEffects();
            playerMat.sharedMaterial.SetFloat("_ReflectBrightness", 1.0f);
            GameObject newPuddle = (GameObject)Instantiate(Resources.Load("Prefabs/Puddle"), player.transform.position, player.transform.rotation);
            sunIcon.color = Color.yellow;
        }
        else
        {
            if (tempIcon.rect.height > 50)
            {
                tempIcon.sizeDelta = new Vector2(tempIcon.sizeDelta.x, tempIcon.sizeDelta.y - 2.0f);

            }
            //playerSize -= (meltRate * 0.05f) * tempIcon.rect.height / 100;
            sl.StopEffects();
            playerMat.sharedMaterial.SetFloat("_ReflectBrightness", 0.0f);
            sunIcon.color = Color.black;
        }
        playerSize -= meltRate * tempIcon.rect.height / 100;
        player.transform.localScale = new Vector3(playerScale * (playerSize / 100), playerScale * (playerSize / 100), playerScale * (playerSize / 100));

    }

    int checkDistance()
    {
        distance = (int)(Vector3.Distance(respawn.transform.position, player.transform.position) * 3);

        return distance;
    }


    //GAME OVER METHODS
    //Called upon gameover, disable Player/HUD and display game over panel with final score/play again button/main menu button
    void gameFinished()
    {
        gameOver = true;
        checkHighScore();
        //GameObject.FindGameObjectWithTag("Player").SetActive(false);

    }

    void checkHighScore()
    {
        //Add logic for final game over screen here.
        if (PlayerPrefs.HasKey("bestDistance"))
        {
            if(distance > PlayerPrefs.GetInt("bestDistance"))
            {
                bestDistance = true;
            }
            else
            {
                bestDistance = false;
            }
        }
        else
        {
            bestDistance = true;
        }

        if (bestDistance)
        {
            PlayerPrefs.SetInt("bestDistance", distance);
            bestDistanceText.text = "NEW HIGHSCORE!";
        }
        else {
            bestDistanceText.text = "High Score: " +  PlayerPrefs.GetInt("bestDistance");
        }
        finalText.text = "Alas Iceman is no more. He travelled " + distance + " metres before melting.";
    }
    
    //Public bool to check for gameover condition
    public bool isGameOver()
    {
        return gameOver;
    }

    //Resets the game loop upon pressing play again
    public void Reset()
    {

        player.transform.position = new Vector3(0,0,0);
        player.transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        playerSize = 100f;
        camera.transform.position = cameraRespawn;
        tempIcon.sizeDelta = tempRespawn;
        gameOver = false;
        bestDistance = false;

        //audioBG = gameObject.GetComponent<AudioSource>();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            playerMat = player.GetComponentInChildren<Renderer>();
        }
        if(camera == null)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            cameraRespawn = camera.transform.position;
        }
        if (respawn == null)
        {
            respawn = GameObject.FindGameObjectWithTag("Respawn");
        }
        if (gameoverPanel == null)
        {
            gameoverPanel = GameObject.Find("Canvas/GameOverPanel");
        }
        if (finalText == null)
        {
            finalText = GameObject.Find("Canvas/GameOverPanel/FinalText").GetComponent<Text>();
        }
        if (bestDistanceText == null)
        {
            bestDistanceText = GameObject.Find("Canvas/GameOverPanel/BestDistanceText").GetComponent<Text>();
        }
        if (meltAgainButton == null)
        {
            meltAgainButton = GameObject.Find("Canvas/GameOverPanel/MeltAgainButton").GetComponent<Button>();
        }
        if (gameUi == null)
        {
            gameUi = GameObject.Find("Canvas/GameUI");
        }
        if (distanceText == null)
        {
            distanceText = GameObject.Find("Canvas/GameUI/DistanceText").GetComponent<Text>();
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
            tempRespawn = tempIcon.sizeDelta;
        }
    }

}
