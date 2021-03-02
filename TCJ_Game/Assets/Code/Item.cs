using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ISaveable
{
    bool selected = false;
    Camera camera = null;
    public string prefabName;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        selected = true;
    }
    private void OnMouseDrag()
    {
        if (selected)
        {
            var p = Input.mousePosition;
            p = camera.ScreenToWorldPoint(p);
            p.z = transform.position.z;
            this.transform.position = p;
        }
    }
    private void OnMouseUp()
    {
        selected = false;
    }

    public SaveData Save()
    {
        SaveData saveData = new SaveData();
        saveData.prefabName = prefabName;
        saveData.position = transform.position;
        return saveData;
    }
}