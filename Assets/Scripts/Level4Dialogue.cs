using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System.Timers;


public class Level4Dialogue : MonoBehaviour
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
        Level4_PlotManager.Instance.CloseAnyClickPoint();
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
        if(index == 7 || index == 9){
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
            case 0:
                Highlight(dialogueData.Highlights[0]);
                dialogueText.text = highlightText;
                Level4_PlotManager.Instance.SetClickPointActive(0);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(0);
                break;
            case 1:
                Highlight(dialogueData.Highlights[1]);
                dialogueText.text = highlightText;
                Level4_PlotManager.Instance.SetClickPointActive(1);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(1);
                break;
            case 2:
                Highlight(dialogueData.Highlights[2], dialogueData.Highlights[3], dialogueData.Highlights[4], dialogueData.Highlights[5]);
                dialogueText.text = highlightText;
                Level4_PlotManager.Instance.SetClickPointActive(2);
                Level4_PlotManager.Instance.SetClickPointActive(3);
                Level4_PlotManager.Instance.SetClickPointActive(4);
                Level4_PlotManager.Instance.SetClickPointActive(5);
                Level4_PlotManager.Instance.SetClickPointActive(6);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(2);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(3);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(4);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(5);
                break;
            case 3:
                Highlight(dialogueData.Highlights[2], dialogueData.Highlights[3], dialogueData.Highlights[4], dialogueData.Highlights[6]);
                dialogueText.text = highlightText;
                Level4_PlotManager.Instance.SetClickPointActive(7);
                Level4_PlotManager.Instance.SetClickPointActive(8);
                Level4_PlotManager.Instance.SetClickPointActive(9);
                Level4_PlotManager.Instance.SetClickPointActive(10);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(6);
                break;
            case 4:
                Highlight(dialogueData.Highlights[2], dialogueData.Highlights[7], null, null, dialogueData.Highlights[11], null, dialogueData.Highlights[2]);
                dialogueText.text = highlightText;
                Level4_PlotManager.Instance.SetClickPointActive(11);
                Level4_PlotManager.Instance.SetClickPointActive(12);
                Level4_PlotManager.Instance.SetClickPointActive(13);
                Level4_PlotManager.Instance.SetClickPointActive(14);
                Level4_PlotManager.Instance.SetClickPointActive(15);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(7);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(11);
                break;
            case 5:
                Highlight(dialogueData.Highlights[8], dialogueData.Highlights[3], null, null, dialogueData.Highlights[12], null, dialogueData.Highlights[8]);
                dialogueText.text = highlightText;
                Level4_PlotManager.Instance.SetClickPointActive(16);
                Level4_PlotManager.Instance.SetClickPointActive(17);
                Level4_PlotManager.Instance.SetClickPointActive(18);
                Level4_PlotManager.Instance.SetClickPointActive(19);
                Level4_PlotManager.Instance.SetClickPointActive(20);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(8);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(12);
                break;
            case 6:
                Highlight(dialogueData.Highlights[2], dialogueData.Highlights[9], dialogueData.Highlights[3], dialogueData.Highlights[8]);
                dialogueText.text = highlightText;
                Level4_PlotManager.Instance.SetClickPointActive(21);
                Level4_PlotManager.Instance.SetClickPointActive(22);
                Level4_PlotManager.Instance.SetClickPointActive(23);
                Level4_PlotManager.Instance.SetClickPointActive(24);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(9);
                break;
            case 8:
                Highlight(dialogueData.Highlights[10], null, null, null, dialogueData.Highlights[13], dialogueData.Highlights[14], null);
                dialogueText.text = highlightText;
                Level4_PlotManager.Instance.SetClickPointActive(25);
                Level4_PlotManager.Instance.SetClickPointActive(26);
                Level4_PlotManager.Instance.SetClickPointActive(27);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(10);
                Level4_PlotManager.Instance.SetWordAndGrammarActive(13);
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

    
    private string Highlight(string targetText, string targetText2 = null, string targetText3 = null, string targetText4 = null, string targetText5 = null, string targetText6 = null, string targetText7 = null){
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
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        if(targetText6 != null){
            target = resultString.IndexOf(targetText6);
            behindString = resultString.Insert(target + targetText6.Length, "</color>");
            frontString = behindString.Insert(target, "<color=red>");
            resultString = frontString;
        }

        if(targetText7 != null){
            target = resultString.LastIndexOf(targetText7);
            behindString = resultString.Insert(target + targetText7.Length, "</color>");
            frontString = behindString.Insert(target, "<color=#FFBB00>");
            resultString = frontString;
        }


        highlightText = currentName + resultString;
        return highlightText;
    }


}
