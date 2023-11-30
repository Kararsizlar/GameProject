using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHub
{
    public static IWalkEvents.WalkStarted walkStartedEvent;
    public static IWalkEvents.WalkStopped walkStoppedEvent;

    public static IDashEvents.DashStarted dashStartedEvent;
    public static IDashEvents.DashStopped dashStoppedEvent;

    public static ILedgeGrabEvents.GrabStarted grabStartedEvent;
    public static ILedgeGrabEvents.GrabStopped grabStoppedEvent;

    public static IJumpEvents.JumpStarted jumpStartedEvent;
    public static IJumpEvents.JumpStopped jumpStoppedEvent;

    public static IAttackEvents.AttackStarted attackStartedEvent;
    public static IAttackEvents.AttackStopped attackStoppedEvent;

    public delegate void OnGrounded();
    public static OnGrounded onGroundedEvent;
}
