
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] Elevator elevator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            elevator.TriggerElevator();
    }

}
