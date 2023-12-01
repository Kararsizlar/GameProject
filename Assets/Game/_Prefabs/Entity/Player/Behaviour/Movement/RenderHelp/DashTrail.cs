using System.Collections;
using UnityEngine;

public class DashTrail : MonoBehaviour{

    [SerializeField] float totalTime;
    [SerializeField] SpriteRenderer spriteRenderer;
    [HideInInspector] public Sprite spriteToSet;
    public bool isFlipped;

    private IEnumerator Start(){
        float survivedTime = 0;
        spriteRenderer.sprite = spriteToSet;
        spriteRenderer.flipX = isFlipped;
        while (survivedTime < totalTime){

            survivedTime += Time.deltaTime;
            spriteRenderer.color = new Color(1,1,1,totalTime - survivedTime / totalTime);
            yield return Time.deltaTime;
        }

        Destroy(gameObject);
    }
}