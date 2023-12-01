using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHub
{
    public delegate void WalkStarted();
    public delegate void WalkStopped();
    public delegate void DashStarted();
    public delegate void DashStopped();
    public delegate void GrabStarted();
    public delegate void GrabStopped();
    public delegate void JumpStarted();
    public delegate void JumpStopped();
    public delegate void AttackStarted();
    public delegate void AttackStopped();
    public delegate void OnGrounded();
    public static WalkStarted walkStartedEvent;
    public static WalkStopped walkStoppedEvent;

    public static DashStarted dashStartedEvent;
    public static DashStopped dashStoppedEvent;

    public static GrabStarted grabStartedEvent;
    public static GrabStopped grabStoppedEvent;

    public static JumpStarted jumpStartedEvent;
    public static JumpStopped jumpStoppedEvent;

    public static AttackStarted attackStartedEvent;
    public static AttackStopped attackStoppedEvent;
    public static OnGrounded onGroundedEvent;
}
