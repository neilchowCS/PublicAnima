using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

public class InputPusher : MonoBehaviour
{
    private float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 2)
        {
            timer += Time.deltaTime;
        }
    }

    public void Push()
    {
        if (timer > 1)
        {
            string json = JsonConvert.SerializeObject(BattleManager.Singleton.history[^1]);
            string escapedInput = "\"" + json.Replace("\"", "\\\"") + "\"";
            Debug.Log(json);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(escapedInput);
            StartCoroutine(ApiConnection.Singleton.pushInputs(bodyRaw));
            timer = 0;
        }
    }
}
