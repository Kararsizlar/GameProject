using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Animator animator;
    public void OnAttackStart()
    {
        print($"Attack{playerData.currentCombo}");
        animator.Play($"Attack{playerData.currentCombo}");
    }

    public void OnAttackStop()
    {
        print("OnAttackStop");
        if(playerData.onWalk)
            animator.Play("Walk");  
        else if(playerData.onAir)
            animator.Play("Fall");
        else 
            animator.Play("Idle");
    }

    public void OnDashStart()
    {
        print("Dash Start");
        animator.Play("Dash");
    }

    public void OnDashStop()
    {
        print("Dash Stop");
        if(playerData.walk.walkData.interrupted)
            animator.Play("Walk");  
        else if(playerData.onAir)
            animator.Play("Fall");
        else 
            animator.Play("Idle");
        
    }

    public void OnJumpStart()
    {
        print("Jump");
        animator.Play("Jump");
    }

    public void OnJumpStop()
    {
        print("Jump Stop");
        if(playerData.onAir)
            animator.Play("Fall");  
        else if(playerData.onWalk)
            animator.Play("Walk");
        else 
            animator.Play("Idle");
    }

    public void OnWalkStart()
    {
        print("Walk Start");
        if(playerData.onAir)
            return;
        if(playerData.dashing)
            return;
        
        animator.Play("Walk");
    }

    public void OnWalkStop()
    {
        print("Walk Stop");
        if(playerData.onAir)
            return;
        else if(playerData.dashing)
            return;
        else animator.Play("Idle");
    }

    public void OnWGrabStart()
    {
        animator.Play("LedgeGrab");
    }

    public void OnWGrabStop()
    {
        if(playerData.onWalk)
            animator.Play("Walk");
        else
            animator.Play("Idle");
    }

    public void OnGrounded(){
        if(playerData.onWalk)
            animator.Play("Walk");
        else if(playerData.dashing)
            animator.Play("Dash");
        else
            animator.Play("Idle");
    }

    private void Start(){
        EventHub.walkStartedEvent += OnWalkStart;
        EventHub.walkStoppedEvent += OnWalkStop;
        EventHub.jumpStartedEvent += OnJumpStart;
        EventHub.jumpStoppedEvent += OnJumpStop;
        EventHub.dashStartedEvent += OnDashStart;
        EventHub.dashStoppedEvent += OnDashStop;
        EventHub.grabStartedEvent += OnWGrabStart;
        EventHub.grabStoppedEvent += OnWGrabStop;
        EventHub.attackStoppedEvent += OnAttackStart;
        EventHub.attackStoppedEvent += OnAttackStop;
        EventHub.onGroundedEvent += OnGrounded;
    }
}
