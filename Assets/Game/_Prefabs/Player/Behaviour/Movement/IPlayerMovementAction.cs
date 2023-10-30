using UnityEngine.InputSystem;

public interface IPlayerMovementAction
{
    void DoAction(InputAction.CallbackContext context);
}