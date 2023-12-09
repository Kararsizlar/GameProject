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

        animator.Play($"Attack{playerData.currentCombo}");
    }

    public void OnAttackStop()
    {

        if(playerData.onWalk)
            animator.Play("Walk");  
        else if(playerData.onAir)
            animator.Play("Fall");
        else 
            animator.Play("Idle");
    }

    public void OnDashStart()
    {

        animator.Play("Dash");
    }

    public void OnDashStop()
    {

        if(playerData.walk.walkData.interrupted)
            animator.Play("Walk");  
        else if(playerData.onAir)
            animator.Play("Fall");
        else 
            animator.Play("Idle");
        
    }

    public void OnJumpStart()
    {

        animator.Play("Jump");
    }

    public void OnJumpStop()
    {

        if(playerData.onAir)
            animator.Play("Fall");  
        else if(playerData.onWalk)
            animator.Play("Walk");
        else 
            animator.Play("Idle");
    }

    public void OnWalkStart()
    {
        if(playerData.onAir)
            return;
        if(playerData.dashing)
            return;
        
        animator.Play("Walk");
    }

    public void OnWalkStop()
    {

        if(playerData.onAir)
            return;
        else if(playerData.dashing)
            return;
        else animator.Play("Idle");
    }

    public void OnWGrabStart()
    {

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

    private void OnEnable(){
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

        private void OnDisable(){
        EventHub.walkStartedEvent -= OnWalkStart;
        EventHub.walkStoppedEvent -= OnWalkStop;
        EventHub.jumpStartedEvent -= OnJumpStart;
        EventHub.jumpStoppedEvent -= OnJumpStop;
        EventHub.dashStartedEvent -= OnDashStart;
        EventHub.dashStoppedEvent -= OnDashStop;
        EventHub.grabStartedEvent -= OnWGrabStart;
        EventHub.grabStoppedEvent -= OnWGrabStop;
        EventHub.attackStoppedEvent -= OnAttackStart;
        EventHub.attackStoppedEvent -= OnAttackStop;
        EventHub.onGroundedEvent -= OnGrounded;
    }
}
