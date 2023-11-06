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
            Vector2 nextPos = (Vector2)transform.parent.position + partolRelativePoints[partolIndex];
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
    }
}
