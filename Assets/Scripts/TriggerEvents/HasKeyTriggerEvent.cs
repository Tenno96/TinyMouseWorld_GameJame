using UnityEngine;
using UnityEngine.Events;

public class HasKeyTriggerEvent : TriggerEvent
{
    [SerializeField] private UnityEvent<Collider2D> OnSuccessEvent;
    [SerializeField] private UnityEvent<Collider2D> OnFailedEvent;


    private bool isFirstInteraction = true;

    protected override void OnTriggerEvent(Collider2D collision)
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (collision.gameObject.CompareTag("Player") && gameManager && gameManager.PlayerHasKey)
        {
            OnSuccessEvent!.Invoke(collision);

            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (isFirstInteraction)
        {
            isFirstInteraction = false;
            OnFailedEvent!.Invoke(collision);
        }
    }
}
