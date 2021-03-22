using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : SceneLoad
{
    const int P_X_N = 4;
    const int P_Y_N = 4;
    const int ALL_P_N = P_X_N * P_Y_N;
    int P_X_Size;
    int P_Y_Size;
    int[] Panel = new int[ALL_P_N];

    enum EState
    {
        TITLE, MAIN, CLEAR,
    }
    EState State = EState.TITLE;
    Sprite[] sprites;
    GameObject[] PanelObj;
    Vector2[] BasePos;
    GameObject clickGameObject;

    public Text NumberTtext;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        sprites = Resources.LoadAll<Sprite>("img");
        GameObject obj = new GameObject();
        PanelObj = new GameObject[sprites.Length];
        BasePos = new Vector2[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            PanelObj[i] = Instantiate(obj, new Vector3(-7.2f + 4.8f * (i % 4), 4.05f - 2.7f * (i / 4), 0.0f), Quaternion.identity) as GameObject;
            SpriteRenderer sr = PanelObj[i].AddComponent<SpriteRenderer>();
            sr.sprite = sprites[i];
            PanelObj[i].AddComponent<BoxCollider2D>();
            PanelObj[i].name = i.ToString();
            PanelObj[i].tag = "Panel";
            BasePos[i].x = PanelObj[i].transform.position.x;
            BasePos[i].y = PanelObj[i].transform.position.y;
        }
        Destroy(obj);
        P_X_Size = Screen.width / P_X_N;
        P_Y_Size = Screen.height / P_Y_N;
    }
    void Change(int x, int y)
    {
        int p1 = y * P_X_N + x;
        int p2 = -1;
        if (x > 0 && Panel[p1 - 1] == ALL_P_N - 1)
        {
            p2 = p1 - 1;
        }
        if (x < P_X_N - 1 && Panel[p1 + 1] == ALL_P_N - 1)
        {
            p2 = p1 + 1;
        }
        if (y > 0 && Panel[p1 - P_Y_N] == ALL_P_N - 1)
        {
            p2 = p1 - P_Y_N;
        }
        if (y < P_Y_N - 1 && Panel[p1 + P_Y_N] == ALL_P_N - 1)
        {
            p2 = p1 + P_Y_N;
        }
        if (p2 != -1)
        {
            Panel[p2] = Panel[p1];
            Panel[p1] = ALL_P_N - 1;
        }

        for (int i = 0; i < ALL_P_N; i++)
        {
            if (Panel[i] < ALL_P_N - 1)
            {
                PanelObj[Panel[i]].transform.position = BasePos[i];
                PanelObj[Panel[i]].name = i.ToString();
            }
            else
            {
                Vector2 temp = PanelObj[Panel[i]].transform.position;
                temp.y = 10.0f;
                PanelObj[Panel[i]].transform.position = temp;
                PanelObj[Panel[i]].name = i.ToString();
            }
        }
    }

    void GameTitle()
    {
        NumberTtext.text = "クリックしたら、スタート";
        if (Input.GetMouseButtonDown(0))
        {
            NumberTtext.text = "";
            for (int i = 0; i < ALL_P_N; i++)
            {
                Panel[i] = i;
            }
            for (int i = 0; i < ALL_P_N * 1000; i++)
            {
                Change(Random.Range(0, P_X_N), Random.Range(0, P_Y_N));
            }
            State = EState.MAIN;
        }

    }

    void GameMain()
    {
        P_X_Size = Screen.width / P_X_N;
        P_Y_Size = Screen.height / P_Y_N;
        if (Input.GetMouseButtonDown(0))
        {
            clickGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            int x = 0;
            int y = 0;
            if (hit2D)
            {
                clickGameObject = hit2D.transform.gameObject;
                x = int.Parse(clickGameObject.name) % P_X_N;
                y = int.Parse(clickGameObject.name) / P_Y_N;
            }

            Change(x, y);
            bool clear = true;
            for (int i = 0; i < ALL_P_N; i++)
            {
                if (Panel[i] != i)
                {
                    clear = false;
                }
            }
            if (clear)
            {
                for (int i = 0; i < ALL_P_N; i++)
                {
                    PanelObj[i].transform.position = BasePos[i];
                    State = EState.CLEAR;
                }
            }
        }
    }
    void GameClear()
    {
        NumberTtext.text = "ロック解除";
        sceneObjectLoader.NextScene(sceneToLoad);
    }
    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case EState.TITLE: GameTitle(); break;
            case EState.MAIN: GameMain(); break;
            case EState.CLEAR: GameClear(); break;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(State);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            GameClear();
        }
    }
}