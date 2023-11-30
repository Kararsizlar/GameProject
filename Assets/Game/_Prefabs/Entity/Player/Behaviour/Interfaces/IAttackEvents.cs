public interface IAttackEvents{
    public delegate void AttackStarted();
    public delegate void AttackStopped();
    public void OnAttackStart();
    public void OnAttackStop();
}