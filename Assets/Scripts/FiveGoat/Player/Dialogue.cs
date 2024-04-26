using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject messageObj;
    [SerializeField] private TMP_Text message;
    [SerializeField] private string[] allDialogue;
    [SerializeField] private float wordSpeed;
    private int index;
    private bool run;

    void Start()
    {
        run = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnMessage()&&Input.GetKeyDown(KeyCode.E))
        {
            if (messageObj.activeInHierarchy)
            {
                EmtyText();
            }
            else
            {
                messageObj.SetActive(true);
                StartCoroutine(Typing());
                run = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Return)) 
        { 
        NextLine();
        }
        
    }


    public void NextLine()
    {
        if (index<allDialogue.Length-1)
        {
            index++;
            message.SetText("");
            StartCoroutine (Typing());
        }
        else
        {
            EmtyText();
        }
    }
    private void EmtyText()
    {
        message.SetText("");
        index = 0;
        messageObj.SetActive(false);

    }

    private bool IsOnMessage()
    {
        return transform.GetComponent<PlayerManager>().GetShowMessage();
    }
    IEnumerator Typing()
    {
        foreach(char c in allDialogue[index].ToCharArray())
        {
            message.text += c;
            yield return new WaitForSeconds(wordSpeed);
        }
        Debug.Log("Dang viet");
    }
}
