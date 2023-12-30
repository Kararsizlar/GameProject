using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBullet : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData p = collision.gameObject.GetComponent<PlayerData>();
            p.HP -= damage;
            if(p.HP < 0)
                Destroy(collision.gameObject);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        //Destroy(gameObject);
    }
}
