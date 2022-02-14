using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;


[Serializable]
public class WebRequest : MonoBehaviour
{
    public delegate void MyDelegate();
    public static event MyDelegate myWebEvent;

    private string _url = "https://cat-fact.herokuapp.com/facts/random?animal_type=cat,dog,horse&amount=10";

    private char _simvole = '"';


    [Obsolete]
    void Start()
    {
        StartCoroutine(LoadTextFromServer(_url, ActionWriter));
    }


    [Obsolete]
    IEnumerator LoadTextFromServer(string url, Action<string> response)
    {
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            response(request.downloadHandler.text);
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

            response(null);
        }

        request.Dispose();

    }


    public void ActionWriter(string writeText)
    {
        if (myWebEvent != null)
        {
            SaveItemInfo(writeText);

            myWebEvent.Invoke();
        }

        Debug.Log(writeText);

    }


    #region SaveInfoPath
    public void SaveItemInfo(string ItemInfo)
    {
        string path = null;
#if UNITY_EDITOR
        path = "Assets/Resources/ItemInfo.json";
#endif
#if UNITY_STANDALONE
         // You cannot add a subfolder, at least it does not work for me
         path = "MyGame_Data/Resources/ItemInfo.json"
#endif

        string str = ItemInfo.ToString();

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write("{" + _simvole + "items" + _simvole + ": " + str + " }");
            }
        }
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    #endregion
}
