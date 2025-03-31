/// <summary>
/// Base event class with handler
/// </summary>
/// <typeparam name="T">Event type</typeparam>
#nullable enable
public abstract class FWStaticEvent<T> where T : FWStaticEvent<T>
{
    public delegate void OnEvent(T e);
    public static event OnEvent? Handler;

    public void Invoke()
    {
        Handler?.Invoke((T)this);
    }
}