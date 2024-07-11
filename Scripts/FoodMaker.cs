using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FoodMaker : MonoBehaviour
{
    private static FoodMaker _instance;

    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }
    public int left = 16;

    public int right = 5;

    public int up = 14;

    public int down = 14;

    public GameObject foodPrefab;

    public Sprite[] foodSprites;

    public GameObject rewardPrefab;

    public GameObject enemyPrefab_1;
    public GameObject enemyPrefab_2;
    public GameObject enemyPrefab_3;
    public GameObject enemyPrefab_4;

    private Transform foodHolder;
    // Start is called before the first frame update

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        foodHolder = GameObject.Find("FoodHolder").transform;
        MakeFood(false);
    }
    
    public void MakeFood(bool isReward)
    {
        int index = Random.Range(0, foodSprites.Length);
        GameObject food = Instantiate(foodPrefab);
        food.GetComponent<Image>().sprite = foodSprites[index];
        food.transform.SetParent(foodHolder, false);

        int x = Random.Range(-left, right);
        int y = Random.Range(-up, down);
        food.transform.localPosition = new Vector3(x * 30 - 210, y * 30, 0);

        if (isReward)
        {
            GameObject reward = Instantiate(rewardPrefab);
            reward.transform.SetParent(foodHolder, false);
            int rewardX = Random.Range(-left, right);
            int rewardY = Random.Range(-up, down);
            reward.transform.localPosition = new Vector3(rewardX * 30 - 210, rewardY * 30, 0);
        }
    }

    public void MakeBoss(bool isExist)
    {
        if (isExist)
        {
            Destroy(GameObject.FindWithTag("Enemy_Boss"));
        }
        else
        {
            int rewardX = Random.Range(-left+3, right-3);
            int rewardY = Random.Range(-up+3, down-3);
            GameObject enemy4 = Instantiate(enemyPrefab_4);
            enemy4.transform.SetParent(foodHolder, false);
            enemy4.transform.localPosition = new Vector3(rewardX * 30 - 210, rewardY * 30, 0);
        }
    }

    public void MakeEnemy(bool isExist)
    {
        if (isExist)
        {
            Destroy(GameObject.FindWithTag("Enemy"));
        }
        else
        {
            int m = Random.Range(1, 3);
            int rewardX = Random.Range(-left+3, right-3);
            int rewardY = Random.Range(-up+3, down-3);
            if (m == 1)
            {
                GameObject enemy_1 = Instantiate(enemyPrefab_1);
                enemy_1.transform.SetParent(foodHolder, false);
                enemy_1.transform.localPosition = new Vector3(rewardX * 30 - 210, rewardY * 30, 0);

            }
            else if (m == 2)
            {
                GameObject enemy_2 = Instantiate(enemyPrefab_2);
                enemy_2.transform.SetParent(foodHolder, false);
                enemy_2.transform.localPosition = new Vector3(rewardX * 30 - 210, rewardY * 30, 0);
            }
            else if (m == 3)
            {
                GameObject enemy_3 = Instantiate(enemyPrefab_3);
                enemy_3.transform.SetParent(foodHolder, false);
                enemy_3.transform.localPosition = new Vector3(rewardX * 30 - 210, rewardY * 30, 0);
            }
        }
    }

}
