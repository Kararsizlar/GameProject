using UnityEngine;

[CreateAssetMenu(menuName = "Data/Movement/Jump")]
public class JumpData : ScriptableObject {

    [Tooltip("Oyuncunun zıplama hızı.")]
    public float speed = 18;

    [Tooltip("Oyuncunun zıplayarak çıkabileceği maksimum yükseklik")]
    public float maxDistance = 4.1f;
}