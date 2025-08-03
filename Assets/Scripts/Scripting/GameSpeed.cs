using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    TextMeshPro tm;
    void Start()
    {
        tm = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        tm.text = "Game Speed: " + ChickenProcessor.Instance.GetGameSpeed().ToString();
    }
}
