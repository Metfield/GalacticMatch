using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    bool gameHasStarted;

    [SerializeField]
    float gameTime = 60;

    // Canvases
    Canvas hudCanvas,
           mainMenuCanvas,
           gameOverCanvas;

    Text timeLeftText;

    Text scoreText;
    uint score;
    uint scoringMultiplier;

    MatchTracer matchTracer;

    bool gameOverActive;

    // Use this for initialization
    void Start()
    {
        // Create background skysphere
        Instantiate(Resources.Load<GameObject>("Prefabs/Skysphere"));

        // Get canvases
        hudCanvas = GameObject.Find("HUD").GetComponent<Canvas>();
        mainMenuCanvas = GameObject.Find("MainMenu").GetComponent<Canvas>();
        gameOverCanvas = GameObject.Find("GameOver").GetComponent<Canvas>();

        // Get reference to time left gui and score HUD objects
        GetHUDTextObjects();
        timeLeftText.text = gameTime.ToString("0.00");

        // Hide HUD and GameOver at start of game
        hudCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);

        // Arbitrary multiplier
        scoringMultiplier = 5;
    }

    public void OnGameStart()
    {
        // Gotta protect it like this because apparently the button callback
        // happens twice...
        if (!gameHasStarted)
        {
            gameHasStarted = true;

            // Hide mainmenu
            mainMenuCanvas.gameObject.SetActive(false);

            // Turn on HUD
            hudCanvas.gameObject.SetActive(true);

            // Create board in scene
            Instantiate(Resources.Load<GameObject>("Prefabs/Board"));

            // Create the helper that traces tile matching
            matchTracer = Instantiate(Resources.Load<GameObject>("Prefabs/MatchTracer")).GetComponent<MatchTracer>();
            matchTracer.Init(this);
        }
    }

    public void UpdateScore(uint combo)
    {
        // Basic formula
        score += combo * (combo + scoringMultiplier);
        scoreText.text = "Score   " + score;
    }

    // Display final score and give option to restart game
    void OnGameOver()
    {
        gameHasStarted = false;

        // Turn off matchtracer
        matchTracer.enabled = false;

        // Toggle canvases
        hudCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);

        // Control flag for input
        gameOverActive = true;
    }

    void GetHUDTextObjects()
    {
        Text[] textChildren = hudCanvas.GetComponentsInChildren<Text>();

        foreach (Text guiText in textChildren)
        {
            if (guiText.name == "TimeLeft")
            {
                timeLeftText = guiText;
            }
            else if(guiText.name == "Score")
            {
                scoreText = guiText;
                scoreText.text = "Score   0";
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (gameHasStarted)
        {
            // Update time
            gameTime -= Time.deltaTime;

            if (gameTime <= 0)
                OnGameOver();

            timeLeftText.text = gameTime.ToString("0.00");

            if (gameTime < 10)
                timeLeftText.color = new Color(0.9f, 0.4f, 0.2f);
        }
        else if(gameOverActive)
        {
            if(Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadSceneAsync("Game");
            }
        }
	}
}
