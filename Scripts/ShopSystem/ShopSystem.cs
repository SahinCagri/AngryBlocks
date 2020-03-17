using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject[] cannons;
    private GameObject cannonContainer;
    private GameObject cannon;

    [SerializeField] Button rightButton;
    [SerializeField] Button leftButton;
    [SerializeField] Button UseButton;
    [SerializeField] Button WatchAdsButton;
    [SerializeField] Button returnGameButton;

    private bool IsReward=false;
    private bool IsBought=false;
    public bool isReward
    {
        get
        {
            return IsReward;
        }
        set
        {
            IsReward = value;
        }
    }
    public bool isBought
    {
        get { return IsBought; }
        set { IsBought = value; }
    }
    private AdsManager adsManager;
    public Text coinText;
    private static int cannonIndex;
 
    private float offsetX = 6f;

    private void Start()
    {
        cannonIndex = 1;
        Time.timeScale = 1;
        Debug.Log(cannons[cannonIndex].ToString());
        cannonContainer = GameObject.Find("CannonsContainer");
        for (int i = 0; i < cannons.Length; i++)
        {
           
            cannon = Instantiate(cannons[i], new Vector2(-5.71f + (offsetX * i), 1.15f), Quaternion.identity);
            cannon.transform.SetParent(cannonContainer.transform);
           
        }
        leftButton.onClick.AddListener(MoveRight);
        rightButton.onClick.AddListener(MoveLeft);
        WatchAdsButton.onClick.AddListener(watchRewardedAd);
        UseButton.onClick.AddListener(BuyCannon);
        returnGameButton.onClick.AddListener(ReturnGameScene);
        adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
        Debug.Log(cannons[cannonIndex].ToString());
    }

    private void ReturnGameScene()
    {
        SceneManager.LoadScene(0);
    }

    private void BuyCannon()
    {
        if (PlayerPrefs.GetInt("Coin") >= 3)
        {
            PlayerPrefs.SetInt("cannonIndex", cannonIndex);
            isBought = true;
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 4);
            coinText.text = PlayerPrefs.GetInt("Coin",0).ToString();
        }
       
    }

    private void watchRewardedAd()
    {
        isReward = true;
        adsManager.ShowRewardedAd();
    }


    private void MoveRight()
    {
        iTween.MoveBy(cannonContainer, iTween.Hash(new object[]
        {
            "x",offsetX,
            "easetype",iTween.EaseType.easeOutSine,
            "time",0.3f
        }));
        rightButton.gameObject.SetActive(true);
         cannonIndex--;


    }

    private void MoveLeft()
    {
        iTween.MoveBy(cannonContainer, iTween.Hash(new object[]
       {
            "x",-offsetX,
            "easetype",iTween.EaseType.easeOutSine,
            "time",0.3f
       }));
        leftButton.gameObject.SetActive(true);
         cannonIndex++;

    }

    private void Update()
    {
        if (cannonContainer.transform.position.x == offsetX)
        {
            leftButton.gameObject.SetActive(false);
        }
        else if (cannonContainer.transform.position.x <= -98f)
        {
            rightButton.gameObject.SetActive(false);
        }
        
    }


}
