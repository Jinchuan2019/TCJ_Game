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

    private PlayerController Player;

    public string Name;
    [TextArea(5, 10)]
    public string[] sentences;

    public int firstTalkLine;
    void Start()
    {
        TalkSystem = FindObjectOfType<TalkSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 65;
        //BackGround.position = Pos;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        this.gameObject.GetComponent<Npc>().enabled = true;
        FindObjectOfType<TalkSystem>().EnterRangeOfNPC();
        if ((other.gameObject.tag == "Player"))
        {
            this.gameObject.GetComponent<Npc>().enabled = true;
            TalkSystem.Names = Name;
            TalkSystem.talkLines = sentences;
            TalkSystem.firstTalkLine = firstTalkLine;

            FindObjectOfType<TalkSystem>().NPCName();

            //set Player
            Player = other.gameObject.GetComponent<PlayerController>();
            //set character
            TalkSystem.SetNPC(this);
        }

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        FindObjectOfType<TalkSystem>().OutOfRange();
        this.gameObject.GetComponent<Npc>().enabled = false;
    }

}

