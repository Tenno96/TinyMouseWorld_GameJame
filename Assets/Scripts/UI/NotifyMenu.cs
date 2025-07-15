using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class NotifyMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelMessage;
    [SerializeField] private TextMeshProUGUI textMeshMessage;

    void OnEnable()
    {
        Bus<NotifyMessageEvent>.OnEvent += OnNotify;
    }

    void OnDisable()
    {
        Bus<NotifyMessageEvent>.OnEvent -= OnNotify;
    }

    private void OnNotify(NotifyMessageEvent evt)
    {
        Notify(evt.Message);
    }

    public async void Notify(string message)
    {
        textMeshMessage.SetText(message);
        panelMessage.SetActive(true);
        await Task.Delay(5000);
        panelMessage.SetActive(false);
    }
}
