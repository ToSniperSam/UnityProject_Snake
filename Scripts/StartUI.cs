using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public Text LastText;

    public Text bestText;
    

    private void Awake()
    {
        LastText.text = "Last: " + PlayerPrefs.GetInt("Last_Score", 0);
        bestText.text = "Best: " + PlayerPrefs.GetInt("Best_Score", 0);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
