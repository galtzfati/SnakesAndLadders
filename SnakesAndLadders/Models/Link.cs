namespace SnakesAndLadders.Models;

internal class Link<T>
{
    public T Endpoint1 { get; }
    public T Endpoint2 { get; }

    public Link(T endpoint1, T endpoint2)
    {
        Endpoint1 = endpoint1;
        Endpoint2 = endpoint2;
    }
}
