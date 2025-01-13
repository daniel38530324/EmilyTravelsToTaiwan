using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System.Timers;
using System.ComponentModel.Design;


public class Level3Dialogue : MonoBehaviour
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
    private bool canClick = true;

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
        Level3_PlotManager.Instance.CloseAnyClickPoint();
        if(index < dialogueData.Dialogues.Length - 1)
        {
            if(index == 3){
                StartCoroutine(ChangeBackground());
                return;
            }
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
        if(!canClick){
            return;
        }

        if(index == 1 || index == 2 || index == 4 || index == 5 || index == 7 || index == 8 || index == 11){
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
            case 1:
                Highlight(dialogueData.Highlights[0], dialogueData.Highlights[1]);
                dialogueText.text = highlightText;
                Level3_PlotManager.Instance.SetClickPointActive(0);
                Level3_PlotManager.Instance.SetClickPointActive(1);
                Level3_PlotManager.Instance.SetClickPointActive(2);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(0);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(1);
                break;
            case 2:
                Highlight(dialogueData.Highlights[2], dialogueData.Highlights[3]);
                dialogueText.text = highlightText;
                Level3_PlotManager.Instance.SetClickPointActive(3);
                Level3_PlotManager.Instance.SetClickPointActive(4);
                Level3_PlotManager.Instance.SetClickPointActive(5);
                Level3_PlotManager.Instance.SetClickPointActive(6);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(2);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(3);
                break;
            case 4:
                Highlight(dialogueData.Highlights[4]);
                dialogueText.text = highlightText;
                Level3_PlotManager.Instance.SetClickPointActive(7);
                Level3_PlotManager.Instance.SetClickPointActive(8);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(4);
                break;
            case 5:
                Highlight(dialogueData.Highlights[9], null, null, null, true);
                dialogueText.text = highlightText;
                Level3_PlotManager.Instance.SetClickPointActive(9);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(9);
                break;
            case 7:
                Highlight(dialogueData.Highlights[5], dialogueData.Highlights[6], dialogueData.Highlights[10]);
                dialogueText.text = highlightText;
                Level3_PlotManager.Instance.SetClickPointActive(10);
                Level3_PlotManager.Instance.SetClickPointActive(11);
                Level3_PlotManager.Instance.SetClickPointActive(12);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(5);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(6);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(10);
                break;
            case 8:
                Highlight(dialogueData.Highlights[7], dialogueData.Highlights[5], dialogueData.Highlights[11]);
                dialogueText.text = highlightText;
                Level3_PlotManager.Instance.SetClickPointActive(13);
                Level3_PlotManager.Instance.SetClickPointActive(14);
                Level3_PlotManager.Instance.SetClickPointActive(15);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(7);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(11);
                break;
            case 11:
                Highlight(dialogueData.Highlights[8], null, null, dialogueData.Highlights[8]);
                dialogueText.text = highlightText;
                Level3_PlotManager.Instance.SetClickPointActive(16);
                Level3_PlotManager.Instance.SetClickPointActive(17);
                Level3_PlotManager.Instance.SetWordAndGrammarActive(8);
                break;
            default:
                dialogueText.text = currentName + dialogueData.Dialogues[index];
                break;
        }
    }

    private string CheckCharactor(){
        if(index < 4){
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
        }
        else{
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
        }
        
        return currentName;
    }

    
    private string Highlight(string targetText, string targetText2 = null, string targetText3 = null, string targetText4 = null, bool isFirstGrammar = false){
        string defaultColor = isFirstGrammar ? "<color=red>" : "<color=#FFBB00>";
        int target = dialogueData.Dialogues[index].IndexOf(targetText);
        string behindString = dialogueData.Dialogues[index].Insert(target + targetText.Length, "</color>");
        string frontString = behindString.Insert(target, defaultColor);
        string resultString = frontString;

        if(targetText2 != null){
            target = resultString.IndexOf(targetText2);
            behindString = resultString.Insert(target + targetText2.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText3 != null){
            target = resultString.IndexOf(targetText3);
            behindString = resultString.Insert(target + targetText3.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        if(targetText4 != null){
            target = resultString.LastIndexOf(targetText4);
            behindString = resultString.Insert(target + targetText4.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        highlightText = currentName + resultString;
        return highlightText;
    }

    IEnumerator ChangeBackground(){
        canClick = false;
        Level3_PlotManager.Instance.ActiveGradient();
        yield return new WaitForSeconds(1.5f);
        dialogueText.text = "";
        yield return new WaitForSeconds(1.5f);
        index++;
        dialogueText.text = CheckCharactor();

        StartCoroutine(TypeLine());
        canClick = true;
    }
}
