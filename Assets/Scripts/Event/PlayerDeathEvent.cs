using UnityEngine;

public struct PlayerDeathEvent : IEvent
{
    public GameObject PlayerDeath { get; private set; }

    public PlayerDeathEvent(GameObject playerDeath)
    {
        PlayerDeath = playerDeath;
    }
}