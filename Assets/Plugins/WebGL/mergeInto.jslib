var func = {
    //============================= SDK相关开始 =============================
    //获取用户信息
    ToonGetUserData: function () {
        getUserData();
    },

    //获取用户Id
    ToonGetUserId: function () {
        getUserId();
    },

    //加载广告
    ToonLoadAds: function () {
        loadRewardAD();
    },

    //展示广告
    ToonShowAds: function () {
        playRewardAD();
    },
    //============================= SDK相关结束 =============================
};

mergeInto(LibraryManager.library, func);