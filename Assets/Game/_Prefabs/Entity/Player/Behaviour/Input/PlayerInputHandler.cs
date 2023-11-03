using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public abstract class InputHandler : MonoBehaviour
    {
        public GameObject componentContainer;
        public InputActionAsset inputAsset;
    }
}

