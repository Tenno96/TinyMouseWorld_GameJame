using UnityEngine;
using UnityEngine.Events;

public class UnityTriggerEvent : TriggerEvent
{
    [SerializeField] protected bool triggerOnce = false;
    [SerializeField] protected UnityEvent<Collider2D> OnObjectEnter;

    protected override void OnTriggerEvent(Collider2D collision)
    {
        OnObjectEnter!.Invoke(collision);

        if (triggerOnce)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
