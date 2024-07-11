using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NewControl : MonoBehaviour

{
    private static NewControl _instance;
    // public float[] floatArray = new float[] { 0.1f, 0.02f, 0.06f};
    private int current = 0;
    public float vec_main;

    
    public static NewControl Instance
    {
        get
        {
            return _instance;
        }
    }
    public int score = 0;

    public int length = 0;

    public Text msgText;

    public Text scoreText;

    public Text lengthText;
    
    public Text SpeedText;

    private Color tempColor;

    public bool hasBorder = true;

    public Image bgImage;
    // Start is called before the first frame update

    // 限制分数，长度越长，普通方式获取的分数越高，奖励获取的概率越低
    private int basic = 0;

    private bool isPause = false;

    public Image pauseButton;
    public Sprite[] pauseSprite;
    
    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if (score < 10000)
        {
            switch (score / 1000)
            {
                case 1:
                    ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 1";
                    break;
                case 2:
                    ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 2";
                    break;
                case 3:
                    ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 3";
                    break;
                case 4:
                    ColorUtility.TryParseHtmlString("#FCEDFAFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 4";
                    break;
                case 5:
                    ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 5";
                    break;
                case 6:
                    ColorUtility.TryParseHtmlString("#CAEEDFEF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 6";
                    break;
                case 7:
                    ColorUtility.TryParseHtmlString("EBFFCCFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 7";
                    break;
                case 8:
                    ColorUtility.TryParseHtmlString("#FCEDFAFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 8";
                    break;
                case 9:
                    ColorUtility.TryParseHtmlString("#FCEDFAFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 9";
                    break;
                case 10:
                    ColorUtility.TryParseHtmlString("#FCEDFAFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 10";
                    break;
                case 11:
                    ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 11";
                    break;
                case 12:
                    ColorUtility.TryParseHtmlString("#FCEDFAFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 12";
                    break;
                case 13:
                    ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                    bgImage.color = tempColor;
                    msgText.text = "Phase 13";
                    break;
                
            }
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FCEDFAFF", out tempColor);
            bgImage.color = tempColor;
            msgText.text = "飞升";
        }

    }
    
    
    public void UpdateUI(bool isReward)
    {
        int s = 5;
        int l = 1;
        int buff = Random.Range(-5, 15);
        if (isReward)
        {
            basic += 2;
            score += buff * basic * s;
        }
        else
        {
            basic += 1;
            score += basic *s;
        }

        length += l;
        scoreText.text = "Score:\n" + score;
        lengthText.text = "Length:\n" + length;
    }

    public void meet_boss()
    {
        score = score / 2;
        scoreText.text = "Score:\n" + score;
    }
    void Start()
    {
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    

    public void ChangeSpeed()
    {
        float[] floatArray = new float[] { 0.1f, 0.08f, 0.045f, 0.4f};
        string[] MySpeed = { "Speed: Medium", "Speed: Fast" , "Speed: Blazing!!", "Speed: Slow"};
        
        current++;
        current %= floatArray.Length;
        vec_main = floatArray[current];
    
        // 取消之前的调用
        NewBehaviourScript.Instance.CancelInvoke("Move");
    
        // 重新调用 InvokeRepeating，使用新的速度值
        NewBehaviourScript.Instance.InvokeRepeating("Move", 0, vec_main);
    
        // 添加调试输出
        Debug.Log("Changed speed to: " + vec_main);
        
        Debug.Log(current);
    
        SpeedText.text = MySpeed[current % MySpeed.Length];
    }
    




}
