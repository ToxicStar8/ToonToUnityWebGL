// **************************************************************
// Script Name: 
// Author: WangYS
// Time : 2022/10/09 17:30:45
// Des: 描述
// **************************************************************

using UnityEngine;

public class ToonSDKTest : MonoBehaviour
{
    public ToonSDK Toon;

    public void FnGetUserData()
    {
        Toon.GetUserData((jsonData) => { Debug.Log(jsonData); }, null);
    }

    public void FnGetUserId()
    {
        Toon.GetUserId((jsonData) => { Debug.Log(jsonData); }, null);
    }

    public void FnLoadAds()
    {
        Toon.LoadAds(null, null);
    }

    public void FnShowAds()
    {
        Toon.ShowAds(null, null);
    }

    public void FnLoadAndShow()
    {
        Toon.LoadAndShowRewardAds(null, null);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FnGetUserData();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            FnLoadAds();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            FnShowAds();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            FnLoadAndShow();
        }
    }
}
