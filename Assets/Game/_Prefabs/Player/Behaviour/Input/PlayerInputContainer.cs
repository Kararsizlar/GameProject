using System;
using UnityEngine;

namespace Player{

    public class PlayerInputContainer : MonoBehaviour{
        public PlayerInputHandler inputHandler;
        public GameObject componentContainer;

        private void Start(){
            inputHandler.Initialize();
            inputHandler.componentContainer = componentContainer;
        }
    }
}