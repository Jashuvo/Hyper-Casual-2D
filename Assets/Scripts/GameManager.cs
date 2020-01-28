using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GoogleMobileAds.Api;
using System;

public class GameManager : MonoBehaviour
{

    private RewardBasedVideoAd rewardBasedVideo;

    [SerializeField]
    private GameObject watchAdPanel, gameOverPanel;
    [SerializeField]
    TextMeshProUGUI scoreText , bestScoreText;

    public static GameManager instance = null;

    void Start(){
        if(instance == null){
            instance = this;
        }

        #if UNITY_ANDROID
            string appId = "ca-app-pub-3940256099942544~3347511713";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        
        
    }

    public void GameOver(){
       StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine(){
        yield return new WaitForSeconds(0.3f);
        watchAdPanel.SetActive(true);
        
        FollowPlayerScript.instance.source.Stop();
        scoreText.text = ScoreManagerScript.instance.gameScore.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("Best",0).ToString();
        yield break;
    }
    public void RestartGame(){
        FollowPlayerScript.instance.source.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void SkipAd(){
        gameOverPanel.SetActive(true);
        watchAdPanel.SetActive(false);
    }

    public void showVideoAds(){
         #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
        this.rewardBasedVideo.OnAdRewarded += HandleRewardedAdLoaded;

    
    }



 public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }


}
