!(function (A) {
  if (!A.mangatoon) {
    var _callerId = 0, _callbacks = {}, _eventListeners = {};
    function _call(methodName, originParam) {
      originParam || (originParam = {});
      var param = JSON.stringify(originParam);
      if (originParam.success || originParam.fail || originParam.complete || originParam.cancel) {
        _callbacks[_callerId] = {
          success: originParam.success,
          fail: originParam.fail,
          complete: originParam.complete,
          cancel: originParam.cancel
        };
      }
      if (A.AndroidInvoker) {
        A.AndroidInvoker.call(methodName, _callerId + '', encodeURIComponent(param));
      } else if(A.webkit && A.webkit.messageHandlers && A.webkit.messageHandlers.IOSInvoker) {
        A.webkit.messageHandlers.IOSInvoker.postMessage({methodName: methodName, callerId: _callerId, data: encodeURIComponent(param)})
      } else {
        A.location.href = 'mangatoon://jscall?methodName=' + methodName + '&callerId=' + _callerId + '&data=' + encodeURIComponent(param);
      }
      _callerId++;
    };
    A.mangatoon = {
      _jscallback: function (methodName, callbackId, result) {
        console.log('receive jscallback:' + methodName + ',' + callbackId + ',' + JSON.stringify(result));
        if (_callbacks[callbackId]) {
          if (result.status == '1') {
            _callbacks[callbackId].success && _callbacks[callbackId].success(result);
          } else if (result.status == '-1') {
            _callbacks[callbackId].fail && _callbacks[callbackId].fail(result);
          } else if (result.status == '0') {
            _callbacks[callbackId].cancel && _callbacks[callbackId].cancel(result);
          }
          _callbacks[callbackId].complete && _callbacks[callbackId].complete(result);
          delete _callbacks[callbackId];
        }
      },
      _notify: function (eventName, data) {
        console.log('receive notify:' + eventName + ',' + data);
        _eventListeners[eventName] && (_eventListeners[eventName](data));
        eventName == 'back' && !_eventListeners[eventName] && (_call('goBack'));
      },
      registerEventListener: function (param) {
        param && param.eventName && typeof param.listener == 'function' && (_eventListeners[param.eventName] = param.listener);
        if(A.webkit.messageHandlers.IOSInvoker) {
          _call('registerEventListener', param);
        }
      },
      unregisterEventListener: function (param) {
        if (param && param.eventName) {
          if (typeof param.listener == 'function') {
            if (_eventListeners[param.eventName] === param.listener) {
              delete _eventListeners[param.eventName];
            }
          } else {
            delete _eventListeners[param.eventName];
          }
          if(A.webkit.messageHandlers.IOSInvoker) {
            _call('unregisterEventListener', param);
          }
        }
      },
      getDeviceInfo: function (param) {
        _call('getDeviceInfo', param);
      },
      getLocation: function (param) {
        _call('getLocation', param);
      },
      logEvent: function (param) {
        _call('logEvent', param);
      },
      getUserId: function (param) {
        _call('getUserId', param);
      },
      getAccessToken: function (param) {
        _call('getAccessToken', param);
      },
      getProfile: function (param) {
        _call('getProfile', param);
      },
      request: function (param) {
        _call('request', param);
      },
      apiRequest: function (param) {
        _call('apiRequest', param);
      },
      apiPost: function (param) {
        _call('apiPost', param);
      },
      apiGet: function (param) {
        _call('apiGet', param);
      },
      gzipPost: function (param) {
        _call('gzipPost', param);
      },
      rsaPost: function (param) {
        _call('rsaPost', param);
      },
      closeWindow: function (param) {
        _call('closeWindow', param);
      },
      goBack: function (param) {
        _call('goBack', param);
      },
      configNavigationBar: function (param) {
        _call('configNavigationBar', param);
      },
      disableFullScreenBackGesture: function (param) {
        _call('disableFullScreenBackGesture', param);
      },
      openPage: function (param) {
        _call('openPage', param);
      },
      toast: function (param) {
        _call('toast', param);
      },
      centerToast: function (param) {
        _call('centerToast', param);
      },
      prompt: function (param) {
        _call('prompt', param);
      },
      confirm: function (param) {
        _call('confirm', param);
      },
      showLoading: function (param) {
        _call('showLoading', param);
      },
      hideLoading: function (param) {
        _call('hideLoading', param);
      },
      share: function (param) {
        _call('share', param);
      },
      showSharePanel: function (param) {
        _call('showSharePanel', param);
      },
      showImageSharePanel: function (param) {
        _call('showImageSharePanel', param);
      },
      screenshotAndShare: function (param) {
        _call('screenshotAndShare', param);
      },
      chooseAndUploadImage: function (param) {
        _call('chooseAndUploadImage', param);
      },
      downloadImage: function (param) {
        _call('downloadImage', param);
      },
      loadAd: function (param) {
        _call('loadAd', param);
      },
      playAd: function (param) {
        _call('playAd', param);
      },
      playInterAd: function (param) {
        _call('playInterAd', param);
      },
      canPlayInterAd: function (param) {
        _call('canPlayInterAd', param);
      },
      showBannerAd: function (param) {
        _call('showBannerAd', param);
      },
      loadPointConfig: function (param) {
        _call('loadPointConfig', param);
      },
      compareVersion: function(version1, version2)  {
        const arr1 = version1.split('.')
        const arr2 = version2.split('.')
        const length1 = arr1.length
        const length2 = arr2.length
        const minlength = Math.min(length1, length2)
        let i = 0
        for (i ; i < minlength; i++) {
          let a = parseInt(arr1[i])
          let b = parseInt(arr2[i])
          if (a > b) {
            return 1
          } else if (a < b) {
            return -1
          }
        }
        if (length1 > length2) {
          for(let j = i; j < length1; j++) {
            if (parseInt(arr1[j]) !== 0) {
              return 1
            }
          }
          return 0
        } else if (length1 < length2) {
          for(let j = i; j < length2; j++) {
            if (parseInt(arr2[j]) !== 0) {
              return -1
            }
          }
          return 0
        }
        return 0
      }
    };
  }
})(window)
