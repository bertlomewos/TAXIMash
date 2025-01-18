using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gley.MobileAds;
[System.Serializable]
public class HintInfor
{
    public int blockIndex = 0;
    public int numberMove = 0;
}

[System.Serializable]
public class HintContainer
{
    public List<HintInfor> hints = new List<HintInfor>();
}
public class BoosterManager : MonoHandler
{
    public static BoosterManager instance;
    [SerializeField]
    private TextMeshProUGUI hintText;
    [SerializeField]
    private bool testMode;
    private int maxWatchAds = 3;
   


    private int countAds = 0;

    private int hintNumber = 0;

   

    private void Awake()
    {
        instance = this;
        countAds = 0;
        hintNumber = PlayerPrefs.GetInt("Hint", (testMode) ? 300 : 2);
        AddHint(0);
        
    }
    public void OnEventHint()
    {
        if (hintNumber > 0)
        {
            AddHint(-1);
            PlayingManager.instance.HintGame();
          
        }
        else
        {
            if (API.IsRewardedVideoAvailable())
            {
                API.ShowRewardedVideo((s) => {
                    AddHint(2);
                });
            }
            else
            {
                Debug.Log("No ADS");
            }
            
        }
    }

    public void AddHint(int number)
    {
      
        hintNumber += number;
        PlayerPrefs.SetInt("Hint", hintNumber);
        UpdateTextHint();
    }

    private void UpdateTextHint()
    {
        if(hintNumber==0)
        {
            hintText.text = "AD";
        }
        else
        {
            hintText.text = hintNumber.ToString() ;
        }
    }

    public void OnEventUndo()
    {
        if(countAds>=maxWatchAds)
        {
           //if(GoogleMobileAdsScript.instance.CheckRewardBasedVideo())
           // {
           //     GoogleMobileAdsScript.instance.ShowRewardBasedVideo();
           // }
            countAds = 0;
            return;
        }
        PlayingManager.instance.UndoGame();
    }

    public void OnEventReplay()
    {
        API.ShowInterstitial();
        PlayingManager.instance.ReplayGame();
    }


 

}
