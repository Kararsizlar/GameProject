using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchMelee : MonoBehaviour
{
    [SerializeField] Transform entity;
    [SerializeField] LayerMask ignoreEnemy;
    [SerializeField] LayerMask targetList;
    [SerializeField] Vector2 visionBoxSize;
    [SerializeField] float realizationTime;
    [SerializeField] float realizationRandomizer;

    private float d;
    private Vector2 dir;
    public bool canSeeTarget;

    private bool LookForPlayer(){
        Collider2D player = Physics2D.OverlapBox((Vector2)entity.position,visionBoxSize,0,targetList);
        if(player == null)
            return false;
        
        Vector2 direction = player.transform.position - entity.position;
        float distance = d = direction.magnitude;
        direction.Normalize();
        dir = direction;

        RaycastHit2D cast = Physics2D.Raycast(entity.position,direction,distance,ignoreEnemy);
        return targetList == ( targetList | (1 <<  cast.collider.gameObject.layer)); //Check the mask or something i guess
    }

    private IEnumerator Start(){
        while(true){
            yield return Time.deltaTime;
            canSeeTarget = LookForPlayer();

        }
    }

    private void OnDrawGizmos(){

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(entity.position,visionBoxSize);

        if(canSeeTarget)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
            
        Gizmos.DrawLine(entity.position,(Vector2)entity.position + (dir * d));
    }
}
