using System.Text;
using UnityEngine;

public static class ConsoleShortCuts
{
    static readonly StringBuilder sb = new StringBuilder(50);
    public static void NotInjectedWarn(string objectName)
    {
        sb.Clear();
        sb.Append("No ").
            Append(objectName).
            Append(" injected");
        Debug.LogWarning(sb);
    }

    public static void FieldOverrideWarn(string fieldName)
    {
        sb.Clear();
        sb.Append(fieldName).
            Append(" field override attempt!");
        Debug.LogWarning(sb);
    }
    public static void MultiInitializeWarn(string name)
    {
        sb.Clear();
        sb.Append(name).
            Append(" field override attempt!");
        Debug.LogWarning(sb);
    }
}
