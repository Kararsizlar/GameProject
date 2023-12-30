using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

namespace PlayerSpace
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] PlayerData playerData;
        [SerializeField] AttackData attackData;
        private AttackHelper attackHelper;

        private void Start(){
            attackHelper = new(attackData,playerData,this);
        }

        public void DoAction(InputAction.CallbackContext context){
            if(context.phase == InputActionPhase.Started)
                attackHelper.TryAttack();
        }
    }


    class AttackHelper{
        private PlayerData playerData;
        private AttackData attackData;
        private Attack attack;
        private Coroutine comboResetter;
        private void EndSlash(Slash slash){
            playerData.canDash = true;
            playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerData.currentCombo = playerData.currentCombo == (attackData.attacks.Length - 1) ? 0 : playerData.currentCombo + 1;
            attack.StartCoroutine(IEWaitCooldown(slash));
            EventHub.attackStoppedEvent?.Invoke();
        }

        private void Hit(Slash slash,List<Transform> transforms){
            Vector2 pos = (Vector2)playerData.playerTransform.position + slash.offset * playerData.direction;
            RaycastHit2D[] hit = Physics2D.BoxCastAll(pos,slash.size,0,Vector2.zero,0,playerData.hittable);

            if(hit.Length == 0)
                return;

            for (int i = 0; i < hit.Length; i++)
            {
                Transform enemy = hit[i].transform;

                if(!transforms.Contains(enemy)){
                    transforms.Add(enemy);
                    enemy.GetComponent<IHealth>().GetHurt(slash.damage);
                }
            }
        }

        private IEnumerator IEStartSlash(Slash slash){
            float waitedTime = 0;
            List<Transform> transforms = new List<Transform>();

            while(waitedTime < slash.attackDuration){
                Hit(slash, transforms);
                waitedTime += Time.fixedDeltaTime;
                yield return Time.fixedDeltaTime;
            }
            EndSlash(slash);
        }

        private IEnumerator IEWaitCooldown(Slash slash){
            yield return new WaitForSeconds(slash.cooldownTime);
            playerData.onAttack = false;
        }

        private IEnumerator IEComboTimer(){
            yield return new WaitForSeconds(playerData.timeForComboEnd);
            playerData.currentCombo = 0;
        }

        public void TryAttack(){
            if(playerData.onAttack || playerData.dashing)
                return;
            
            playerData.onAttack = true;
            playerData.canDash = false;
            playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            EventHub.attackStartedEvent?.Invoke();
            attack.StartCoroutine(IEStartSlash(attackData.attacks[playerData.currentCombo]));

            if(comboResetter != null){
                attack.StopCoroutine(comboResetter);
                comboResetter = null;
            }
            comboResetter = attack.StartCoroutine(IEComboTimer());
        }

        public AttackHelper(AttackData ad, PlayerData p, Attack a){
            attack = a;
            attackData = ad;
            playerData = p;
        }
    }
}
