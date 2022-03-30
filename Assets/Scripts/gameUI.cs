using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class gameUI : MonoBehaviour
{
    public playerHealth playerHealthScript;
    public Image[] healthHearts;
    public float playerScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public GameObject gameOverMenu;
    public GameObject retryButtonObject;
    public GameObject startButtonObject;

    private Button startButton;
    private Button retryButton;
    // Start is called before the first frame update
    void Start()
    {
        retryButton = retryButtonObject.GetComponent<Button>();
        startButton = startButtonObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        int playerHealth = playerHealthScript.health;

        if (playerHealth < 4 && playerHealth != -1)
        {
            healthHearts[playerHealth].enabled = false;
        }

        else if (playerHealth == -1)
        {
            healthHearts[0].enabled = false;
        }

        scoreText.text = playerScore.ToString();
        scoreText2.text = playerScore.ToString();

        if (!playerHealthScript.isDead)
        {
            gameOverMenu.SetActive(false);
        }

        else
        {
            gameOverMenu.SetActive(true);
        } 
    } 

    public void NewGame()
    {
        playerScore = 0;
        for (int i = 0; i < healthHearts.Length; i++)
        {
            healthHearts[i].enabled = true;
        }
    }
    
}
