
public struct NotifyMessageEvent : IEvent
{
    public string Message { get; private set; }

    public NotifyMessageEvent(string message = "")
    {
        Message = message;
    }
}