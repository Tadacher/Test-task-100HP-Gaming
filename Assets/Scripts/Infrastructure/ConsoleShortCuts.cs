using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class ConsoleShortCuts
{
    static StringBuilder sb = new StringBuilder(50);
    internal static void NotInjectedWarn(string objectName)
    {
        sb.Clear();
        sb.Append("No ").
            Append(objectName).
            Append(" injected");
        Debug.LogWarning(sb);
    }

    internal static void FieldOverrideWarn(string fieldName)
    {
        sb.Clear();
        sb.Append(fieldName).
            Append(" field override attempt!");
        Debug.LogWarning(sb);
    }
    internal static void MultiInitializeWarn(string name)
    {
        sb.Clear();
        sb.Append(name).
            Append(" field override attempt!");
        Debug.LogWarning(sb);
    }
}
