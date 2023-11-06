using UnityEngine;

[CreateAssetMenu(menuName = "Data/Movement/Dash")]
public class DashData : ScriptableObject
{

    [Header("Genel Değerler, hem spot dodge hemde dash bunu kullanıyor")]
    
    [Tooltip("Dash'in yeniden kullanıabilir olması için gereken süre.")]
    public float timeBetweenDashes;
    [Tooltip("Dash'in ne kadar uzun süre durduğu.")]
    public float dashActionTime;

    [Header("Dash İçin  gerekli değerler")]
    [Tooltip("Dash Hızı")]
    public float dashSpeed;
}