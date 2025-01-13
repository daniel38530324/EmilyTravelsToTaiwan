using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public enum Level2_GameState{
    Explain,
    Game,
    Success,
    Fail
}

public class Level2_GameManager : MonoBehaviour
{
    public static Level2_GameManager Instance{get; private set;}
    public Level2_GameState Level2_GameState{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject success_Image, fail_Image, explain_Image, dialogue_Image, check_Button, submit_Button, explain_Button, return_Button, exit_Button;
    [SerializeField] private TMP_Text beer_Text, milk_Text, cookie_Text, appleJuice_Text, bread_Text, water_Text, laundryDetergent_Text, fail_Text;
    [SerializeField] private GameObject[] bucket_beers, bucket_milks, bucket_cookies, bucket_appleJuices, bucket_breads, bucket_waters, bucket_laundryDetergents;

    private BoxCollider2D[] dragObjects;
    private int beer_amount, milk_amount, cookie_amount, appleJuice_amount, bread_amount, water_amount, laundryDetergent_amount;

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
        AudioManager.Instance.PlayMusic("Level2Game");
        UpdateLevel2_GameState(Level2_GameState.Explain);
    }

    public void UpdateLevel2_GameState(Level2_GameState newState){
        Level2_GameState = newState;

        switch(Level2_GameState){
            case Level2_GameState.Explain:
                StartCoroutine(Explain());

                dragObjects = FindObjectsOfType<BoxCollider2D>();
                foreach (BoxCollider2D item in dragObjects)
                {
                    item.enabled = false;
                }
                break;
            case Level2_GameState.Game:
                explain_Button.SetActive(true);
                return_Button.SetActive(true);
                exit_Button.SetActive(true);
                dialogue_Image.SetActive(true);
                check_Button.SetActive(true);
                submit_Button.SetActive(true);

                foreach (BoxCollider2D item in dragObjects)
                {
                    item.enabled = true;
                }
                break;
            case Level2_GameState.Success:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Win");
                dialogue_Image.SetActive(false);
                success_Image.SetActive(true);
                break;
            case Level2_GameState.Fail:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Lose");
                fail_Text.text = "請正確完成艾蜜莉的購物清單!";
                dialogue_Image.SetActive(false);
                fail_Image.SetActive(true);
                break;
        }
    }

    public void UpdateLevel2_GameState_Int(int newState){
        UpdateLevel2_GameState((Level2_GameState)newState);
    }

    IEnumerator Explain(){
        yield return new WaitForSeconds(2);
        explain_Image.SetActive(true);
    }

    public void CloseExplain(){
        if(Level2_GameState == Level2_GameState.Explain){
            UpdateLevel2_GameState(Level2_GameState.Game);
        }
    }

    public void UpdateCommodity(string commodityName){
        switch(commodityName){
            case "Beer":
                beer_amount++;
                beer_Text.text = $"X {beer_amount}";
                CheckCommodity(commodityName, beer_amount);
                break;
            case "Milk":
                milk_amount++;
                milk_Text.text = $"X {milk_amount}";
                CheckCommodity(commodityName, milk_amount);
                break;
            case "Cookie":
                cookie_amount++;
                cookie_Text.text = $"X {cookie_amount}";
                CheckCommodity(commodityName, cookie_amount);
                break;
            case "AppleJuice":
                appleJuice_amount++;
                appleJuice_Text.text = $"X {appleJuice_amount}";
                CheckCommodity(commodityName, appleJuice_amount);
                break;
            case "Bread":
                bread_amount++;
                bread_Text.text = $"X {bread_amount}";
                CheckCommodity(commodityName, bread_amount);
                break;
            case "Water":
                water_amount++;
                water_Text.text = $"X {water_amount}";
                CheckCommodity(commodityName, water_amount);
                break;
            case "LaundryDetergent":
                laundryDetergent_amount++;
                laundryDetergent_Text.text = $"X{laundryDetergent_amount}";
                CheckCommodity(commodityName, laundryDetergent_amount);
                break;
        }
    }

    private void CheckCommodity(string commodityNmae, int amount){
        switch(commodityNmae){
            case "Beer":
                if(amount > 6){return;}
                bucket_beers[amount - 1].SetActive(true);
                break;
            case "Milk":
                if(amount > 2){return;}
                bucket_milks[amount - 1].SetActive(true);
                break;
            case "Cookie":
                if(amount > 3){return;}
                bucket_cookies[amount - 1].SetActive(true);
                break;
            case "AppleJuice":
                if(amount > 6){return;}
                bucket_appleJuices[amount - 1].SetActive(true);;
                break;
            case "Bread":
                if(amount > 2){return;}
                bucket_breads[amount - 1].SetActive(true);
                break;
            case "Water":
                if(amount > 4){return;}
                bucket_waters[amount - 1].SetActive(true);
                break;
            case "LaundryDetergent":
                if(amount > 2){return;}
                bucket_laundryDetergents[amount - 1].SetActive(true);
                break;
        }
    }

    public void SubmitCommodity(){
        if(beer_amount == 6 && cookie_amount == 3 && bread_amount == 2 && milk_amount == 2 && appleJuice_amount == 6 && water_amount == 0 && laundryDetergent_amount == 0){
            UpdateLevel2_GameState(Level2_GameState.Success);
        }
        else{
            UpdateLevel2_GameState(Level2_GameState.Fail);
        }
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }
}
