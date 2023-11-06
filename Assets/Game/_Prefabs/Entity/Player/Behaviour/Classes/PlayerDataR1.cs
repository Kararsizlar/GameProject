using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;

public class PlayerDataR1 : PlayerData
{
    [Header("Birinci bölgeye özel değerler")]
    public Walk walk;
    public Jump jump;
    public Dash dash;
    public Attack attack;
    public LedgeGrab ledgeGrab;
    public IsOnAir isOnAir;
    public IsFlipped isFlipped;

    private void Awake(){
        walk.playerData = this;
        jump.playerData = this;
        dash.playerData = this;
        attack.playerData = this;
        ledgeGrab.playerData = this;
        isOnAir.playerData = this;
        isFlipped.playerData = this;
    }
}
