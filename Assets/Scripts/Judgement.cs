using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Judgement : MonoBehaviour
{
    public static bool isStart;
    [SerializeField] private Text gameText;
    [SerializeField] private GameObject playerTempField;
    [SerializeField] private Text playerScore;
    [SerializeField] private GameObject enemyTempField;
    [SerializeField] private Text enemyScore;
    [SerializeField] private GameObject motherNeutral;
    [SerializeField] private Text gameTimer;

    private int pScore, eScore;
    private int neutralSize;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 60f;
        playerScore.text = "Player's Score";
        enemyScore.text = "Enemy's Score";
        StartCoroutine("GameStart");
        neutralSize = motherNeutral.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0 && isStart)
        {
            ScoreCounter();
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            if (isStart) Judge();
            isStart = false;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }

        var second = (int) timer;
        gameTimer.text = second.ToString();
    }

    private void ScoreCounter()
    {
        int player = 0, enemy = 0;
        for (int i = 0; i < neutralSize; i++)
        {
            var neutralController = motherNeutral.transform.GetChild(i).GetComponent<NeutralController>();
            if (neutralController.currentIndex == 2) player++;
            else if (neutralController.currentIndex == 0) enemy++;
        }
        
        pScore = player;
        eScore = enemy;
        
        playerScore.text = "Player: " + player.ToString();
        enemyScore.text = "Enemy: " + enemy.ToString();
        
    }

    private void Judge()
    {
        if (pScore > eScore)
        {
            gameText.text = "You Win! Press R to Restart!";
        }
        else if (pScore < eScore)
        {
            gameText.text = "You Lose! Press R to Restart!";
        }
        else
        {
            gameText.text = "Draw! Press R to Restart!";
        }
    }
    
    private IEnumerator GameStart()
    {
        gameText.text = "READY?";
        yield return new WaitForSeconds(1);
        gameText.text = "3";
        yield return new WaitForSeconds(1);
        gameText.text = "2";
        yield return new WaitForSeconds(1);
        gameText.text = "1";
        yield return new WaitForSeconds(1);
        gameText.text = "GO!";
        isStart = true;
        playerTempField.SetActive(false);
        enemyTempField.SetActive(false);
        yield return new WaitForSeconds(1);
        gameText.text = "";
    }
}
