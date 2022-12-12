// **************************************************************
// Script Name: 
// Author: WangYS
// Time : 2022/10/09 10:48:21
// Des: 描述
// **************************************************************
//#if UNITY_WebGL
using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ToonSDK : MonoBehaviour
{
    public static ToonSDK Instance { private set; get; }

    /// <summary>
    /// 已加载的广告数量
    /// </summary>
    private int _loadedRewardCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            //预加载三个
            ToonLoadAds();
        }
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    [DllImport("__Internal")]
    private static extern void ToonGetUserData();
    /// <summary>
    /// 获取用户信息的回调
    /// </summary>
    private Action<string> _getUserDataSuccessCallback;
    private Action _getUserDataFailedCallback;
    /// <summary>
    /// 获取用户信息
    /// </summary>
    public void GetUserData(Action<string> successCallback, Action failedCallback)
    {
        _getUserDataSuccessCallback = successCallback;
        _getUserDataFailedCallback = failedCallback;
        ToonGetUserData();
    }
    /// <summary>
    /// 获取用户信息JS执行回调  ""=失败 jsonData=成功
    /// </summary>
    [MonoPInvokeCallback(typeof(string))]
    private void GetUserDataCallback(string jsonData)
    {
        if (jsonData == "")
        {
            _getUserDataFailedCallback?.Invoke();
        }
        else
        {
            _getUserDataSuccessCallback?.Invoke(jsonData);
        }
        _getUserDataFailedCallback = null;
        _getUserDataSuccessCallback = null;
    }

    /// <summary>
    /// 获取用户Id
    /// </summary>
    [DllImport("__Internal")]
    private static extern void ToonGetUserId();
    /// <summary>
    /// 获取用户Id的回调
    /// </summary>
    private Action<long> _getUserIdSuccessCallback;
    private Action _getUserIdFailedCallback;
    /// <summary>
    /// 获取用户Id
    /// </summary>
    public void GetUserId(Action<long> successCallback, Action failedCallback)
    {
        _getUserIdSuccessCallback = successCallback;
        _getUserIdFailedCallback = failedCallback;
        ToonGetUserId();
    }
    /// <summary>
    /// 获取用户Id的JS执行回调  ""=失败 jsonData=成功
    /// </summary>
    [MonoPInvokeCallback(typeof(long))]
    private void GetUserIdCallback(long userId)
    {
        if (userId == 0)
        {
            _getUserIdFailedCallback?.Invoke();
        }
        else
        {
            _getUserIdSuccessCallback?.Invoke(userId);
        }
        _getUserIdSuccessCallback = null;
        _getUserIdFailedCallback = null;
    }



    /// <summary>
    /// 预加载广告
    /// </summary>
    [DllImport("__Internal")]
    private static extern void ToonLoadAds();
    /// <summary>
    /// 预加载广告的回调
    /// </summary>
    private Action _loadSuccessAdsCallback;
    private Action<int> _loadFailedAdsCallback;
    /// <summary>
    /// 预加载广告
    /// </summary>
    public void LoadAds(Action successCallback, Action<int> failedCallback)
    {
        _loadSuccessAdsCallback = successCallback;
        _loadFailedAdsCallback = failedCallback;
        ToonLoadAds();
    }
    /// <summary>
    /// 预加载广告JS执行回调  0=失败 1=成功
    /// </summary>
    [MonoPInvokeCallback(typeof(int))]
    private void LoadAdsCallback(int code)
    {
        if (code == 1)
        {
            _loadedRewardCount++;
            _loadSuccessAdsCallback?.Invoke();
        }
        else
        {
            _loadFailedAdsCallback?.Invoke(code);
        }
        _loadSuccessAdsCallback = null;
        _loadFailedAdsCallback = null;
    }



    /// <summary>
    /// 显示广告 0=失败 1=成功
    /// </summary>
    [DllImport("__Internal")]
    private static extern void ToonShowAds();
    /// <summary>
    /// 显示广告的回调
    /// </summary>
    private Action _showSuccessAdsCallback;
    private Action<int> _showFailedAdsCallback;
    /// <summary>
    /// 显示广告
    /// </summary>
    public void ShowAds(Action successCallback, Action<int> failedCallback)
    {
        _showSuccessAdsCallback = successCallback;
        _showFailedAdsCallback = failedCallback;
        ToonShowAds();
    }

    /// <summary>
    /// 预加载广告JS执行回调
    /// </summary>
    [MonoPInvokeCallback(typeof(int))]
    private void ShowAdsCallback(int code)
    {
        if (code == 1)
        {
            _loadedRewardCount--;
            _showSuccessAdsCallback?.Invoke();
        }
        else
        {
            _showFailedAdsCallback?.Invoke(code);
        }
        _showSuccessAdsCallback = null;
        _showFailedAdsCallback = null;
    }

    /// <summary>
    /// 加载并显示激励广告
    /// </summary>
    /// <param name="completed"></param>
    public void LoadAndShowRewardAds(Action successCallback, Action<int> failedCallback)
    {
        //已经加载了
        if(_loadedRewardCount > 0)
        {
            ShowAds(successCallback, failedCallback);
            //看完再加载一个
            LoadAds(null, null);
        }
        else
        {
            //现场加载并播放
            LoadAds(() =>
            {
                Debug.Log("加载广告成功");
                ShowAds(successCallback, failedCallback);
                //看完再加载一个
                LoadAds(null, null);
            }, failedCallback);
        }
    }



    /// <summary>
    /// Js调用Log
    /// </summary>
    /// <param name="log"></param>
    [MonoPInvokeCallback(typeof(string))]
    private void Log(string log)
    {
        Debug.Log(log);
    }
}
//#endif

