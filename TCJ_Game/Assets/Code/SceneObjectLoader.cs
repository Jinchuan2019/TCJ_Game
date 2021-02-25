using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectLoader : MonoBehaviour
{
    public static SceneObjectLoader instance = null;
    public Dictionary<string, List<SaveData>> saveDatas;
    public Dictionary<string, bool> isFirstLoad;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            isFirstLoad = new Dictionary<string, bool>();
            saveDatas = new Dictionary<string, List<SaveData>>();
            isFirstLoad.Add("TalkSystem", false);
            isFirstLoad.Add("scene1",false);      
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
                    foreach (SaveData saveData in saveDatas[sceneName])
                    {
                        GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/" + saveData.prefabName));
                        go.transform.position = saveData.position;
                    }
                }
            }
            else
            {
                SaveData ItemData = new SaveData();
                ItemData.prefabName = "Box";
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
            StartCoroutine(LoadScene("scene1"));
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            StartCoroutine(LoadScene("TalkSystem"));
        }
        
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Box"));
            go.transform.position = Vector3.zero;
        }
    }
}