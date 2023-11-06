using UnityEngine.InputSystem;

public interface IPlayerMovementAction
{
    public void DoAction(InputAction.CallbackContext context);
}