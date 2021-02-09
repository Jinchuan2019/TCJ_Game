using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefController : CharacterController
{
    // Start is called before the first frame update
    protected override  void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerStay(Collider other) 
    {
        base.OnTriggerStay(other);
    }
}
