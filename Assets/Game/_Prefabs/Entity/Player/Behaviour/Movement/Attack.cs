using UnityEngine;
using UnityEngine.InputSystem;
using Enemies;
using System.Collections;
using System.Collections.Generic;

public class Attack : MonoBehaviour , IPlayerMovementAction
{
    private Transform player;
    private Rigidbody2D rigidBody;
    private MovementData movementData;
    private AttackData attackData;
    private Coroutine attackCoroutine;//for debugging
    
    private void Hit(Slash slash,List<Transform> listHit){
        Vector2 pos = (Vector2)player.position + slash.offset * movementData.direction;
        RaycastHit2D[] hit = Physics2D.BoxCastAll(pos,slash.size,0,Vector2.zero,0,movementData.hittable);

        if(hit.Length == 0)
            return;

        for (int i = 0; i < hit.Length; i++)
        {
            Transform enemy = hit[i].transform.parent;

            if(!listHit.Contains(enemy)){
                enemy.GetComponentInChildren<Enemy>().healthHandler.GetHurt(slash.damage);
                listHit.Add(enemy);
            }
        }
    }

    public void DoAction(InputAction.CallbackContext context){
        if(movementData.onAttack)
            return;
        
        movementData.onAttack = true;
        movementData.canDash = false;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        print($"Doing attack {movementData.currentCombo}!");
        attackCoroutine = StartCoroutine(WaitSlash(attackData.attacks[movementData.currentCombo]));
    }

    private IEnumerator WaitSlash(Slash slash){
        float waitedTime = 0;
        List<Transform> enemiesHitWithThisSlash = new();

        while(waitedTime < slash.attackDuration){
            Hit(slash,enemiesHitWithThisSlash);
            waitedTime += Time.fixedDeltaTime;
            yield return Time.fixedDeltaTime;
        }
        movementData.canDash = true;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        movementData.currentCombo = movementData.currentCombo == (attackData.attacks.Count - 1) ? 0 : movementData.currentCombo + 1;
        attackCoroutine = null;
        StartCoroutine(WaitCooldown(slash));
    }

    private IEnumerator WaitCooldown(Slash slash){
        yield return new WaitForSeconds(slash.cooldownTime);
        movementData.onAttack = false;
    }

    private void Start(){
        attackData = Resources.Load<AttackData>("MovementData/AttackData");
        player = transform.parent.parent;
           
        if(!TryGetComponent<MovementData>(out movementData))
            movementData = gameObject.AddComponent<MovementData>();

        rigidBody = player.GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos(){
        if(attackCoroutine == null)
            return;
        
        Gizmos.color = Color.yellow;
        Slash s = attackData.attacks[movementData.currentCombo];
        Gizmos.DrawCube((Vector2)player.position + (movementData.direction * s.offset),s.size);
    }
}
