public interface IDashEvents
{
    public delegate void DashStarted();
    public delegate void DashStopped();
    public void OnDashStart();
    public void OnDashStop();
}
