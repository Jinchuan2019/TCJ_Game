using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectLoader : MonoBehaviour
{
    public static SceneObjectLoader instance = null;
    public Dictionary<string, List<SaveData>> saveDatas;
    public Dictionary<string, bool> isFirstLoad;
    public GameObject bagManager;
    public GameObject canvas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(bagManager);
            DontDestroyOnLoad(canvas);
            isFirstLoad = new Dictionary<string, bool>();
            saveDatas = new Dictionary<string, List<SaveData>>();

            isFirstLoad.Add("Level1", false);
            isFirstLoad.Add("Level2", false);
            isFirstLoad.Add("Level3", false);
            //
            GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Item"));
            go.transform.position = new Vector3(18.0f, -3.0f);

            GameObject player = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Character/Player"));
            player.transform.position = new Vector3(-20.0f, -10.0f);

            DontDestroyOnLoad(player);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public IEnumerator LoadScene(string sceneName)
    {
        if (!isFirstLoad.ContainsKey(sceneName))
        {
            ;
        }
        else
        {

            //save Old scene
            SceneObjectSaver objectSaver = GameObject.Find("objectSaver")?.GetComponent<SceneObjectSaver>();
            if (objectSaver != null)
            {
                Scene scene = SceneManager.GetActiveScene();
                saveDatas.Remove(scene.name);
                saveDatas.Add(scene.name, objectSaver.SaveScene());
            }
            //Load New scene base
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            if (!isFirstLoad[sceneName])
            {
                //Load New scene saved objects
                if (saveDatas.ContainsKey(sceneName))
                {
                    foreach (var saveData in saveDatas[sceneName])
                    {
                        if (saveData.GetType().Name == "SaveCharacter")
                        {
                            //SaveCharacter saveCharacter = (SaveCharacter) saveData;

                            //var go = Instantiate<CharacterController>(Resources.Load<CharacterController>("Prefabs/Character/" + saveCharacter.prefabName));
                            //go.transform.position = saveCharacter.position;
                            //go.SetKey(saveCharacter.key);
                            //go.SetOpenDoor(saveCharacter.isOpenTheDoor);
                        }
                        else
                        {
                            GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/" + saveData.prefabName));
                            go.transform.position = saveData.position;
                        }    

                    }

                }
            }
            else
            {
                SaveData ItemData = new SaveData();
                ItemData.prefabName = "Item";
                ItemData.position = Vector3.zero;
                saveDatas.Add(sceneName, new List<SaveData>() { ItemData });
                isFirstLoad[sceneName] = false;
                foreach (SaveData saveData in saveDatas[sceneName])
                {
                    GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/" + saveData.prefabName));
                    go.transform.position = saveData.position;
                }
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            StartCoroutine(LoadScene("Level2"));
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            StartCoroutine(LoadScene("Level1"));
        }
        
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Item"));
            go.transform.position = Vector3.zero;
        }
    }
}