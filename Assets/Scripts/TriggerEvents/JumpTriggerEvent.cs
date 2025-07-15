using UnityEngine;

public class JumpTriggerEvent : TriggerEvent
{
    [SerializeField] private float jumpForce = 200.0f;

    protected override void OnTriggerEvent(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character)
        {
            character.JumpForce(jumpForce);
        }
    }
}
