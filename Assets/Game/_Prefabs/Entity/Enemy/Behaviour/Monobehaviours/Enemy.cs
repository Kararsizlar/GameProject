using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour{
        public EnemyHealth healthHandler;
        [SerializeField] HealthData healthDataAsset;
        
        internal void Kill(){
            Destroy(transform.parent.gameObject);
        }
        
        private void Awake(){
            healthHandler = new();
            healthHandler.data = Instantiate(healthDataAsset);
            healthHandler.entity = this;
        }
    }
}

