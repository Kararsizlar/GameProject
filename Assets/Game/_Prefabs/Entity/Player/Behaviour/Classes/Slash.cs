using UnityEngine;

[System.Serializable]
public class Slash{
    [Tooltip("Karakterin merkezinden ne kadar uzakta hitbox konumlandırılacak?")]
    public Vector2 offset;
    [Tooltip("Hitbox boyu")]
    public Vector2 size;
    [Tooltip("Verilecek hasar")]
    public float damage;
    [Tooltip("Hitboxun açık kalma süresi")]
    public float attackDuration;
    [Tooltip("Atak bittikten sonra başlaya ve sonraki atak için gereken süre. Hitbox süresi ile eklendiğinde iki atak arasındaki gerçek zaman bulunur")]
    public float cooldownTime;
}