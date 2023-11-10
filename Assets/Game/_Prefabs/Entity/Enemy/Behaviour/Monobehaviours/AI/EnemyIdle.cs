using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies{
    public class EnemyIdle : MonoBehaviour
    {
        [SerializeField] List<Vector2> partolRelativePoints;
        [SerializeField] int partolIndex;
        [SerializeField] bool _Patrolling;

        public bool Patrolling{
            get{return _Patrolling;}

            set{
                _Patrolling = value;
                if (_Patrolling)
                    StartCoroutine(OnPatrol());
            }
        }
        private IEnumerator OnPatrol(){
            Vector2 nextPos = partolRelativePoints[partolIndex];
            Vector2 currentPos = transform.parent.position;
            float timeToWalk = 3f;
            float currentTime = 0;

            while(currentTime < timeToWalk){
                yield return Time.deltaTime;
                currentTime += Time.deltaTime;
                transform.parent.position = Vector2.Lerp(currentPos,nextPos,currentTime / timeToWalk);
            }
            partolIndex++;
            if(partolIndex == partolRelativePoints.Count)
                partolIndex = 0;
                
            Patrolling = false;
        }

        private IEnumerator Start(){

            while(true){
                yield return new WaitForSeconds(5f);
                Patrolling = true;
            }
        }

        private void OnDrawGizmos(){
            if (partolRelativePoints.Count == 0)
                return;
            
            Gizmos.color = Color.magenta;
            foreach (Vector2 point in partolRelativePoints)
            {
                Gizmos.DrawSphere(point,0.1f);
            }               
        }

        private void OnDrawGizmosSelected(){
            if (partolRelativePoints.Count == 0)
                return;
            
            Gizmos.color = Color.red;
            foreach (Vector2 point in partolRelativePoints)
            {
                Gizmos.DrawSphere(point,0.1f);
            }               
        }
    }
}
