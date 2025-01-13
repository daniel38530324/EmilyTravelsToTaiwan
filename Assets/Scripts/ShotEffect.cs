using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2);
    }
}
