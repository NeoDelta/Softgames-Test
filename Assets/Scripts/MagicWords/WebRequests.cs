using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequests
{
    public static IEnumerator GetJSON(string uri, System.Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                callback(webRequest.downloadHandler.text);
            }
        }
    }

    public static IEnumerator GetTexture(string uri, System.Action<Texture> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                try
                {
                    Texture myTexture = DownloadHandlerTexture.GetContent(webRequest);

                    callback(myTexture);
                }
                catch(Exception e)
                {
                    Debug.Log("Failed retrieving texture from: " + uri);
                }
               
            }
        }
    }
}
