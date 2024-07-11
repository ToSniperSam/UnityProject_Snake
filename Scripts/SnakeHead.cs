using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    public List<Transform> bodyList = new List<Transform>();
    public int steps;
    public float vec;


    private float vec_boss = 5f;
    private float vec_enemy = 13f;
    public AudioClip eat_Clip;
    public AudioClip die_clip;
    public AudioClip bos_clip;
    private int x;
    private int y;
    private Vector3 headPos;
    public GameObject dieEffect;


    private Transform canvas;
    // Start is called before the first frame update

    private float passedTime; // default 0
    public float targetTime = 1f;
    
    
    public GameObject bodyPrefab;
    public Sprite[] bodySprites = new Sprite[2];
    private bool isDie = false;
    public bool isExist = false;
    public bool is_Boss_exist = false;
    private static NewBehaviourScript _instance;


    void Start()
    {
        vec = 0.1f;
        InvokeRepeating("Move", 0, vec);
        InvokeRepeating("Create_Boss", 7, vec_boss);
        InvokeRepeating("Create_enemy", 15, vec_enemy);
        x = 0;
        y = steps;
    }

    public static NewBehaviourScript Instance
    {
        get
        {
            return _instance;
        }
    }
    
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && y!= -steps && isDie ==false)
        {
            // 上 14
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            x = 0;
            y = steps;
        }
        
        if (Input.GetKey(KeyCode.S) && y!= steps&& isDie ==false)
        {
            // 下 14
            gameObject.transform.localRotation = Quaternion.Euler(0,0,180);
            x = 0;
            y = -steps;
        }
        
        if (Input.GetKey(KeyCode.A) && x!= steps&& isDie ==false)
        {
            // 左 14
            gameObject.transform.localRotation = Quaternion.Euler(0,0,90);
            x = -steps;
            y = 0;
        }
        
        if (Input.GetKey(KeyCode.D) && x!= -steps&& isDie ==false)
        {
            // 右 25
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            x = steps;
            y = 0;
        }
        
    }

    void Create_enemy()
    {
        FoodMaker.Instance.MakeEnemy(isExist);
        isExist = !isExist;
    }

    void Create_Boss()
    {
        FoodMaker.Instance.MakeBoss(is_Boss_exist);
        is_Boss_exist = !is_Boss_exist;
    }
    
    void Move()
    {
        headPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);
        if (bodyList.Count > 0)
        {
            bodyList.Last().localPosition = new Vector3(headPos.x-620,headPos.y,headPos.z);
            bodyList.Insert(0,bodyList.Last());
            bodyList.RemoveAt(bodyList.Count -1);
        }
    }


    void Grow()
    {
        AudioSource.PlayClipAtPoint(eat_Clip,Vector3.zero);
        int Index = (bodyList.Count % 2 == 0) ? 0 : 1;
        GameObject body = Instantiate(bodyPrefab, new Vector3(2000,2000,0),Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprites[Index];
        body.transform.SetParent(canvas, false);
        bodyList.Add(body.transform);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(die_clip,Vector3.zero);
        CancelInvoke();
        isDie = true;
        Instantiate(dieEffect);
        PlayerPrefs.SetInt("Last_Length",NewControl.Instance.length);
        PlayerPrefs.SetInt("Last_Score", NewControl.Instance.score);
        if (PlayerPrefs.GetInt("Best_Score",0)<NewControl.Instance.score)
        {
            PlayerPrefs.SetInt("Best_Length",NewControl.Instance.length);
            PlayerPrefs.SetInt("Best_Score", NewControl.Instance.score);
        }
        StartCoroutine(GameOver(1.5f));
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            NewControl.Instance.UpdateUI(false);
            Grow();
            Random rand = new Random();
            if (rand.Next(100) < 24)
            {
                FoodMaker.Instance.MakeFood(true);
            }
            else
            {
                FoodMaker.Instance.MakeFood(false);
            }
            
        }
        else if (collision.gameObject.CompareTag("Reward"))
        {
            Destroy(collision.gameObject);
            NewControl.Instance.UpdateUI(true);
            Grow();
            Random luck = new Random();
            if (luck.Next(10) == 2)
            {
                FoodMaker.Instance.MakeFood(false);
            }
            else if (luck.Next(10) < 4)
            { 
                Create_enemy();
            }
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(bos_clip,Vector3.zero);
            isExist = !isExist;
            Destroy(collision.gameObject);
            NewControl.Instance.meet_boss();
        }
        else if (collision.gameObject.CompareTag("Enemy_Boss"))
        {
            Die();
        }
        else
        {
                switch (collision.gameObject.name)
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 40,
                            transform.localPosition.z);
                        break;
                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 40,
                            transform.localPosition.z);     
                        break;
                
                    case "Left":
                        Die();
                        break;
                    case "Right":
                        Die();
                        break;
            }

        }
    }
    
    
}
