using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Rigidbody2D ElevatorPanel;
    [SerializeField] AnimationCurve elevatorCurve;
    [SerializeField] float timeToReachOtherSide;
    [SerializeField] Vector2 posDown,posUp;
    [SerializeField] bool isDown;
    [SerializeField] bool currentlyActive;

    private WaitForFixedUpdate elevatorWaiter = new();

    private IEnumerator UseElevator(){
        currentlyActive = true;
        Vector2 target;
        Vector2 start;
        if(isDown){
            target = posUp;
            start = posDown;
        }
        else{
            target = posDown;
            start = posUp;
        }

        float t = 0;
        isDown = !isDown;
        while(!ElevatorPanel.position.Equals(target)){
            t += Time.fixedDeltaTime;
            ElevatorPanel.MovePosition(Vector2.Lerp(start,target,elevatorCurve.Evaluate(t / timeToReachOtherSide)));
            yield return elevatorWaiter;
        }
        currentlyActive = false;
    }

    public void TriggerElevator(){
        if(!currentlyActive)
            StartCoroutine(UseElevator());
    }
}
