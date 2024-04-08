using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;

public class LoginButton : MonoBehaviour
{
    public TMP_InputField user;
    public TMP_InputField pass;

    private Coroutine login;
    private Coroutine api;
    private bool isRunning;
    // Start is called before the first frame update
    void Awake()
    {
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryLogin()
    {
        if (!isRunning)
        {
            isRunning = true;
            string json = $"{{ \"username\": \"{user.text.Trim()}\", \"password\": \"{pass.text.Trim()}\" }}";
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            Debug.Log(json);
            login = StartCoroutine(LoginCoroutine(bodyRaw));
        }
    }

    private IEnumerator LoginCoroutine(byte[] bodyRaw)
    {
        yield return api = StartCoroutine(ApiConnection.Singleton.Login(bodyRaw));
        isRunning = false;
    }

    private void OnDestroy()
    {
        if (login != null)
        {
            StopCoroutine(login);
        }
        if (api != null)
        {
            StopCoroutine(api);
        }
    }
}
