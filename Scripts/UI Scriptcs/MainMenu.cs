using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class MainMenu : MonoBehaviour {
    public Text levelNumber;
    public GameObject pauseMenu;

    public GameObject ShowAds;
    private Button showAdsButton;
    private Button dontShowAds;
    private bool IsReward;
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
    public GameObject gameOver;

    public Text scoreText;
    public Text bestScoreText;

    private AdsManager adsManager;
    private GameController gameController;

	void Awake () {
        isReward = false;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        ShowAds = transform.Find("ShowAds").gameObject;
        showAdsButton = ShowAds.transform.Find("WatchAds").GetComponent<Button>();
        dontShowAds = ShowAds.transform.Find("OrText").GetComponent<Button>();
        adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
    }
    private void Start()
    {
        ShowAds.SetActive(false);
        showAdsButton.onClick.AddListener(OnClickShowAds);
        dontShowAds.onClick.AddListener(OnClickDontShowAds);
    }

    void Update ()
    {
        scoreText.text = gameController.score.ToString();

        if (gameController.score > PlayerPrefs.GetInt("BestScore", 0))
            PlayerPrefs.SetInt("BestScore", gameController.score);
        bestScoreText.text = "Best " + PlayerPrefs.GetInt("BestScore");
	}

    public void TryAgain()
    {
        adsManager.showInterstitialAd();
        Time.timeScale = 1;
        Destroy(GameObject.Find("AdsManager"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }

    private void OnClickShowAds()
    {
        Time.timeScale = 1;
        ShowAds.SetActive(false);
        adsManager.ShowRewardedAd();
    }
    private void OnClickDontShowAds()
    {
        Time.timeScale = 1;
        ShowAds.SetActive(false);
        gameOver.SetActive(true);
        GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn", false);
    }
 
    public void ShopMenu()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
