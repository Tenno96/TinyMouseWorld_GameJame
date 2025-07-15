using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Character ownerCharacter;


    void OnEnable()
    {
        Bus<PlayerDeathEvent>.OnEvent += OnPlayerDeath;
    }

    void OnDisable()
    {
        Bus<PlayerDeathEvent>.OnEvent -= OnPlayerDeath;
    }

    private void OnPlayerDeath(PlayerDeathEvent evt)
    {
        DisablePlayerInput();
    }

    public void Posses(Character character)
    {
        ownerCharacter = character;
        ownerCharacter.PossesedBy(this);
    }

    public void DisablePlayerInput()
    {
        GetComponent<PlayerInput>().enabled = false;
    }

    public void EnablePlayerInput()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (ownerCharacter)
        {
            ownerCharacter.Move(context.ReadValue<Vector2>().x);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (ownerCharacter && context.performed)
        {
            ownerCharacter.Jump();
        }
    }
}
