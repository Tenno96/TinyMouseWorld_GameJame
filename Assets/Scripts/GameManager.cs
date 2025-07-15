using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables

    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject playerControllerPrefab;
    [SerializeField] private GameObject hudPrefab;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private AudioClip audioGameOver;
    [SerializeField,] private UnityEvent playGameEvent;

    protected PlayerController playerController;
    protected Character playerCharacter;

    // Properties

    public bool PlayerHasKey { get; private set; } = false;

    void Awake()
    {
        InitGame();
        playGameEvent?.Invoke();

        Bus<PlayerDeathEvent>.OnEvent += PlayerDie;
    }

    void OnDestroy()
    {
        Bus<PlayerDeathEvent>.OnEvent -= PlayerDie;
    }

    private void InitGame()
    {
        GameObject character = GameObject.FindGameObjectWithTag("Player");
        if (character == null)
        {
            Transform playerStartTranform = GetPlayerStart();
            character = Instantiate(characterPrefab, playerStartTranform.position, transform.rotation);
        }

        playerCharacter = character.GetComponent<Character>();

        GameObject playerControllerSpawned = Instantiate(playerControllerPrefab, transform.position, transform.rotation);
        playerController = playerControllerSpawned.GetComponent<PlayerController>();
        playerController.Posses(playerCharacter);

        Instantiate(hudPrefab);

        cinemachineCamera.Follow = character.transform;
    }

    private Transform GetPlayerStart()
    {
        GameObject foundPlayerStart = GameObject.FindGameObjectWithTag("Respawn");
        if (foundPlayerStart)
        {
            return foundPlayerStart.transform;
        }

        Debug.LogError("Not found PlayerStart gameobject!");
        return transform;
    }

    private void GameOver()
    {
        // Play audio GameOver
        GameObject audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        AudioSource audioSourceManager = audioManager.GetComponent<AudioSource>();
        audioSourceManager.clip = audioGameOver;
        audioSourceManager.loop = false;
        audioSourceManager.Play();

        // Set visible cursor
        Cursor.visible = true;
    }

    private void PlayerDie(PlayerDeathEvent evt)
    {
        GameOver();
    }

    public void AddKeyInventory()
    {
        PlayerHasKey = true;
    }

    public void WinGame()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("CrediteScene");
    }

    public void Notify(string message)
    {
        Bus<NotifyMessageEvent>.Raise(new NotifyMessageEvent(message));
    }

    public void DisablePlayerInput()
    {
        playerController.DisablePlayerInput();
    }

    public void EnablePlayerInput()
    {
        playerController.EnablePlayerInput();
    }
}
