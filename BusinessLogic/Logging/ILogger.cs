namespace BusinessLogic.Logging
{
    public interface IProcessLogger
    {
        void Log(string message, bool logMessage);
    }
}