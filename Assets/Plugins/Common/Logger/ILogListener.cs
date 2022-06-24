using UnityEngine;

namespace Plugins.Common
{
    public interface ILogsListener
    {
        public void Log(LogType type, string log);
    }
}