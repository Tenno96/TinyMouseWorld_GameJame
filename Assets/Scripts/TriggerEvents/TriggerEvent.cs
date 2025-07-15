using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class TriggerEvent : MonoBehaviour
{

    void OnEnable()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnDisable()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEvent(collision);
    }

    protected abstract void OnTriggerEvent(Collider2D collision);
}
