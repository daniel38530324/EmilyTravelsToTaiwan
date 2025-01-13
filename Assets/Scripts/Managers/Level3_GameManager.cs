using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public enum Level3_GameState{
    Explain,
    Game,
    Success,
    Fail
}

public class Level3_GameManager : MonoBehaviour
{
    public static Level3_GameManager Instance{get; private set;}
    public Level3_GameState Level3_GameState{get; private set;}
    public bool CanShot{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject success_Image, fail_Image, explain_Image, good_Image, bad_Image, explain_Button, return_Button, exit_Button;
    [SerializeField] private TMP_Text chineseTarget_Text, englishTarget_Text, score_Text, fail_Text;
    [SerializeField] private Balloon[] balloons_Chinese, balloons_English;

    private Balloon firstTarget = null, secondTarget = null;
    private int score, chineseIndex = -1, englishIndex = -1, isShotCount;

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
        AudioManager.Instance.PlayMusic("Level3Game");
        UpdateLevel3_GameState(Level3_GameState.Explain);
    }

    public void UpdateLevel3_GameState(Level3_GameState newState){
        Level3_GameState = newState;

        switch(Level3_GameState){
            case Level3_GameState.Explain:
                chineseTarget_Text.text = String.Empty;
                englishTarget_Text.text = String.Empty;
                StartCoroutine(Explain());
                break;
            case Level3_GameState.Game:
                CanShot = true;
                explain_Button.SetActive(true);
                return_Button.SetActive(true);
                exit_Button.SetActive(true);
                chineseTarget_Text.transform.parent.gameObject.SetActive(true);
                englishTarget_Text.transform.parent.gameObject.SetActive(true);
                score_Text.transform.parent.gameObject.SetActive(true);
                break;
            case Level3_GameState.Success:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Win");
                success_Image.SetActive(true);
                break;
            case Level3_GameState.Fail:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Lose");
                fail_Text.text = "請正確射擊對應的中文與漢語拼音氣球!";
                fail_Image.SetActive(true);
                break;
        }
    }

    public void UpdateLevel3_GameState_Int(int newState){
        UpdateLevel3_GameState((Level3_GameState)newState);
    }

    public void UpdateLevel3_GameState_Later(){
        if(Level3_GameState == Level3_GameState.Explain){
            StartCoroutine(UpdateState_Later()); 
        }
        else{
            SetExplain(false);
        }
    }

    
    IEnumerator UpdateState_Later(){
        yield return new WaitForSeconds(1f);
        UpdateLevel3_GameState(Level3_GameState.Game);
    }

    IEnumerator Explain(){
        yield return new WaitForSeconds(2);
        explain_Image.SetActive(true);
    }

    public void CheckBalloon(Balloon currentBalloon){
        isShotCount++;

        if(firstTarget == null){
            firstTarget = currentBalloon;
            //chineseTarget_Text.text = currentBalloon.name.Substring(3);

            if(firstTarget.IsChinese){
                chineseIndex = firstTarget.Id;
                chineseTarget_Text.text = currentBalloon.name.Substring(3);
            }
            else{
                englishIndex = firstTarget.Id;
                englishTarget_Text.text = currentBalloon.name.Substring(3);
            }
        }
        else{
            secondTarget = currentBalloon;
            //englishTarget_Text.text = currentBalloon.name.Substring(3);

            if(secondTarget.IsChinese){
                chineseIndex = secondTarget.Id;
                chineseTarget_Text.text = currentBalloon.name.Substring(3);
            }
            else{
                englishIndex = secondTarget.Id;
                englishTarget_Text.text = currentBalloon.name.Substring(3);
            }

            if(chineseIndex == englishIndex){
                CheckScore(true);
            }
            else{
                // Debug.Log("chinese" + chineseIndex);
                // Debug.Log("english" + englishIndex);
                // Debug.Log("--");
                CheckScore(false);
            }
        }
    }

    private void CheckScore(bool isAdd){
        CanShot = false;
        firstTarget = null;
        secondTarget = null;
        chineseIndex = -1;
        englishIndex = -1;
        // chineseTarget_Text.text = String.Empty;
        // englishTarget_Text.text = String.Empty;

        if(isAdd){
            score++;
            score_Text.text = $"X {score}";
            Debug.Log("Score" + score);

            if(score >= 10){
                UpdateLevel3_GameState(Level3_GameState.Success);
                return;
            }

            if(isShotCount >= 32){
                UpdateLevel3_GameState(Level3_GameState.Fail);
                return;
            }

            AudioManager.Instance.PlaySound("Correct");
            good_Image.SetActive(true);
        }
        else{
            if(isShotCount >= 32){
                UpdateLevel3_GameState(Level3_GameState.Fail);
                return;
            }

            AudioManager.Instance.PlaySound("Error");
            bad_Image.SetActive(true);
        }

        
    }

    public void ClearTargetText(){
        chineseTarget_Text.text = String.Empty;
        englishTarget_Text.text = String.Empty;
        SetCanShot(true);
    }

    public void SetCanShot(bool can){
        CanShot = can;
    }

    public void SetExplain(bool isOpen){
        StartCoroutine(DelayExplain(isOpen));
    }

    IEnumerator DelayExplain(bool isOpen){
        if(isOpen){
            CanShot = false;
        }
        else{
            yield return new WaitForSeconds(0.9f);
            CanShot = true;
        }
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }
}
