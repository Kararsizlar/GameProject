using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Transform player;
    public SpriteRenderer conirmation;
    public int sceneID;

    public bool canUse;

    private void Open()
    {
        if (conirmation != null)
            conirmation.enabled = true;
        
        canUse = true;
    }
    private void Close()
    {
        if(conirmation != null)
            conirmation.enabled = false;
        
        canUse = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == player)
            Open();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == player)
            Close();
    }

    public void OnDoorInput(InputAction.CallbackContext ctx)
    {
        if (canUse && ctx.phase == InputActionPhase.Started)
            SceneManager.LoadScene(sceneID);
    }
}
