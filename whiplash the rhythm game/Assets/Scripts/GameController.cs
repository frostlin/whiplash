using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region variables
    public static GameController instance;

    public AudioSource music;
    public NoteScroller scroller;
    public bool startPlaying;

    public Sprite perfect, good, meh, miss;

    public GameObject hitEffect;
    public GameObject resultScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public Transform healthBar;
    public Text currentScoreText, currentComboText, currentAccText, accuracyText, badText, 
        goodText, perfectText, missText, rankText, scoreText, comboText, countdownText;

    private float currentHealth;
    private float maxHealth;
    private float currentAcc;
    private int currentScore;
    private int currentCombo;
    private int highestCombo;

    private int countAll;
    private int countPerfect;
    private int countGood;
    private int countBad;
    private int countMiss;

    private float countdownTimer = 1.5f;
    bool countdownStarted;
    public bool gamePaused;
    #endregion


    void Start()
    { 
        instance = this;
        maxHealth = 20;
        currentHealth = maxHealth;
        currentAcc = 100.0f;
        currentScoreText.text = "Score: 0";
        highestCombo = 0;
        //countAll = GameObject.FindGameObjectsWithTag("note").Length;
    }
    void Update()
    {
        if (currentHealth > 0 && !gamePaused)
        {
            currentHealth -= Time.deltaTime;
            healthBar.localScale = new Vector3(healthBar.localScale.x, currentHealth / maxHealth * 4, 1);
        } 
        if (currentHealth <= 0)
        {
            gameOver();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameRetry();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseScreen.activeInHierarchy)
            {
                gamePause();
            }
            else
            {
                gameResume();
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if ((collision.tag.Equals("note") || collision.tag.Equals("slider")) && !startPlaying)
        {
            music.Play();
            startPlaying = true;
        }
        if (collision.tag.Equals("Finish"))
        {
            gameFinished();
        }
        }
    public void noteHit(float magnitude, Vector3 position)
    {
        int points;
        float healthInc;
        if (magnitude <= 0.3)
        {
            ++countPerfect;
            points = 300;
            healthInc = 0.6f;
            EffectController.instance.changeSprite(perfect);
        }
        else if (magnitude > 0.3 && magnitude <= 0.5)
        {
            ++countGood;
            points = 100;
            healthInc = 0.4f;
            EffectController.instance.changeSprite(good);
        }
        else
        {
            ++countBad;
            points = 50;
            healthInc = 0.2f;
            EffectController.instance.changeSprite(meh);
        }
        currentScore += points * currentCombo;

        if (currentHealth <= maxHealth) 
            currentHealth += healthInc;
        ++currentCombo;
        ++countAll;

        updateTextFields();
        Instantiate(hitEffect, position, Quaternion.identity);
    }
    public void noteMissed()
    {
        ++countMiss;
        ++countAll;

        if (currentCombo > highestCombo)
            highestCombo = currentCombo;
        currentCombo = 0;

        updateTextFields();
        GetComponent<AudioSource>().Play();
        EffectController.instance.changeSprite(miss);

        if (currentHealth > 0)
            currentHealth -= 1;
        else
            gameOver();
    }

    private void updateTextFields()
    {
        currentAcc = ((countPerfect + countGood * 0.85f + countBad * 0.65f) / countAll) * 100.0f;

        //healthBar.localScale = new Vector3(healthBar.localScale.x, currentHealth / maxHealth * 4, 1);

        currentScoreText.text = currentScore.ToString();
        currentComboText.text = currentCombo.ToString();
        currentAccText.text = currentAcc.ToString("F2") + "%";  
    }

    public void gamePause()
    {

        pauseScreen.SetActive(true);
        scroller.hasStarted = false;
        music.Pause();
        gamePaused = true;
    }
    public void gameResume()
    {
        pauseScreen.SetActive(false);
        scroller.hasStarted = true;
        music.Play();
        gamePaused = false;
    }
    public void gameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        scroller.hasStarted = false;
        music.Stop();
        gamePaused = true;
    }
    public void gameRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void gameFinished()
    {
        gamePaused = true;
        if (currentCombo > highestCombo)
            highestCombo = currentCombo;

        resultScreen.SetActive(true);

        badText.text = countBad.ToString();
        goodText.text = countGood.ToString();
        perfectText.text = countPerfect.ToString();
        missText.text = countMiss.ToString();

        comboText.text = highestCombo.ToString();
        scoreText.text = currentScore.ToString();
        accuracyText.text = currentAcc.ToString("F2") + "%";
        if (currentAcc == 100) rankText.text = "SS";
        else if (currentAcc >= 95) rankText.text = "S";
        else if (currentAcc >= 90 && currentAcc < 95) rankText.text = "A";
        else if (currentAcc >= 85 && currentAcc < 90) rankText.text = "B";
        else if (currentAcc >= 70 && currentAcc < 85) rankText.text = "C";
        else rankText.text = "D";
    }


}
