using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    private BannerView bannerAd;
    private string bannerId = "ca-app-pub-8967561106308787/6159984328";

    private RewardBasedVideoAd rewardedAd;
    private string rewardedId = "ca-app-pub-8967561106308787/5001981173";

    private InterstitialAd interstitialAd;
    private string interstitialId = "ca-app-pub-8967561106308787/3878888610";

    private GameController gameController;
    private ShopSystem shopSystem;


    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        MobileAds.Initialize(reklamver => { });
        //BannerAds();
        rewardedAd = RewardBasedVideoAd.Instance;
        RequestRewardedAds();
        rewardedAd.OnAdRewarded += Reward;
        rewardedAd.OnAdClosed += VideoClosed;
        InterstitialAd();
        interstitialAd.OnAdClosed += Interstitial_OnAdClosed;
    }

   
    public void BannerAds()
    {
        bannerAd = new BannerView(bannerId,AdSize.SmartBanner,AdPosition.Bottom);
        AdRequest newAd = new AdRequest.Builder().Build();
        bannerAd.LoadAd(newAd);
        
    }
    public void RequestRewardedAds()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request,rewardedId);
      
    }
    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
        else
            Debug.Log("Failed to Rewarded Ad");
    }

    private void InterstitialAd()
    {
        interstitialAd = new InterstitialAd(interstitialId);
        AdRequest newAd = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(newAd);
    }
    public void showInterstitialAd()
    {
        if(interstitialAd.IsLoaded())
        interstitialAd.Show();
    }
   
    private void Reward(object sender,EventArgs e)
    {//shopSystem.isReward == true
        if (SceneManager.GetActiveScene().buildIndex==1)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin",0) + 4);
            shopSystem.coinText.text = PlayerPrefs.GetInt("Coin").ToString();
            shopSystem.isReward = false;
          
        }
        else
        {
             PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                PlayerPrefs.SetInt("BallsCount", PlayerPrefs.GetInt("BallsCount",5) + 2);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }  
           
        
            
            
               
     
       
       
    }
    private void VideoClosed(object sender, EventArgs e)
    {
        RequestRewardedAds();
    }



    private void Update()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            shopSystem = GameObject.Find("CannonsContainer").GetComponent<ShopSystem>();
            if (shopSystem.isBought == true)
            {
                gameController.isBought = true;
            
                shopSystem.isBought = false;
            }
        }
        
    }

    private void Interstitial_OnAdClosed(object sender, EventArgs e)
    {
        InterstitialAd();
    }

}
