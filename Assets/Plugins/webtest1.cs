using Gpm.WebView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gpm.WebView;

public class webtest1 : MonoBehaviour
{

// FullScreen
public void ShowUrlFullScreen()
{
    GpmWebView.ShowUrl(
        "https://upskillmafia.com/mern/tasks",
        new GpmWebViewRequest.Configuration()
        {
            style = GpmWebViewStyle.FULLSCREEN,
            orientation = GpmOrientation.UNSPECIFIED,
            isClearCookie = true,
            isClearCache = true,
            backgroundColor = "#FFFFFF",
            isNavigationBarVisible = true,
            navigationBarColor = "#4B96E6",
            title = "Task Section.",
            //isBackButtonVisible = true,
            //isForwardButtonVisible = true,
            isCloseButtonVisible = true,
            supportMultipleWindows = true,
#if UNITY_IOS
            contentMode = GpmWebViewContentMode.MOBILE
#endif
        },
        // See the end of the code example
        OnCallback,
        new List<string>()
        {
            "USER_ CUSTOM_SCHEME"
        });
}

// Popup default
public void ShowUrlPopupDefault()
{
    GpmWebView.ShowUrl(
        "https://upskillmafia.com/mern/tasks",
        new GpmWebViewRequest.Configuration()
        {
            style = GpmWebViewStyle.POPUP,
            orientation = GpmOrientation.UNSPECIFIED,
            isClearCookie = true,
            isClearCache = true,
            isNavigationBarVisible = true,
            isCloseButtonVisible = true,
            supportMultipleWindows = true,
#if UNITY_IOS
            contentMode = GpmWebViewContentMode.MOBILE,
            isMaskViewVisible = true,
#endif
        },
        // See the end of the code example
        OnCallback,
        new List<string>()
        {
            "USER_ CUSTOM_SCHEME"
        });
}

// Popup custom position and size
public void ShowUrlPopupPositionSize()
{
    GpmWebView.ShowUrl(
        "https://upskillmafia.com/mern/tasks",
        new GpmWebViewRequest.Configuration()
        {
            style = GpmWebViewStyle.POPUP,
            orientation = GpmOrientation.UNSPECIFIED,
            isClearCookie = true,
            isClearCache = true,
            isNavigationBarVisible = true,
            isCloseButtonVisible = true,
            position = new GpmWebViewRequest.Position
            {
                hasValue = true,
                x = (int)(Screen.width * 0.1f),
                y = (int)(Screen.height * 0.1f)
            },
            size = new GpmWebViewRequest.Size
            {
                hasValue = true,
                width = (int)(Screen.width * 0.8f),
                height = (int)(Screen.height * 0.8f)
            },
            supportMultipleWindows = true,
#if UNITY_IOS
            contentMode = GpmWebViewContentMode.MOBILE,
            isMaskViewVisible = true,
#endif
        }, null, null);
}

// Popup custom margins
public void ShowUrlPopupMargins()
{
    GpmWebView.ShowUrl(
        "https://upskillmafia.com/mern/tasks",
        new GpmWebViewRequest.Configuration()
        {
            style = GpmWebViewStyle.POPUP,
            orientation = GpmOrientation.UNSPECIFIED,
            isClearCookie = true,
            isClearCache = true,
            isNavigationBarVisible = true,
            isCloseButtonVisible = true,
            margins = new GpmWebViewRequest.Margins
            {
                hasValue = true,
                left = (int)(Screen.width * 0.1f),
                top = (int)(Screen.height * 0.1f),
                right = (int)(Screen.width * 0.1f),
                bottom = (int)(Screen.height * 0.1f)
            },
            supportMultipleWindows = true,
#if UNITY_IOS
            contentMode = GpmWebViewContentMode.MOBILE,
            isMaskViewVisible = true,
#endif
        }, null, null);
}

private void OnCallback(
    GpmWebViewCallback.CallbackType callbackType,
    string data,
    GpmWebViewError error)
{
    Debug.Log("OnCallback: " + callbackType);
    switch (callbackType)
    {
        case GpmWebViewCallback.CallbackType.Open:
            if (error != null)
            {
                Debug.LogFormat("Fail to open WebView. Error:{0}", error);
            }
            break;
        case GpmWebViewCallback.CallbackType.Close:
            if (error != null)
            {
                Debug.LogFormat("Fail to close WebView. Error:{0}", error);
            }
            break;
        case GpmWebViewCallback.CallbackType.PageStarted:
            if (string.IsNullOrEmpty(data) == false)
            {
                Debug.LogFormat("PageStarted Url : {0}", data);
            }
            break;
        case GpmWebViewCallback.CallbackType.PageLoad:
            if (string.IsNullOrEmpty(data) == false)
            {
                Debug.LogFormat("Loaded Page:{0}", data);
            }
            break;
        case GpmWebViewCallback.CallbackType.MultiWindowOpen:
            Debug.Log("MultiWindowOpen");
            break;
        case GpmWebViewCallback.CallbackType.MultiWindowClose:
            Debug.Log("MultiWindowClose");
            break;
        case GpmWebViewCallback.CallbackType.Scheme:
            if (error == null)
            {
                if (data.Equals("USER_ CUSTOM_SCHEME") == true || data.Contains("CUSTOM_SCHEME") == true)
                {
                    Debug.Log(string.Format("scheme:{0}", data));
                }
            }
            else
            {
                Debug.Log(string.Format("Fail to custom scheme. Error:{0}", error));
            }
            break;
        case GpmWebViewCallback.CallbackType.GoBack:
            Debug.Log("GoBack");
            break;
        case GpmWebViewCallback.CallbackType.GoForward:
            Debug.Log("GoForward");
            break;
        case GpmWebViewCallback.CallbackType.ExecuteJavascript:
            Debug.LogFormat("ExecuteJavascript data : {0}, error : {1}", data, error);
            break;
#if UNITY_ANDROID
        case GpmWebViewCallback.CallbackType.BackButtonClose:
            Debug.Log("BackButtonClose");
            break;
#endif
    }
}
}
