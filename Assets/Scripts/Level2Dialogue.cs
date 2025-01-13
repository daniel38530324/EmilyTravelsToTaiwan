using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System.Timers;


public class Level2Dialogue : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject dialogueImage1;
    [SerializeField] private GameObject dialogueImage2;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text recordText;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private GameObject nextButton, pass_Button;

    [Header("Setting")]
    [SerializeField] private float textSpeed;

    private int index;
    private StringBuilder sb = new StringBuilder();
    private string currentName, highlightText;

    private void Start() {
        StartDialogue();
    }

    private void StartDialogue(){
        index = 0;
        dialogueText.text = CheckCharactor();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        nextButton.SetActive(false);
        AudioManager.Instance.PlaySound(index.ToString());

        foreach(char c in dialogueData.Dialogues[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        CheckLine(index);

        sb.AppendLine(dialogueText.text);
        recordText.text = sb.ToString();

        nextButton.SetActive(true);
    }

    private void NextLine()
    {
        Level2_PlotManager.Instance.CloseAnyClickPoint();
        if(index < dialogueData.Dialogues.Length - 1)
        {
            index++;
            dialogueText.text = CheckCharactor();

            StartCoroutine(TypeLine());
        }
        else
        {
            pass_Button.SetActive(true);
            gameObject.SetActive(false);
            //musicGame.SetActive(true);
            //bkGround.GetComponent<AudioSource>().enabled = false;
        }
    }

    public void DialogueClick()
    {
        if(index == 0 || index == 5 || index == 6 || index == 12){
            if(dialogueText.text == currentName + dialogueData.Dialogues[index]){
                AudioManager.Instance.StopSound(index.ToString());
                NextLine();
            }
            else{
                StopAllCoroutines();
                //dialogueText.text = currentName + dialogueData.Dialogues[index];
                CheckLine(index);

                sb.AppendLine(dialogueText.text);
                recordText.text = sb.ToString();

                nextButton.SetActive(true);
            }
        }
        else{
            if(dialogueText.text == highlightText){
                AudioManager.Instance.StopSound(index.ToString());
                NextLine();
            }
            else{
                StopAllCoroutines();
                //dialogueText.text = currentName + dialogueData.Dialogues[index];
                CheckLine(index);

                sb.AppendLine(dialogueText.text);
                recordText.text = sb.ToString();

                nextButton.SetActive(true);
            }
        } 
    }

    private void CheckLine(int index){
        switch(index){
            case 1:
                Highlight(dialogueData.Highlights[0]);
                dialogueText.text = highlightText;
                Level2_PlotManager.Instance.SetClickPointActive(0);
                Level2_PlotManager.Instance.SetWordAndGrammarActive(0);
                break;
            case 2:
                Highlight(dialogueData.Highlights[0]);
                Level2_PlotManager.Instance.SetClickPointActive(1);
                dialogueText.text = highlightText;
                break;
            case 3:
                Highlight(dialogueData.Highlights[0]);
                Level2_PlotManager.Instance.SetClickPointActive(2);
                dialogueText.text = highlightText;
                break;
            case 4:
                Highlight(dialogueData.Highlights[0]);
                Level2_PlotManager.Instance.SetClickPointActive(3);
                dialogueText.text = highlightText;
                break;
            case 7:
                Highlight(dialogueData.Highlights[0], dialogueData.Highlights[1], dialogueData.Highlights[1]);
                Level2_PlotManager.Instance.SetClickPointActive(4);
                Level2_PlotManager.Instance.SetClickPointActive(5);
                Level2_PlotManager.Instance.SetClickPointActive(6);
                Level2_PlotManager.Instance.SetWordAndGrammarActive(1);
                dialogueText.text = highlightText;
                break;
            case 8:
                Highlight(dialogueData.Highlights[1]);
                Level2_PlotManager.Instance.SetClickPointActive(7);
                dialogueText.text = highlightText;
                break;
            case 9:
                Highlight(dialogueData.Highlights[1]);
                Level2_PlotManager.Instance.SetClickPointActive(8);
                dialogueText.text = highlightText;
                break;
            case 10:
                Highlight(dialogueData.Highlights[1]);
                Level2_PlotManager.Instance.SetClickPointActive(9);
                dialogueText.text = highlightText;
                break;
            case 11:
                Highlight(dialogueData.Highlights[1]);
                Level2_PlotManager.Instance.SetClickPointActive(10);
                dialogueText.text = highlightText;
                break;
            case 13:
                Highlight(dialogueData.Highlights[0]);
                Level2_PlotManager.Instance.SetClickPointActive(11);
                dialogueText.text = highlightText;
                break;
            default:
                dialogueText.text = currentName + dialogueData.Dialogues[index];
                break;
        }
    }

    private string CheckCharactor(){
        if(index % 2 != 0){
            currentName = "<color=#00BFFF>柏翰：</color>";
            dialogueImage1.SetActive(false);
            dialogueImage2.SetActive(true);
        }
        else{
            currentName = "<color=#F19CC1>艾蜜莉：</color>";
            dialogueImage1.SetActive(true);
            dialogueImage2.SetActive(false);
        }
        return currentName;
    }

    
    private string Highlight(string targetText, string targetText2 = null, string targetText3 = null){
        int target = dialogueData.Dialogues[index].IndexOf(targetText);
        string behindString = dialogueData.Dialogues[index].Insert(target + targetText.Length, "</color>");
        string frontString = behindString.Insert(target, "<color=#FFBB00>");
        string resultString = frontString;

        if(targetText2 != null){
            target = resultString.IndexOf(targetText2);
            behindString = resultString.Insert(target + targetText2.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText3 != null){
            target = resultString.LastIndexOf(targetText3);
            behindString = resultString.Insert(target + targetText3.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        highlightText = currentName + resultString;
        return highlightText;
    }


}
