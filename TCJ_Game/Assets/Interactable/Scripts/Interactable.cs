using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumClass
{
    public enum Event
    {
        OpenTheDoor,
    }
}

interface Interactable
{
    void Interact(CharacterController sender,EnumClass.Event eventName, bool key);
}
