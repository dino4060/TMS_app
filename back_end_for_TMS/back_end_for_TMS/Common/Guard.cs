namespace back_end_for_TMS.Common;

public static class Guard
{
    public static T TryCatch<T>(Func<T> action, Func<Exception, Exception> errorMapper)
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            throw errorMapper(ex);
        }
    }

    public static Func<T> TryFunc<T>(Func<T> func) => func;

    public static T OrThrow<T>(this Func<T> action, Func<Exception, Exception> errorMapper)
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            throw errorMapper(ex);
        }
    }
}