using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Data", menuName = "Scriptable Objects/Sound Data")]
public class SoundData : ScriptableObject
{
    public Sounds[] Musics;
    public Sounds[] Sounds;
}
