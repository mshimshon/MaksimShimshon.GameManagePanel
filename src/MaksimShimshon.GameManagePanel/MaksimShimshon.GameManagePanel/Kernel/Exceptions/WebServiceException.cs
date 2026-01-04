namespace MaksimShimshon.GameManagePanel.Kernel.Exceptions;


public class WebServiceException : Exception
{
    public WebServiceException(string? message) : base(message)
    {
    }
    public WebServiceException(string? message, Exception origin) : base(message)
    {
        Origin = origin;
    }

    public Exception? Origin { get; }
}
