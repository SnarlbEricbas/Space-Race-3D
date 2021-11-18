using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Lives = 3;

    private void Awake()
    {
        GameManager instance = FindObjectOfType<GameManager>();

        if (instance.GetComponent<GameManager>() != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void AddLife()
    {
        Lives = Lives + 1;
    }

    public void RemoveLife()
    {
        Lives = Lives - 1;
    }
}