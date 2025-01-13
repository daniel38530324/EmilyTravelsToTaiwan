using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] float rotSpeed;
    void Update()
    {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }
}
