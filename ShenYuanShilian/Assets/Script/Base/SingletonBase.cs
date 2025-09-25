public class SingletonBase<T> where T : new()
{
    private static T instance;
    private static readonly object locker = new();
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (locker)
                {
                    instance ??= new T();
                }
            }
            return instance;
        }
    }
}
