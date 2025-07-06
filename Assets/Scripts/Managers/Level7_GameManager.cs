using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum Level7_GameState{
    Explain,
    Game,
    Success,
    Fail
}

public class Level7_GameManager : MonoBehaviour
{
    public static Level7_GameManager Instance{get; private set;}
    public Level7_GameState Level7_GameState{get; private set;}
    public bool PlayerCanMove{get; set;}

    //[field: SerializeField] public

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject success_Image, fail_Image, explain_Image, explain_Button, return_Button, exit_Button, good_Image, bad_Image, health_Image, ball, bomb, bombEffect, smile;
    [SerializeField] private GameObject[] questions, answers, spawnPoints;
    [SerializeField] private Sprite[] ballTextures;
    [SerializeField] private SpriteRenderer[] bodyTextures;
    [SerializeField] private string[] questionStrings;

    private int questionIndex, health = 5;
    private float timer;
    Ball[] balls;


    //[Header("Setting")]


    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }

        if(GameManager.Instance == null){
            Instantiate(gameManager);
        }
    }

    private void Start() {
        AudioManager.Instance.PlayMusic("Level7Game");
        UpdateLevel7_GameState(Level7_GameState.Explain);
    }

    private void Update() {
        if(Level7_GameState == Level7_GameState.Game){
            timer += Time.deltaTime;
            if(timer >= 1.5f){
                timer = 0;

                int ballIndex = UnityEngine.Random.Range(0, 4);
                GameObject target;
                if(ballIndex != 3){
                    target = ball;
                    int ballColorIndex = UnityEngine.Random.Range(0, 9);
                    target.GetComponentInChildren<SpriteRenderer>().sprite = ballTextures[ballColorIndex];

                    int ballStringIndex = UnityEngine.Random.Range(0, 8);
                    target.GetComponentInChildren<TMP_Text>().text = questionStrings[ballStringIndex];
                }
                else{
                    target = bomb;
                }
                int ballSpawnIndex = UnityEngine.Random.Range(0, 9);
                Instantiate(target, spawnPoints[ballSpawnIndex].transform.position, Quaternion.identity);
            }
        }
    }

    public void UpdateLevel7_GameState(Level7_GameState newState){
        Level7_GameState = newState;

        switch(Level7_GameState){
            case Level7_GameState.Explain:
                  StartCoroutine(Explain());
                  PlayerCanMove = false;
                break;
            case Level7_GameState.Game:
                explain_Button.SetActive(true);
                return_Button.SetActive(true);
                exit_Button.SetActive(true);
                health_Image.SetActive(true);
                PlayerCanMove = true;
                questions[0].transform.parent.gameObject.SetActive(true);
                break;
            case Level7_GameState.Success:
                PlayerCanMove = false;
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Win");
                questions[0].transform.parent.gameObject.SetActive(false);
                success_Image.SetActive(true);

                balls = FindObjectsOfType<Ball>();
                foreach (Ball item in balls)
                {
                    Destroy(item.gameObject);
                }
                break;
            case Level7_GameState.Fail:
                PlayerCanMove = false;
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Lose");
                questions[0].transform.parent.gameObject.SetActive(false);
                fail_Image.SetActive(true);

                balls = FindObjectsOfType<Ball>();
                foreach (Ball item in balls)
                {
                    Destroy(item.gameObject);
                }

                foreach (SpriteRenderer item in bodyTextures)
                {
                    item.color = new Color(0.3018868f, 0.3018868f, 0.3018868f);
                }
                smile.SetActive(true);
                break;
        }
    }
    public void UpdateLevel7_GameState_Int(int newState){
        UpdateLevel7_GameState((Level7_GameState)newState);
    }

    public void CheckBall(Ball target){
        if(target.isBall){
            if(target.GetComponentInChildren<TMP_Text>().text == questionStrings[questionIndex]){
                questions[questionIndex].SetActive(false);
                answers[questionIndex].SetActive(true);
                questionIndex++;

                if(questionIndex >= 5){
                    UpdateLevel7_GameState(Level7_GameState.Success);
                    return;
                }
                good_Image.SetActive(true);
                AudioManager.Instance.PlaySound("Correct");
            }
        }
        else{
            //bad_Image.SetActive(true);
            AudioManager.Instance.PlaySound("Bomb");
            Instantiate(bombEffect, target.transform.position, Quaternion.identity);
            StartCoroutine(Injured());
            health--;
            health_Image.GetComponentInChildren<TMP_Text>().text = "X " + health;
            if(health <= 0){
                UpdateLevel7_GameState(Level7_GameState.Fail);
            }
        }

    }

    IEnumerator Injured(){
        foreach (SpriteRenderer item in bodyTextures)
        {
            item.color = new Color(0.3018868f, 0.3018868f, 0.3018868f);
        }
        smile.SetActive(true);

        yield return new WaitForSeconds(2);

        if (Level7_GameState != Level7_GameState.Fail){
            foreach (SpriteRenderer item in bodyTextures)
            {
                item.color = new Color(1, 1, 1);
            }
            smile.SetActive(false);
        }
    }

    public void ChangeQuestion(){
        answers[questionIndex-1].SetActive(false);
        questions[questionIndex].SetActive(true);
    }


    IEnumerator Explain(){
        yield return new WaitForSeconds(2);
        explain_Image.SetActive(true);
    }

    public void CloseExplain(){
        if(Level7_GameState == Level7_GameState.Explain){
            UpdateLevel7_GameState(Level7_GameState.Game);
        }
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }
}
