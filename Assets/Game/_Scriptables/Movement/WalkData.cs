using UnityEngine;

[CreateAssetMenu(menuName = "Data/Movement/Walk")]
public class WalkData : ScriptableObject
{

    [Tooltip("Oyuncunun yürüme hızı")]
    public float speed = 8f;

    [Tooltip("Oyuncunun hızının sıfıra ayarlanmadan önceki minimum hızı. Bu sayının altında hız sıfırlanıyor.")]
    public float minSpeedBeforeStop = 0.25f;

    [Tooltip("Hızın yavaşlama çarpanı. Oyuncunun hızı yavaşlarken her frame bu sayıyla çarpılır. 0 ile 1 arasında olmalı.")]
    [Range(0,1)]public float slowDownMultiplier = 0.85f;
}
