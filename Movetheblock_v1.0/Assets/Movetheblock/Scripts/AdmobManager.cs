
//using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
//using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
//using GoogleMobileAds.Api;
using Gley.MobileAds;

public class AdmobManager : MonoBehaviour
{
    

    private void Start()
    {
        API.Initialize(OnInitialized);

    }

    private void OnInitialized()
    {
        API.ShowBanner(BannerPosition.Bottom, BannerType.Adaptive);

        if (!API.GDPRConsentWasSet())
        {
            API.ShowBuiltInConsentPopup(PopupCloseds);
        }
    }

    private void PopupCloseds()
    {

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == false)
        {
           API.ShowAppOpen();
        }
    }
}
