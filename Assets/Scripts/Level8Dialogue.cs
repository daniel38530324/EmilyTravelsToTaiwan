using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System.Timers;
using System.ComponentModel.Design;


public class Level8Dialogue : MonoBehaviour
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
        Level8_PlotManager.Instance.CloseAnyClickPoint();
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
        }
    }

    public void DialogueClick()
    {
        if(!canClick){
            return;
        }

        if(index == 0){
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
        else{
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
    }

    private void CheckLine(int index){
        switch(index){
            case 1:
                Highlight(dialogueData.Highlights[0], null, null, null, null, null, null, null, null, null, null, true);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(0);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(24);
                break;
            case 2:
                Highlight(dialogueData.Highlights[2], dialogueData.Highlights[3], null, null, null, null, null, dialogueData.Highlights[1], dialogueData.Highlights[4], dialogueData.Highlights[5], dialogueData.Highlights[6]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(1);
                Level8_PlotManager.Instance.SetClickPointActive(2);
                Level8_PlotManager.Instance.SetClickPointActive(3);
                Level8_PlotManager.Instance.SetClickPointActive(4);
                Level8_PlotManager.Instance.SetClickPointActive(5);
                Level8_PlotManager.Instance.SetClickPointActive(6);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(0);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(1);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(25);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(26);
                break;
            case 3:
                Highlight(dialogueData.Highlights[7]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(7);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(2);
                break;
            case 4:
                Highlight(dialogueData.Highlights[8], dialogueData.Highlights[9]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(8);
                Level8_PlotManager.Instance.SetClickPointActive(9);
                Level8_PlotManager.Instance.SetClickPointActive(10);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(3);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(4);
                break;
            case 5:
                Highlight(dialogueData.Highlights[10], dialogueData.Highlights[11], dialogueData.Highlights[12], dialogueData.Highlights[13], dialogueData.Highlights[14]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(11);
                Level8_PlotManager.Instance.SetClickPointActive(12);
                Level8_PlotManager.Instance.SetClickPointActive(13);
                Level8_PlotManager.Instance.SetClickPointActive(14);
                Level8_PlotManager.Instance.SetClickPointActive(15);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(5);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(6);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(7);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(8);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(9);
                break;
            case 6:
                Highlight(dialogueData.Highlights[13], dialogueData.Highlights[15], dialogueData.Highlights[16], dialogueData.Highlights[17], dialogueData.Highlights[18], dialogueData.Highlights[19], dialogueData.Highlights[20]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(16);
                Level8_PlotManager.Instance.SetClickPointActive(17);
                Level8_PlotManager.Instance.SetClickPointActive(18);
                Level8_PlotManager.Instance.SetClickPointActive(19);
                Level8_PlotManager.Instance.SetClickPointActive(20);
                Level8_PlotManager.Instance.SetClickPointActive(21);
                Level8_PlotManager.Instance.SetClickPointActive(22);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(10);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(11);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(12);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(13);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(14);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(15);
                break;
            case 7:
                Highlight(dialogueData.Highlights[20], dialogueData.Highlights[21], dialogueData.Highlights[22], dialogueData.Highlights[23]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(23);
                Level8_PlotManager.Instance.SetClickPointActive(24);
                Level8_PlotManager.Instance.SetClickPointActive(25);
                Level8_PlotManager.Instance.SetClickPointActive(26);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(16);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(17);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(18);
                break;
            case 8:
                Highlight(dialogueData.Highlights[21], dialogueData.Highlights[24]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(27);
                Level8_PlotManager.Instance.SetClickPointActive(28);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(19);
                break;
            case 9:
                Highlight(dialogueData.Highlights[12]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(29);
                break;
            case 10:
                Highlight(dialogueData.Highlights[24], dialogueData.Highlights[25], dialogueData.Highlights[26], dialogueData.Highlights[27]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(30);
                Level8_PlotManager.Instance.SetClickPointActive(31);
                Level8_PlotManager.Instance.SetClickPointActive(32);
                Level8_PlotManager.Instance.SetClickPointActive(33);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(20);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(21);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(22);
                break;
            case 11:
                Highlight(dialogueData.Highlights[28]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(34);
                Level8_PlotManager.Instance.SetWordAndGrammarActive(23);
                break;
            case 12:
                Highlight(dialogueData.Highlights[24]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(35);
                break;
            case 13:
                Highlight(dialogueData.Highlights[24]);
                dialogueText.text = highlightText;
                Level8_PlotManager.Instance.SetClickPointActive(36);
                break;
            default:
                dialogueText.text = currentName + dialogueData.Dialogues[index];
                break;
        }
    }

    private string CheckCharactor(){
        if(index % 2 == 0){
        currentName = "<color=#00EC00>欣怡：</color>";
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

    
    private string Highlight(string targetText, string targetText2 = null, string targetText3 = null, string targetText4 = null, string targetText5 = null, string targetText6 = null, string targetText7 = null, string targetText8 = null, string targetText9 = null, string targetText10 = null, string targetText11 = null, bool isFirstGrammar = false){
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
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText4 != null){
            target = resultString.IndexOf(targetText4);
            behindString = resultString.Insert(target + targetText4.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText5 != null){
            target = resultString.IndexOf(targetText5);
            behindString = resultString.Insert(target + targetText5.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText6 != null){
            target = resultString.IndexOf(targetText6);
            behindString = resultString.Insert(target + targetText6.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText7 != null){
            target = resultString.IndexOf(targetText7);
            behindString = resultString.Insert(target + targetText7.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText8 != null){
            target = resultString.LastIndexOf(targetText8);
            behindString = resultString.Insert(target + targetText8.Length, "</color=red>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        if(targetText9 != null){
            target = resultString.LastIndexOf(targetText9);
            behindString = resultString.Insert(target + targetText9.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        if(targetText10 != null){
            target = resultString.LastIndexOf(targetText10);
            behindString = resultString.Insert(target + targetText10.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        if(targetText11 != null){
            target = resultString.LastIndexOf(targetText11);
            behindString = resultString.Insert(target + targetText11.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        highlightText = currentName + resultString;
        return highlightText;
    }
}
