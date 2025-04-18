using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Level5_GameState{
    Explain,
    Question,
    Tea,
    Success,
    Fail
}

public class Level5_GameManager : MonoBehaviour
{
    public static Level5_GameManager Instance{get; private set;}
    public Level5_GameState Level5_GameState{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject success_Image, fail_Image, explain_Image, dialogue_Image, score_Image, explain_Button, return_Button, exit_Button, good, bad;
    [SerializeField] private SpriteRenderer expression;
    [SerializeField] private Animator teaAnimator;
    [SerializeField] private GameObject[] questions, options;
    [SerializeField] private Sprite[] expressionSprites;
    [SerializeField] private TMP_Text fail_Text, dialog_Text;
    [SerializeField] private Material teaFlow, teaPool;

    private int questionIndex, expressionIndex, score, currentTeaNum;
    private string[] dialog = {"我想喝綠茶", "我想喝紅茶", "我想喝烏龍茶", "我想喝黑茶"}; 
    private Color[] teaFlowColor = {new Color(1, 0.9230983f, 0, 1), new Color(1, 0.5049599f, 0.004716992f, 1), new Color(1, 0.8197486f, 0, 1), new Color(0.7264151f, 0.2005436f, 0, 1)};

    private bool isCorrect;

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
        AudioManager.Instance.PlayMusic("Level5Game");
        UpdateLevel5_GameState(Level5_GameState.Explain);
    }

    public void UpdateLevel5_GameState(Level5_GameState newState){
        Level5_GameState = newState;

        switch(Level5_GameState){
            case Level5_GameState.Explain:
                StartCoroutine(Explain());
                break;
            case Level5_GameState.Question:
                explain_Button.SetActive(true);
                return_Button.SetActive(true);
                exit_Button.SetActive(true);
                score_Image.SetActive(true);
                if(questionIndex > 0){
                    questions[questionIndex-1].SetActive(false);
                }
                questions[questionIndex].SetActive(true);
                expressionIndex = 0;
                expression.sprite = expressionSprites[expressionIndex];
                good.SetActive(false);
                bad.SetActive(false);
                currentTeaNum = Random.Range(0, 4);
                dialog_Text.text = dialog[currentTeaNum];
                dialogue_Image.SetActive(true);
                break;
            case Level5_GameState.Tea:
                questions[questionIndex].SetActive(false);
                dialogue_Image.SetActive(false);
                explain_Button.SetActive(false);
                return_Button.SetActive(false);
                exit_Button.SetActive(false);
                score_Image.SetActive(false);
 
                expression.sprite = expressionSprites[expressionIndex];
                break;
            case Level5_GameState.Success:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Win");
                dialogue_Image.SetActive(false);
                success_Image.SetActive(true);
                break;
            case Level5_GameState.Fail:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Lose");
                fail_Text.text = "請正確倒給艾蜜莉想喝的茶!";
                dialogue_Image.SetActive(false);
                fail_Image.SetActive(true);
                break;
        }
    }

    public void UpdateLevel5_GameState_Int(int newState){
        UpdateLevel5_GameState((Level5_GameState)newState);
    }

    public void CheckQuestion(bool isCorrect){
        this.isCorrect = isCorrect;
        if(isCorrect){
            expressionIndex = 1;
            score++;
            score_Image.transform.GetChild(0).GetComponent<TMP_Text>().text = "X" + score;
            teaFlow.color = teaFlowColor[currentTeaNum];
            teaPool.color = teaFlowColor[currentTeaNum];
        }
        else{
            expressionIndex = 2;
            while(true){
                int failTeaNum = Random.Range(0, 4);
                if(currentTeaNum == failTeaNum){
                    continue;
                }else{
                    currentTeaNum = failTeaNum;
                    break;
                }
            }
            teaFlow.color = teaFlowColor[currentTeaNum];
            teaPool.color = teaFlowColor[currentTeaNum];
        }
        UpdateLevel5_GameState(Level5_GameState.Tea);
    }

    IEnumerator Explain(){
        yield return new WaitForSeconds(2);
        explain_Image.SetActive(true);
    }

    public void CloseExplain(){
        if(Level5_GameState == Level5_GameState.Explain){
            UpdateLevel5_GameState(Level5_GameState.Question);
        }
    }

    public void SetTeaAnimation(int index){
        //555
        //questions[questionIndex].SetActive(false);
        teaAnimator.SetInteger("Tea", index);
    }

    public void TeaAnimation(bool isStart){
        Button[] buttons = questions[questionIndex].transform.GetComponentsInChildren<Button>();
        TMP_Text[] texts = questions[questionIndex].transform.GetComponentsInChildren<TMP_Text>();

        if(isStart){
            for(int i = 0; i < buttons.Length; i++){
                buttons[i].enabled = false;
                texts[i].enabled = false;
            }
        }
        else{
            for(int i = 0; i < buttons.Length; i++){
                buttons[i].enabled = true;
                texts[i].enabled = true;
            }
            teaAnimator.SetInteger("Tea", 0);
            questionIndex++;

            if(score >= 5){
                UpdateLevel5_GameState(Level5_GameState.Success);
                return;
            }
            if(questionIndex > 9){
                UpdateLevel5_GameState(Level5_GameState.Fail);
                return;
            }
            else{
                UpdateLevel5_GameState(Level5_GameState.Question);
            }
        }
    }

    public void HintAnimation(){
        if(isCorrect){
            good.SetActive(true);
            AudioManager.Instance.PlaySound("Correct");
        }
        else{
            bad.SetActive(true);
            AudioManager.Instance.PlaySound("Error");
        }
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }
}
