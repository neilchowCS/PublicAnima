using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows;


public class ApiConnection : MonoBehaviour
{
    public static ApiConnection Singleton {  get; private set; }
    public DataTransfer dataTransfer;
    private string ip = "10.250.221.199";
    //private string ip = "10.0.0.17";
    private string jwt = "";
    // Start is called before the first frame update
    void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator Login(byte[] bodyRaw)
    {
        string uri = $"http://{ip}:5068/api/AnimaAuthentication";
        
        using (UnityWebRequest www = UnityWebRequest.Post(uri, "POST"))
        {
            www.certificateHandler = new ForceAcceptAll();
            //www.method = "POST";
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else //REQUEST SUCCESS
            {

                jwt = www.downloadHandler.text;
                Debug.Log("Response: " + jwt);

                //this.LoadBattleSceneAsync();
            }
            www.Dispose();
        }
    }

    public IEnumerator Upload(byte[] bodyRaw)
    {
        string uri = $"http://{ip}:5068/api/Anima/join";
        
        using (UnityWebRequest www = UnityWebRequest.Post(uri, $""))
        {
            www.certificateHandler = new ForceAcceptAll();
            www.SetRequestHeader("Authorization", "Bearer " + jwt);
            //www.method = "POST";
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else //REQUEST SUCCESS
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                dataTransfer.Response = JsonUtility.FromJson<ClientBattleResponse>(www.downloadHandler.text);
                this.LoadBattleSceneAsync();
            }
            www.Dispose();
        }
    }

    public IEnumerator pushInputs(byte[] bodyRaw)
    {
        string uri = $"http://{ip}:5068/api/AnimaBattle";
        
        using (UnityWebRequest www = UnityWebRequest.Post(uri, "POST"))
        {
            www.certificateHandler = new ForceAcceptAll();
            www.SetRequestHeader("Authorization", "Bearer " + jwt);
            //www.method = "POST";
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else //REQUEST SUCCESS
            {
                Debug.Log("Response: " + www.downloadHandler.text);
            }
            www.Dispose();
        }
    }

    public IEnumerator requestResult()
    {
        string uri = $"http://{ip}:5068/api/AnimaBattle/result";
        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            www.certificateHandler = new ForceAcceptAll();
            www.SetRequestHeader("Authorization", "Bearer " + jwt);
            //www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else //REQUEST SUCCESS
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                if (www.downloadHandler.text != "waiting for opponent")
                {
                    BattleManager.Singleton.TestBattleResult(JsonConvert.DeserializeObject<ClientBattleResponse>(www.downloadHandler.text));
                }
            }
            www.Dispose();
        }
    }

}

public class ForceAcceptAll : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}

[Serializable]
public class LoginModel
{
    public string username;
    public string password;

    public LoginModel(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}