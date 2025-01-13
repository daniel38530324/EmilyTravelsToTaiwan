using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dialogue Data", menuName = "Scriptable Objects/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string[] Dialogues;
    public string[] Highlights;
}
