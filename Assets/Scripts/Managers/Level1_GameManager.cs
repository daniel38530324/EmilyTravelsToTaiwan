using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum Level1_GameState{
    Explain,
    Game,
    Question1,
    Question2,
    Question3,
    Question4,
    Question5,
    Question6,
    Question7,
    Success,
    Fail
}

public class Level1_GameManager : MonoBehaviour
{
    public static Level1_GameManager Instance{get; private set;}
    public Level1_GameState Level1_GameState{get; private set;}

    [Header("References")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player, leftWall;
    [SerializeField] private MeshRenderer background;
    [SerializeField] private Image poewr_Image, DialogueBg_Image;
    [SerializeField] private Transform playerRespawnPos;
    [SerializeField] private GameObject success_Image, fail_Image, explain_Image, health_Image, power_Image, explain_Button, return_Button, exit_Button, good_Image, bad_Image;
    [SerializeField] private TMP_Text health_Text, fail_Text;
    [SerializeField] private GameObject[] questions, batterys, cars;
    [SerializeField] private Transform[] batterySpawnPos;
    [SerializeField] private Transform[] carSpawnPos;
    [SerializeField] private Sprite[] powerSprites;

    [Header("Setting")]
    [SerializeField] private float backgroundSpeed;

    private Material[] backgroundMat;
    private float backgroundMovement;
    private int power = 0, health = 5;
    private float gameTimer = 0, carSpawnTimer = 0;
    int previousCarNum = -1, previousSpawnPosNum = -1;

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
        AudioManager.Instance.PlayMusic("Level1Game");
        backgroundMat = background.materials;
        UpdateLevel1_GameState(Level1_GameState.Explain);
    }

    private void Update() {
        backgroundMovement += backgroundSpeed * Time.deltaTime;
        backgroundMat[0].SetTextureOffset("_BaseMap", new Vector2(backgroundMovement, 0));
        //float OffsetX = backgroundMat[0].GetTextureOffset("_BaseMap").x;
        //backgroundMat[0].SetTextureOffset("_BaseMap", new Vector2(OffsetX + backgroundSpeed * Time.deltaTime, 0));

        if(Level1_GameState == Level1_GameState.Game){
            gameTimer += Time.deltaTime;
            if(gameTimer >= 30){
                UpdateLevel1_GameState(Level1_GameState.Question1);
                Car[] cars = FindObjectsOfType<Car>();
                foreach(Car item in cars){
                    Destroy(item.gameObject);
                }
            }

            carSpawnTimer += Time.deltaTime;
            if(carSpawnTimer >= 2){
                SpawnCar();
                carSpawnTimer = 0;
            }
        }
    }

    public void UpdateLevel1_GameState(Level1_GameState newState){
        Level1_GameState = newState;

        switch(Level1_GameState){
            case Level1_GameState.Explain:
                  StartCoroutine(Explain());
                break;
            case Level1_GameState.Game:
                health_Image.SetActive(true);
                explain_Button.SetActive(true);
                return_Button.SetActive(true);
                exit_Button.SetActive(true);
                break;
            case Level1_GameState.Question1:
                health_Image.SetActive(false);
                power_Image.SetActive(true);
                DialogueBg_Image.gameObject.SetActive(true);
                questions[0].SetActive(true);
                foreach(GameObject item in batterys){
                    int index = Array.IndexOf(batterys, item);
                    Instantiate(item, batterySpawnPos[index].transform.position, Quaternion.identity);
                }
                break;
            case Level1_GameState.Question2:
                questions[0].SetActive(false);
                questions[1].SetActive(true);
                foreach(GameObject item in batterys){
                    int index = Array.IndexOf(batterys, item);
                    Instantiate(item, batterySpawnPos[index].transform.position, Quaternion.identity);
                }
                break;
            case Level1_GameState.Question3:
                questions[1].SetActive(false);
                questions[2].SetActive(true);
                foreach(GameObject item in batterys){
                    int index = Array.IndexOf(batterys, item);
                    Instantiate(item, batterySpawnPos[index].transform.position, Quaternion.identity);
                }
                break;
            case Level1_GameState.Question4:
                questions[2].SetActive(false);
                questions[3].SetActive(true);
                foreach(GameObject item in batterys){
                    int index = Array.IndexOf(batterys, item);
                    Instantiate(item, batterySpawnPos[index].transform.position, Quaternion.identity);
                }
                break;
            case Level1_GameState.Question5:
                questions[3].SetActive(false);
                questions[4].SetActive(true);
                foreach(GameObject item in batterys){
                    int index = Array.IndexOf(batterys, item);
                    Instantiate(item, batterySpawnPos[index].transform.position, Quaternion.identity);
                }
                break;
            case Level1_GameState.Question6:
                questions[4].SetActive(false);
                questions[5].SetActive(true);
                foreach(GameObject item in batterys){
                    int index = Array.IndexOf(batterys, item);
                    Instantiate(item, batterySpawnPos[index].transform.position, Quaternion.identity);
                }
                break;
            case Level1_GameState.Question7:
                questions[5].SetActive(false);
                questions[6].SetActive(true);
                foreach(GameObject item in batterys){
                    int index = Array.IndexOf(batterys, item);
                    Instantiate(item, batterySpawnPos[index].transform.position, Quaternion.identity);
                }
                break;
            case Level1_GameState.Success:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Win");
                DialogueBg_Image.gameObject.SetActive(false);
                questions[0].transform.parent.gameObject.SetActive(false);
                success_Image.SetActive(true);
                break;
            case Level1_GameState.Fail:
                AudioManager.Instance.StopAll();
                AudioManager.Instance.PlaySound("Lose");
                DialogueBg_Image.gameObject.SetActive(false);
                questions[0].transform.parent.gameObject.SetActive(false);
                fail_Image.SetActive(true);
                break;
        }
    }
    public void UpdateLevel1_GameState_Int(int newState){
        UpdateLevel1_GameState((Level1_GameState)newState);
    }

