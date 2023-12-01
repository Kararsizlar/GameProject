using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;
using UnityEngine.InputSystem;

public class Down : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    public delegate void OnDownInput();

    public void DoAction(InputAction.CallbackContext callback){
            
        if(callback.phase == InputActionPhase.Started){
            playerData.down = true;
        }

        if(callback.phase == InputActionPhase.Canceled){
            playerData.down = false;
        }
    }
}
