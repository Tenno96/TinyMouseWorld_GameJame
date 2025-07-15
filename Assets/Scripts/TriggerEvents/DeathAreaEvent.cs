using UnityEngine;
using UnityEngine.Events;

public class DeathAreaEvent : TriggerEvent
{

    [SerializeField] private UnityEvent onDeathEvent;

    protected override void OnTriggerEvent(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            character.TakeDamage();
            onDeathEvent?.Invoke();
        }
    }
}