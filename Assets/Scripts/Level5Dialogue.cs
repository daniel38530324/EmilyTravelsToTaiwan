using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System.Timers;
using System.ComponentModel.Design;


public class Level5Dialogue : MonoBehaviour
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
        Level5_PlotManager.Instance.CloseAnyClickPoint();
        if(index < dialogueData.Dialogues.Length - 1)
        {
            if(index == 2){
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

        if(index == 1 || index == 4 || index == 5 || index == 6 || index == 7 || index == 8 || index == 9 || index == 10 || index == 11 || index == 13 || index == 14 || index == 15 || index == 16 || index == 17){
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
                Highlight(dialogueData.Highlights[2], null, null, null, null, null, null,dialogueData.Highlights[0], dialogueData.Highlights[1]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(0);
                Level5_PlotManager.Instance.SetClickPointActive(1);
                Level5_PlotManager.Instance.SetClickPointActive(2);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(22);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(0);
                break;
            case 4:
                Highlight(dialogueData.Highlights[3], dialogueData.Highlights[4]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(3);
                Level5_PlotManager.Instance.SetClickPointActive(4);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(1);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(2);
                break;
            case 5:
                Highlight(dialogueData.Highlights[5]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(5);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(3);
                break;
            case 6:
                Highlight(dialogueData.Highlights[6]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(6);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(4);
                break;
            case 7:
                Highlight(dialogueData.Highlights[6]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(7);
                break;
            case 8:
                Highlight(dialogueData.Highlights[7], dialogueData.Highlights[8]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(8);
                Level5_PlotManager.Instance.SetClickPointActive(9);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(5);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(6);
                break;
            case 9:
                Highlight(dialogueData.Highlights[7], dialogueData.Highlights[9], dialogueData.Highlights[5], dialogueData.Highlights[3], dialogueData.Highlights[4], dialogueData.Highlights[10]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(10);
                Level5_PlotManager.Instance.SetClickPointActive(11);
                Level5_PlotManager.Instance.SetClickPointActive(12);
                Level5_PlotManager.Instance.SetClickPointActive(13);
                Level5_PlotManager.Instance.SetClickPointActive(14);
                Level5_PlotManager.Instance.SetClickPointActive(15);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(7);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(8);
                break;
            case 10:
                Highlight(dialogueData.Highlights[4], dialogueData.Highlights[11], dialogueData.Highlights[5]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(16);
                Level5_PlotManager.Instance.SetClickPointActive(17);
                Level5_PlotManager.Instance.SetClickPointActive(18);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(9);
                break;
            case 11:
                Highlight(dialogueData.Highlights[12]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(19);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(10);
                break;
            case 13:
                Highlight(dialogueData.Highlights[13], dialogueData.Highlights[14], dialogueData.Highlights[15]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(20);
                Level5_PlotManager.Instance.SetClickPointActive(21);
                Level5_PlotManager.Instance.SetClickPointActive(22);
                Level5_PlotManager.Instance.SetClickPointActive(23);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(11);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(12);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(13);
                break;
            case 14:
                Highlight(dialogueData.Highlights[16], dialogueData.Highlights[7]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(24);
                Level5_PlotManager.Instance.SetClickPointActive(25);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(14);
                break;
            case 15:
                Highlight(dialogueData.Highlights[17], dialogueData.Highlights[7], null, null, null, null, null, dialogueData.Highlights[18]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(26);
                Level5_PlotManager.Instance.SetClickPointActive(27);
                Level5_PlotManager.Instance.SetClickPointActive(28);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(15);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(16);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(23);
                break;
            case 16:
                Highlight(dialogueData.Highlights[19], dialogueData.Highlights[20], dialogueData.Highlights[21], dialogueData.Highlights[22]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(29);
                Level5_PlotManager.Instance.SetClickPointActive(30);
                Level5_PlotManager.Instance.SetClickPointActive(31);
                Level5_PlotManager.Instance.SetClickPointActive(32);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(17);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(18);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(19);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(20);
                break;
            case 17:
                Highlight(dialogueData.Highlights[7], dialogueData.Highlights[10], dialogueData.Highlights[4], dialogueData.Highlights[23], dialogueData.Highlights[7], dialogueData.Highlights[9], dialogueData.Highlights[24]);
                dialogueText.text = highlightText;
                Level5_PlotManager.Instance.SetClickPointActive(33);
                Level5_PlotManager.Instance.SetClickPointActive(34);
                Level5_PlotManager.Instance.SetClickPointActive(35);
                Level5_PlotManager.Instance.SetClickPointActive(36);
                Level5_PlotManager.Instance.SetClickPointActive(37);
                Level5_PlotManager.Instance.SetClickPointActive(38);
                Level5_PlotManager.Instance.SetClickPointActive(39);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(8);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(21);
                Level5_PlotManager.Instance.SetWordAndGrammarActive(22);
                break;
            default:
                dialogueText.text = currentName + dialogueData.Dialogues[index];
                break;
        }
    }

    private string CheckCharactor(){
        if(index < 3){
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
        }
        else{
            if(index % 2 != 0){
            currentName = "<color=#00EC00>欣怡：</color>";
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

    
    private string Highlight(string targetText, string targetText2 = null, string targetText3 = null, string targetText4 = null, string targetText5 = null, string targetText6 = null, string targetText7 = null, string targetText8 = null, string targetText9 = null, bool isFirstGrammar = false){
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
            target = resultString.LastIndexOf(targetText4);
            behindString = resultString.Insert(target + targetText4.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText5 != null){
            target = resultString.LastIndexOf(targetText5);
            behindString = resultString.Insert(target + targetText5.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText6 != null){
            target = resultString.LastIndexOf(targetText6);
            behindString = resultString.Insert(target + targetText6.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText7 != null){
            target = resultString.LastIndexOf(targetText7);
            behindString = resultString.Insert(target + targetText7.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }

        if(targetText8 != null){
            target = resultString.LastIndexOf(targetText8);
            behindString = resultString.Insert(target + targetText8.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        if(targetText9 != null){
            target = resultString.LastIndexOf(targetText9);
            behindString = resultString.Insert(target + targetText9.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        highlightText = currentName + resultString;
        return highlightText;
    }

    IEnumerator ChangeBackground(){
        canClick = false;
        Level5_PlotManager.Instance.ActiveGradient();
        yield return new WaitForSeconds(1.5f);
        dialogueText.text = "";
        yield return new WaitForSeconds(1.5f);
        index++;
        dialogueText.text = CheckCharactor();

        StartCoroutine(TypeLine());
        canClick = true;
    }
}
