using UnityEngine;
using UnityEngine.InputSystem;
using Enemies;
using System.Collections;
using System.Collections.Generic;

namespace PlayerSpace
{
    public class Attack : MonoBehaviour , IPlayerMovementAction
    {
        [HideInInspector] public PlayerData playerData;
        public AttackData attackData;
        private Coroutine attackCoroutine;//for debugging
        
        private void Hit(Slash slash,List<Transform> listHit){
            Vector2 pos = (Vector2)playerData.playerTransform.position + slash.offset * playerData.direction;
            RaycastHit2D[] hit = Physics2D.BoxCastAll(pos,slash.size,0,Vector2.zero,0,playerData.hittable);

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
            if(playerData.onAttack)
                return;
            
            playerData.onAttack = true;
            playerData.canDash = false;
            playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            print($"Doing attack {playerData.currentCombo}!");
            attackCoroutine = StartCoroutine(WaitSlash(attackData.attacks[playerData.currentCombo]));
        }

        private IEnumerator WaitSlash(Slash slash){
            float waitedTime = 0;
            List<Transform> enemiesHitWithThisSlash = new();

            while(waitedTime < slash.attackDuration){
                Hit(slash,enemiesHitWithThisSlash);
                waitedTime += Time.fixedDeltaTime;
                yield return Time.fixedDeltaTime;
            }
            playerData.canDash = true;
            playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerData.currentCombo = playerData.currentCombo == (attackData.attacks.Count - 1) ? 0 : playerData.currentCombo + 1;
            attackCoroutine = null;
            StartCoroutine(WaitCooldown(slash));
        }

        private IEnumerator WaitCooldown(Slash slash){
            yield return new WaitForSeconds(slash.cooldownTime);
            playerData.onAttack = false;
        }

        private void OnDrawGizmos(){
            if(!Application.isPlaying)
                return;
            
            if(attackCoroutine == null)
                return;
            
            Gizmos.color = Color.yellow;
            Slash s = attackData.attacks[playerData.currentCombo];
            Gizmos.DrawCube((Vector2)playerData.playerTransform.position + (playerData.direction * s.offset),s.size);
        }
    }
}
