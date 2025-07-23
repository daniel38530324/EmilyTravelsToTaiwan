using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public enum Level8_GameState{
    Explain,
    Game,
    Success,
    Fail
}

public class Level8_GameManager : MonoBehaviour
{
    public static Level8_GameManager Instance{get; private set;}
    public Level8_GameState Level8_GameState{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject success_Image, fail_Image, explain_Image, dialogue_Image, explain_Button, return_Button, exit_Button, sound_Button, good_Image, bad_Image;
    [SerializeField] private TMP_Text score_Text;
    [SerializeField] private GameObject[] bodys, questions, dialogues, options;
    [SerializeField] private string[] answer;
    private BoxCollider2D[] dragObjects;
    private int questionIndex, dialogueIndex, score;

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
        AudioManager.Instance.PlayMusic("Level8Game");
        UpdateLevel8_GameState(Level8_GameState.Explain);
        
    }

    public void UpdateLevel8_GameState(Level8_GameState newState){
        Level8_GameState = newState;

        switch(Level8_GameState){
            case Level8_GameState.Explain:
                StartCoroutine(Explain());
                dragObjects = FindObjectsOfType<BoxCollider2D>();
                SetClickClothes(false);
                break;
            case Level8_GameState.Game:
                explain_Button.SetActive(true);
                return_Button.SetActive(true);
                exit_Button.SetActive(true);
                dialogue_Image.SetActive(true);
                sound_Button.SetActive(true);
                score_Text.transform.parent.gameObject.SetActive(true);

                SetClickClothes(true);

                questions[0].transform.parent.gameObject.SetActive(true);
                options[0].SetActive(true);

                break;
            case Level8_GameState.Success:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Win");
                dialogue_Image.SetActive(false);
                success_Image.SetActive(true);
                break;
            case Level8_GameState.Fail:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Lose");
                dialogue_Image.SetActive(false);
                fail_Image.SetActive(true);
                break;
        }
    }

    public void UpdateLevel8_GameState_Int(int newState){
        UpdateLevel8_GameState((Level8_GameState)newState);
    }

    IEnumerator Explain(){
        yield return new WaitForSeconds(2);
        explain_Image.SetActive(true);
    }

    public void CloseExplain(){
        if(Level8_GameState == Level8_GameState.Explain){
            UpdateLevel8_GameState(Level8_GameState.Game);
        }
    }

    public void SetClickClothes(bool canClick){
        foreach (BoxCollider2D item in dragObjects){
            item.enabled = canClick;
        }
    }

    public void CheckClothes(string commodityName){
        SetClickClothes(false);

        if(commodityName == answer[questionIndex]){
            foreach (GameObject item in bodys)
            {
                if(item.name == commodityName){
                    item.SetActive(true);
                }
                else{
                    item.SetActive(false);
                }
            }
            
            score++;
            score_Text.text = "X" + score;
            if(score >= 5){
                UpdateLevel8_GameState(Level8_GameState.Success);
                return;
            }

            questionIndex++;
            if(questionIndex > 9){
                UpdateLevel8_GameState(Level8_GameState.Fail);
                return;
            }

            good_Image.SetActive(true);
            AudioManager.Instance.PlaySound("Correct");
        }
        else{
            questionIndex++;
            if(questionIndex > 9){
                UpdateLevel8_GameState(Level8_GameState.Fail);
                return;
            }
            bad_Image.SetActive(true);
            AudioManager.Instance.PlaySound("Error");
        }
    }

     public void ChangeQuestion(){
        questions[questionIndex-1].SetActive(false);
        questions[questionIndex].SetActive(true);

        options[questionIndex - 1].SetActive(false);
        options[questionIndex].SetActive(true);

        int previousIndex = dialogueIndex;
        dialogueIndex++;
        dialogueIndex = dialogueIndex > 4 ? 0 : dialogueIndex;
        dialogues[previousIndex].SetActive(false);
        dialogues[dialogueIndex].SetActive(true);

        SetClickClothes(true);
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }
}
