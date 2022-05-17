using UnityEditor;
using UnityEngine;

public static class DeletePlayerPrefs
{
    [MenuItem("Edit/Clear PlayerPrefs")]
    private static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    
    
    [MenuItem("Edit/Force update")]
    private static void ForceUpdate()
    {
        UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
    }
    
}