    public void UpdateLevel1_GameState_Later(int later){
        StartCoroutine(UpdateState_Later(later)); 
    }

    
    IEnumerator UpdateState_Later(int later){
        yield return new WaitForSeconds(later);
        UpdateLevel1_GameState_Int((int)++Level1_GameState);
    }

    public void UpdatePower(bool isCorrect){
        if(isCorrect){
            if(power < 4){
                power++;
                poewr_Image.sprite = powerSprites[power];
                if(power == 4){
                    UpdateLevel1_GameState(Level1_GameState.Success);
                    return;
                }

                if(Level1_GameState == Level1_GameState.Question7){
                    UpdateLevel1_GameState(Level1_GameState.Fail);
                    return;
                }
                AudioManager.Instance.PlaySound("Correct");
                good_Image.SetActive(true);
            }
        }
        else{
            if(Level1_GameState == Level1_GameState.Question7){
                fail_Text.text = "請回答正確的答案，幫電池充滿電!";
                UpdateLevel1_GameState(Level1_GameState.Fail);
                return;
            }
            AudioManager.Instance.PlaySound("Error");
            bad_Image.SetActive(true);
        }

        
        UpdateLevel1_GameState_Later(4);
    }

    public void HandleBatrry(){
        Battery[] batrrys = FindObjectsOfType<Battery>();
        foreach(Battery item in batrrys){
            Destroy(item.gameObject);
        }
    }

    private void SpawnCar(){
        while(true){
            int carNum = UnityEngine.Random.Range(0, 4);
            int spawnPosNum = UnityEngine.Random.Range(0, 4);
            if(carNum != previousCarNum && spawnPosNum != previousSpawnPosNum){
                previousCarNum = carNum;
                previousSpawnPosNum = spawnPosNum;
                break;
            }
        }
        
        Instantiate(cars[previousCarNum], carSpawnPos[previousSpawnPosNum].position, Quaternion.identity);
    }

    public void HandldCarCollision(bool isCollision){
        leftWall.SetActive(!isCollision);
    }

    public void HandlePlayerRespawn(){
        AudioManager.Instance.PlaySound("Car");
        player.transform.position = playerRespawnPos.position;
        leftWall.SetActive(true);
        StartCoroutine(InvincibleMode());
    }

    IEnumerator InvincibleMode(){
        SpriteRenderer[] playerRenderers = player.GetComponentsInChildren<SpriteRenderer>(); 

        Car[] previousCars = FindObjectsOfType<Car>();
        foreach(Car item in previousCars){
            if(item.TryGetComponent<CapsuleCollider2D>(out CapsuleCollider2D collider)){
                collider.enabled = false;
            }
            else{
                item.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        foreach (SpriteRenderer item in playerRenderers)
        {
            item.color = new Color(1, 1, 1, 0.2470588f);
        }

        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer item in playerRenderers)
        {
            item.color = new Color(1, 1, 1, 0.8117647f);
        }

        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer item in playerRenderers)
        {
            item.color = new Color(1, 1, 1, 0.2470588f);
        }

        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer item in playerRenderers)
        {
            item.color = new Color(1, 1, 1, 0.8117647f);
        }

        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer item in playerRenderers)
        {
            item.color = new Color(1, 1, 1, 0.2470588f);
        }

        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer item in playerRenderers)
        {
            item.color = new Color(1, 1, 1, 0.8117647f);
        }

        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer item in playerRenderers)
        {
            item.color = new Color(1, 1, 1, 1);
        }

        Car[] currentCars = FindObjectsOfType<Car>();
        foreach(Car item in currentCars){
            if(item.TryGetComponent<CapsuleCollider2D>(out CapsuleCollider2D collider)){
                collider.enabled = true;
            }
            else{
                item.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    IEnumerator Explain(){
        yield return new WaitForSeconds(2);
        explain_Image.SetActive(true);
    }

    public void CloseExplain(){
        if(Level1_GameState == Level1_GameState.Explain){
            UpdateLevel1_GameState(Level1_GameState.Game);
        }
    }

    public void CheckHealth(){
        health--;
        if(health <= 0){
            health = 0;
            player.SetActive(false);
            fail_Text.text = "請注意交通安全!";
            UpdateLevel1_GameState(Level1_GameState.Fail);
        }

        health_Text.text = $"X {health}";
    }

    public void ChangeScene(string sceneName){
        GameManager.Instance.ChangeScene(sceneName);
    }
}
