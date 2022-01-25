using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField]
    GameObject LogMessagePrefab;

    [SerializeField]
    GameObject ScrollBoxContent;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TEST");
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log("TEST " + Time.time);
    }

    void OnEnable()
    {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type)
    {
        GameObject newText = GameObject.Instantiate(LogMessagePrefab, ScrollBoxContent.transform);
        newText.GetComponent<Text>().text = message;
    }


}
