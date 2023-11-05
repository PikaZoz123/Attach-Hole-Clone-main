using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public static class DeletePlayerPrefs
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
