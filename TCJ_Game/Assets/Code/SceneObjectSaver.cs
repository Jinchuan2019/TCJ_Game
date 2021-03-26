using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectSaver : MonoBehaviour
{


    public void SaveScene(out List<SaveData> nonsaveableData,out List<SaveData> moveableData)
    {

        nonsaveableData = new List<SaveData>();
        moveableData = new List<SaveData>();
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            ISaveable[] childrenInterfaces = rootGameObject.GetComponentsInChildren<ISaveable>();
            foreach (var childInterface in childrenInterfaces)
            {
                var saveData = childInterface.Save();
                if (saveData.moveable)
                {
                    moveableData.Add(saveData);
                }
                else
                {
                    nonsaveableData.Add(saveData);
                }
            }
        }
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
    public bool moveable;
}

public class SaveCharacter : SaveData
{
    public bool key;
    public bool isOpenTheDoor;
}