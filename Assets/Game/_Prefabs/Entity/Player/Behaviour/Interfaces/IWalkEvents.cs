public interface IWalkEvents
{
    public delegate void WalkStarted();
    public delegate void WalkStopped();
    public void OnWalkStart();
    public void OnWalkStop();
}
