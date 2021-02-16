using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectSaver : MonoBehaviour
{


    public List<SaveData> SaveScene() {

        List<SaveData> res = new List<SaveData>();
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            ISaveable[] childrenInterfaces = rootGameObject.GetComponentsInChildren<ISaveable>();
            foreach (var childInterface in childrenInterfaces)
            {
                res.Add(childInterface.Save());
            }
        }
        return res;
    }
}
public interface ISaveable
{
    SaveData Save();
}

[System.Serializable]
public class SaveData
{
    public string prefabName;
    public Vector3 position;
}