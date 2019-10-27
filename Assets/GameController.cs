using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject hearthOne;
    public GameObject hearthTwo;
    public GameObject hearthThree;
    public TextMeshProUGUI scoreText;
    public GameObject player;
    private bool game_over = false;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    public static int health;
    public static int score;
    public static int gun_lvl = 1;
    public static int gun_power = 1;
    public static int enemiesKilled = 0;
    public static int collectedPowerUps = 0;
    public static int survivedSeconds = 0;
    public float timer;
    public static bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = 3;
        this.hearthOne.SetActive(true);
        this.hearthTwo.SetActive(true);
        this.hearthThree.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
        this.gameOverText.alpha = 0;
        this.restartText.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)

        {

            score += 1;
            survivedSeconds += 1 ;
            if (survivedSeconds == 30) {
                achivementController.OutlivedThirtySeconds = true;
            }

            //We only need to update the text if the score changed.
            

            //Reset the timer to 0.
            timer = 0;
        }
        if (!game_over) {
            scoreText.text = "Score: " + score.ToString();
        }
        




        if (health > 3) {
            health = 3;
        }

        if (!game_over)
        {
            switch (health)
            {
                case 3:
                    hearthOne.SetActive(true);
                    hearthTwo.SetActive(true);
                    hearthThree.SetActive(true);
                    break;
                case 2:
                    hearthOne.SetActive(true);
                    hearthTwo.SetActive(true);
                    hearthThree.SetActive(false);
                    break;
                case 1:
                    hearthOne.SetActive(true);
                    hearthTwo.SetActive(false);
                    hearthThree.SetActive(false);
                    break;
                case 0:
                    hearthOne.SetActive(false);
                    hearthTwo.SetActive(false);
                    hearthThree.SetActive(false);
                    player.GetComponent<Animator>().Play("Destroy");
                    this.game_over = true;
                    Invoke("toggleRestartText", 0.5f);
                    Invoke("Die", 0.7f);

                    break;
            }
        }
        else {
            if(Input.GetKeyDown(KeyCode.Space)) {
                CancelInvoke("toggleRestartText");
                this.restartText.alpha = 0;
                isDead = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //Time.timeScale = 1;
                
            }
            if (Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }
        }
    }

    void Die()
    {
        isDead = true;
        Animator anim = player.GetComponent<Animator>();
        ParticleSystem gas1 = player.transform.Find("gas1").GetComponent<ParticleSystem>();
        ParticleSystem gas2 = player.transform.Find("gas2").GetComponent<ParticleSystem>();
        anim.Play("Destroy");
        gas1.Stop();
        gas2.Stop();
        this.gameOverText.alpha = 1;
        this.restartText.alpha = 1;
        enemiesKilled = 0;
        collectedPowerUps = 0;
        survivedSeconds = 0;
        gun_lvl = 1;
        gun_power = 1;
        
    }

    void toggleRestartText() {
        if (this.restartText.alpha == 1)
        {
            this.restartText.alpha = 0;
            Invoke("toggleRestartText", 0.5f);
        }
        else {
            this.restartText.alpha = 1;
            Invoke("toggleRestartText", 0.5f);
        }
    }
}
