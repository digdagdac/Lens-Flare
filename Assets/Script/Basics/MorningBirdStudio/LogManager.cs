using System;
using System.ComponentModel;
using UnityEngine;

namespace MorningBird.IO
{
    public enum EGameStatus : sbyte
    {
        None = 0,
        
    }

    public enum EManagerStatus : sbyte
    {
        None = 0,
    }

    public enum ELogLevel : sbyte
    {
        None, // For Debug
        Normal = 1,
        ImportantNormal = 2,
        Expected = 3,
        Warning = 4,
        Error = 5,
        MoreThanNormal = 6,
        LessThanNormal = 7,
        
    }

    [System.Serializable]
    public class LogFormet
    {
        public LogFormet(string logDescription, ELogLevel logLevel)
        {
            LogDescription = logDescription;
            LogLevel = logLevel;
        }
        public LogFormet(DateTime logTime, ELogLevel logLevel, string logDescription)
        {
            LogTime = logTime;
            LogLevel = logLevel;
            LogDescription = logDescription;
        }

        public LogFormet(DateTime logTime, float gameTime, ELogLevel logLevel, string logDescription)
        {
            LogTime = logTime;
            GameTime = gameTime;
            LogLevel = logLevel;
            LogDescription = logDescription;
        }

        [ReadOnly(true)]
        public DateTime LogTime = DateTime.Now;
        [ReadOnly(true)]
        public float GameTime = Time.realtimeSinceStartup;
        [ReadOnly(true)]
        public ELogLevel LogLevel = ELogLevel.Normal;
        [ReadOnly(true)]
        public string LogDescription;
    }

    public static class LogManager
    {
        // 색상 표 참조 https://namu.wiki/w/%ED%97%A5%EC%8A%A4%20%EC%BD%94%EB%93%9C

        public static readonly string ColorRed = "#DC143C";
        public static readonly string ColorGreen = "#228B22";
        public static readonly string ColorOrange = "#FF4500";
        public static readonly string ColorYellow = "Yellow";
        public static readonly string ColorBlue = "#4AA8D8";

        public static float RealtimeSinceStartup;

        public static void UpdateExecution()
        {
            RealtimeSinceStartup = Time.realtimeSinceStartup;
        }

        public static void ReportLogAssertion(LogFormet logFormet)
        {
#if UNITY_EDITOR || DEBUG

            string perfix;
            string description = logFormet.LogDescription;

            switch (logFormet.LogLevel)
            {
                case ELogLevel.LessThanNormal:
                    perfix = $"<color={ColorYellow}>Normal</color>";
                    break;
                case ELogLevel.Normal:
                    perfix = "<color=white>Normal</color>";
                    break;
                case ELogLevel.MoreThanNormal:
                    perfix = $"<color={ColorBlue}>Normal</color>";
                    break;
                case ELogLevel.ImportantNormal:
                    perfix = $"<color={ColorOrange}>Normal</color>";
                    break;
                case ELogLevel.Expected:
                    perfix = $"<color={ColorGreen}>Expected</color>";
                    break;
                case ELogLevel.Warning:
                    perfix = $"<color={ColorYellow}>Warning</color>";
                    break;
                case ELogLevel.Error:
                    perfix = $"<color={ColorRed}>Error</color>";
                    break;
                case ELogLevel.None:
                default:
                    CheckValueReportLogAssertion(logFormet.LogLevel.ToString());
                    perfix = $"<color={ColorRed}>Error</color>";
                    break;
            }

            Debug.LogAssertion($"{perfix} :: {logFormet.LogTime} , {logFormet.GameTime}\n{description}");
#endif
        }

        public static void CheckValueReportLogAssertion(string objectName)
        {
#if UNITY_EDITOR || DEBUG

            DateTime LogTime = DateTime.Now;
            float GameTime = Time.realtimeSinceStartup;

            Debug.LogAssertion($"<color={ColorRed}>Error</color> :: {LogTime} , {GameTime}\nCheck Value! {objectName}");
#endif
        }

        public static void ReportLog(LogFormet logFormet)
        {
            string perfix;
            string description = logFormet.LogDescription;
            string compliteLog = "";

            switch (logFormet.LogLevel)
            {
                case ELogLevel.LessThanNormal:
                    perfix = $"<color={ColorYellow}>Normal</color>";
                    break;
                case ELogLevel.Normal:
                    perfix = "<color=white>Normal</color>";
                    break;
                case ELogLevel.MoreThanNormal:
                    perfix = $"<color={ColorBlue}>Normal</color>";
                    break;
                case ELogLevel.ImportantNormal:
                    perfix = $"<color={ColorOrange}>Normal</color>";
                    break;
                case ELogLevel.Expected:
                    perfix = $"<color={ColorGreen}>Expected</color>";
                    break;
                case ELogLevel.Warning:
                    perfix = $"<color={ColorYellow}>Warning</color>";
                    break;
                case ELogLevel.Error:
                    perfix = $"<color={ColorRed}>Error</color>";
                    break;
                case ELogLevel.None:
                default:
                    CheckValueReportLogAssertion(logFormet.LogLevel.ToString());
                    perfix = $"<color={ColorRed}>Error</color>";
                    CheckValueReportLog(logFormet.LogLevel.ToString());
                    return;
            }

            compliteLog = $"{perfix} :: {logFormet.LogTime} , {logFormet.GameTime}\n::{description}";

            switch (logFormet.LogLevel)
            {
                case ELogLevel.LessThanNormal:
                case ELogLevel.Normal:
                case ELogLevel.MoreThanNormal:
                case ELogLevel.ImportantNormal:
                    Debug.Log(compliteLog);
                    break;
                case ELogLevel.Expected:
                    Debug.Log(compliteLog);
                    break;
                case ELogLevel.Warning:
                    Debug.LogWarning(compliteLog);
                    break;
                case ELogLevel.Error:
                    Debug.LogError(compliteLog);
                    break;
                case ELogLevel.None:
                default:
                    Debug.LogError(compliteLog);
                    break;
            }

        }

        public static void CheckValueReportLog(string objectName)
        {
            DateTime LogTime = DateTime.Now;
            float GameTime = Time.realtimeSinceStartup;

            Debug.Log($"<color={ColorRed}>Error</color> :: {LogTime} , {GameTime}\nCheck Value! {objectName}");
        }

        public static void ReportTotalGameContorlGameStatusChange(EGameStatus gameStatus, bool isStartReport, bool isArrived)
        {
            ReportLog(new LogFormet($"TotalGameControlManager - Report :: {gameStatus.ToString()}_{(isStartReport ? "Start" : "End")}_{(isArrived ? "Begine" : "Done")}", ELogLevel.ImportantNormal));

        }

        public static void ReportManagerGameStatusChange(object classNameToShow, EManagerStatus gameStatus, bool isStartReport, bool isArrived)
        {
            ReportLog(new LogFormet($"{classNameToShow.ToString()} - Report :: {gameStatus.ToString()}_{(isStartReport ? "Start" : "End")}_{(isArrived ? "Begine" : "Done")}", ELogLevel.ImportantNormal));
        }
    }
}