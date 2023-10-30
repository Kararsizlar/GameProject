using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputInitializer : MonoBehaviour
    {
        [SerializeField] InputActionAsset actionAsset;
        [SerializeField] PlayerInputContainer inputDataContainer;
        [SerializeField] GameObject componentContainer;
        [SerializeField] ActionMap action;
        private void Awake(){
            switch (action)
            {
                case ActionMap.Region1:
                    PlayerInputHandler inputClass = new PlayerInputR1
                    {
                        componentContainer = componentContainer,
                        inputMap = actionAsset.FindActionMap("Region1")
                    };
                    inputDataContainer.inputHandler = inputClass;
                    Destroy(gameObject);
                break;
            }
        }
    }

    public enum ActionMap{
        Region1 = 0,
        Region2 = 1
    }
}