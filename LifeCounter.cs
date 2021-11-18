using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public Text LifeText;
    public GameManager GM;

    public void Awake()
    {
        GM = FindObjectOfType<GameManager>();

        LifeText = gameObject.GetComponent<Text>();

        LifeText.text = GM.Lives.ToString();
    }
    public void FixedUpdate()
    {
        LifeText.text = GM.Lives.ToString();
    }
}