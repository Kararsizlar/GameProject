using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public abstract class PlayerInputHandler
    {
        public GameObject componentContainer;
        public InputActionMap inputMap;

        public void Initialize(){
            SetUpActionHandler();
        }

        public abstract void SetUpActionHandler();
    }
}

