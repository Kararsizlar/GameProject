public interface IJumpEvents
{
    public delegate void JumpStarted();
    public delegate void JumpStopped();
    public void OnJumpStart();
    public void OnJumpStop();
}