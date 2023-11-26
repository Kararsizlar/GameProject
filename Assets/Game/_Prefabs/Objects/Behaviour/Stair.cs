using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] Vector2 teleportPosition;
    [SerializeField] int signOfVelocityToNextStair;

    private void OnTriggerStay2D(Collider2D collision2D){
        if(!collision2D.gameObject.CompareTag("Player"))
            return;

        PlayerData playerData = collision2D.gameObject.transform.parent.GetComponent<PlayerData>();

        if(playerData.direction == signOfVelocityToNextStair && playerData.onRun)
            Teleport(collision2D.gameObject.transform); print("??");
    }

    private void Teleport(Transform playerCollider){
        playerCollider.parent.position += (Vector3)teleportPosition;
    }
}
