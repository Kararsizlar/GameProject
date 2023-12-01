using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;

public class DashTrailBehaviour : MonoBehaviour
{
    [SerializeField] Dash dash;
    [SerializeField] GameObject trailPrefab;
    [SerializeField] SpriteRenderer playerRenderer;
    [SerializeField] Sprite sprite;
    [SerializeField] float amountOfTrails;

    public delegate void OnDashDelegate();
    public static OnDashDelegate onDash;

    private void OnDash(){
        StartCoroutine(ShootTrails());
    }

    private IEnumerator ShootTrails(){
        DashData dashData = dash.dashData;
        float dashTime = dashData.dashActionTime;
        int totalTrailSpawned = 0;

        while (totalTrailSpawned < amountOfTrails){
            totalTrailSpawned += 1;
            GameObject g = Instantiate(trailPrefab,transform.position,Quaternion.identity);
            DashTrail d = g.GetComponent<DashTrail>();
            d.spriteToSet = sprite;
            d.isFlipped = playerRenderer.flipX;
            yield return new WaitForSeconds(dashTime / amountOfTrails);
        }
    }

    private void Start(){
        onDash = OnDash;
    }
}
