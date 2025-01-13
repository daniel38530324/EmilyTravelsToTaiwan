using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System.Timers;


public class Level1Dialogue : MonoBehaviour
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
        Level1_PlotManager.Instance.CloseAnyClickPoint();
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
        if(index == 0 || index == 4 || index == 6 || index == 10 || index == 12){
            if(dialogueText.text == highlightText){
                AudioManager.Instance.StopSound(index.ToString());
                NextLine();
            }
            else{
                StopAllCoroutines();
                CheckLine(index);

                sb.AppendLine(dialogueText.text);
                recordText.text = sb.ToString();

                nextButton.SetActive(true);
            }
        }
        else{
            if(dialogueText.text == currentName + dialogueData.Dialogues[index]){
                AudioManager.Instance.StopSound(index.ToString());
                NextLine();
            }
            else{
                StopAllCoroutines();
                CheckLine(index);

                sb.AppendLine(dialogueText.text);
                recordText.text = sb.ToString();

                nextButton.SetActive(true);
            }
        } 
    }

    private void CheckLine(int index){
        switch(index){
            case 0:
                Highlight(dialogueData.Highlights[0], dialogueData.Highlights[1]);
                dialogueText.text = highlightText;
                Level1_PlotManager.Instance.SetClickPointActive(0);
                Level1_PlotManager.Instance.SetClickPointActive(1);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(0);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(1);
                break;
            case 4:
                Highlight(dialogueData.Highlights[2], dialogueData.Highlights[3]);
                dialogueText.text = highlightText;
                Level1_PlotManager.Instance.SetClickPointActive(2);
                Level1_PlotManager.Instance.SetClickPointActive(3);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(2);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(3);
                break;
            case 6:
                Highlight(null, null, dialogueData.Highlights[10], dialogueData.Highlights[11], dialogueData.Highlights[12]);
                dialogueText.text = highlightText;
                Level1_PlotManager.Instance.SetClickPointActive(4);
                Level1_PlotManager.Instance.SetClickPointActive(5);
                Level1_PlotManager.Instance.SetClickPointActive(6);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(4);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(9);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(10);
                break;
            case 10:
                Highlight(dialogueData.Highlights[5], dialogueData.Highlights[6]);
                dialogueText.text = highlightText;
                Level1_PlotManager.Instance.SetClickPointActive(8);
                Level1_PlotManager.Instance.SetClickPointActive(9);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(5);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(6);
                break;
            case 12:
                Highlight(dialogueData.Highlights[7], dialogueData.Highlights[8]);
                dialogueText.text = highlightText;
                Level1_PlotManager.Instance.SetClickPointActive(10);
                Level1_PlotManager.Instance.SetClickPointActive(11);
                Level1_PlotManager.Instance.SetClickPointActive(12);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(7);
                Level1_PlotManager.Instance.SetWordAndGrammarActive(8);
                break;
            default:
                dialogueText.text = currentName + dialogueData.Dialogues[index];
                break;
        }
    }

    private string CheckCharactor(){
        if(index % 2 == 0){
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

    
    private string Highlight(string targetText, string targetText2 = null, string targetText3 = null, string targetText4 = null, string targetText5 = null){
        int target;
        string behindString, frontString, resultString;

        if(targetText == null){
            resultString = dialogueData.Dialogues[index];
        }
        else{
            target = dialogueData.Dialogues[index].IndexOf(targetText);
            behindString = dialogueData.Dialogues[index].Insert(target + targetText.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText2 != null){
            target = resultString.IndexOf(targetText2);
            behindString = resultString.Insert(target + targetText2.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText3 != null){
            target = resultString.IndexOf(targetText3);
            behindString = resultString.Insert(target + targetText3.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FF8000>");
            resultString = frontString;
        }

        if(targetText4 != null){
            target = resultString.IndexOf(targetText4);
            behindString = resultString.Insert(target + targetText4.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        if(targetText5 != null){
            target = resultString.IndexOf(targetText5);
            behindString = resultString.Insert(target + targetText5.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        highlightText = currentName + resultString;
        return highlightText;
    }


}
