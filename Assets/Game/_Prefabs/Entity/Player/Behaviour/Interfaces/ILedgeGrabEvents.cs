public interface ILedgeGrabEvents
{
    public delegate void GrabStarted();
    public delegate void GrabStopped();
    public void OnWGrabStart();
    public void OnWGrabStop();
}
