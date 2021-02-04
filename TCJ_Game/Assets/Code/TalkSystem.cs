using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSystem : MonoBehaviour
{
    public Text nameText;
    public Text TalkText;

    public GameObject TalkGui;
    public Transform TalkBoxGui;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public KeyCode TalkInput = KeyCode.F;

    public string Names;
    public string[] talkLines;
    public bool letterIsMUltiplied = false;
    public bool talkActive = false;
    public bool talkEnded = false;
    public bool outOfRange = true;
   
    void Start()
    {
        TalkText.text = "";
    }
   
    void Update()
    {
        
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        TalkGui.SetActive(true);
        if (talkActive==true) {
            {
                TalkGui.SetActive(false);
            }
        }
    }
    public void NPCName()
    {
        outOfRange = false;
        TalkBoxGui.gameObject.SetActive(true);
        nameText.text = Names;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!talkActive)
            {
                talkActive = true;
                StartCoroutine(StartTalk());
            }
        }
        StartTalk();
    }
    private IEnumerator StartTalk()
    {
        if (outOfRange == false)
        {
            int talkLength = talkLines.Length;
            int currentTalkIndex = 0;

            while (currentTalkIndex < talkLength || !letterIsMUltiplied)
            {
                if (!letterIsMUltiplied)
                {
                    letterIsMUltiplied = true;
                    StartCoroutine(DisplayString(talkLines[currentTalkIndex++]));
                    if (currentTalkIndex >= talkLength)
                    {
                        talkEnded = true;
                    }
                }
                yield return 0;
            }
            while (true)
            {
                if (Input.GetKeyDown(TalkInput) && talkEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            talkEnded = false;
            talkActive = false;
            DropTalk();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;
            TalkText.text = "";
            while (currentCharacterIndex < stringLength)
            {
                TalkText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;
                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(TalkInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);
                    }
                }
                else
                {
                    talkEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(TalkInput))
                {
                    break;
                }
                yield return 0;
            }
            talkEnded = false;
            letterIsMUltiplied = false;
            TalkText.text = "";
        }
    }
    public void DropTalk()
    {
        TalkGui.SetActive(false);
        TalkBoxGui.gameObject.SetActive(false);
    }
    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMUltiplied = false;
            talkActive = false;
            StopAllCoroutines();
            TalkGui.SetActive(false);
            TalkBoxGui.gameObject.SetActive(false);
        }
    }
}
