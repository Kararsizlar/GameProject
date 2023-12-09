using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private float yTeleportLength;
    private BoxCollider2D Collider2D;
    [SerializeField] StairDirection stairDirection;

    private void OnCollisionStay2D(Collision2D collision2D){
        if(!collision2D.gameObject.CompareTag("Player"))
            return;
        if(collision2D.GetContact(0).normal.x == 0)
            return;

        PlayerData playerData = collision2D.transform.GetComponent<PlayerData>();
        bool isInCorrectState = playerData.direction == (int)stairDirection && playerData.onRun;
        if(isInCorrectState)
            Teleport(collision2D.transform);
    }

    private void Teleport(Transform playerCollider){
        Vector3 p = new(0,yTeleportLength,0);
        playerCollider.GetComponent<Rigidbody2D>().MovePosition(playerCollider.transform.position + p);
    }

    private void Start(){    
        Collider2D = GetComponent<BoxCollider2D>();
        yTeleportLength = Collider2D.size.y / 2;
    }

    public enum StairDirection{
        Left = -1,
        Right = 1
    }
}
