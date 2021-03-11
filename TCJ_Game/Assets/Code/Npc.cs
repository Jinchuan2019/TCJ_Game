using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Npc : MonoBehaviour
{
    public Transform BackGround;
    public Transform NPCCharacter;
    private TalkSystem TalkSystem;
    public int nextTalk;

    public string Name;
    [TextArea(5, 10)]
    public string[] sentences;
    void Start()
    {
        TalkSystem = FindObjectOfType<TalkSystem>();

    }


    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 65;
        BackGround.position = Pos; 
    }
    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<Npc>().enabled = true;
        FindObjectOfType<TalkSystem>().EnterRangeOfNPC();
        if((other.gameObject.tag=="Player")&&Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<Npc>().enabled = true;
            TalkSystem.Names = Name;
            TalkSystem.talkLines = sentences;
            TalkSystem.nextTalk = nextTalk;
            var player = other.gameObject.GetComponent<ObjectCollet>();
            TalkSystem.key = player.Key;
            FindObjectOfType<TalkSystem>().NPCName();
            
        }


    }
    public void OnTriggerExit(Collider other)
    {
        FindObjectOfType<TalkSystem>().OutOfRange();
        this.gameObject.GetComponent<Npc>().enabled = false; 
    }

}

