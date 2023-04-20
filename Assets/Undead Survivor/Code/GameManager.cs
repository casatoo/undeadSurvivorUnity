using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("#Game control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("#Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp;
    [Header("#Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    void Awake() 
    {
        instance = this;
    }

    public void GameStart() {
        health = maxHealth;

        // 최초 무기 지급을 위한 임시 스크립트
        uiLevelUp.Select(0);
        Remuse();
    }

    public void GameOver() {
        StartCoroutine(GameOverRutine());
    }

    IEnumerator GameOverRutine() {

        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }
    public void GameVictory() {
        StartCoroutine(GameVictoryRutine());
    }

    IEnumerator GameVictoryRutine() {

        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }


    public void GameRetry() {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if(!isLive)
        return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp () {

        if(!isLive){
            return;
        }
        exp++;
        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop() {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Remuse() {
        isLive = true;
        Time.timeScale = 1;
    }
}
