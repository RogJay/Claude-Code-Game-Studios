// PROTOTYPE - NOT FOR PRODUCTION
// Question: Macht die Gotteshand (packen, hochheben, werfen) bei 50 Menschen nach 30 s Spass, und haelt die Bildzeit?
// Date: 2026-10-25
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Allmacht.Prototype.Gotteshand.Editor
{
    // Menu entry: builds a fresh empty scene with the prototype and enters Play Mode.
    public static class StartPrototypeMenu
    {
        [MenuItem("Allmacht/Prototyp 2 - Gotteshand starten")]
        static void StartPrototype()
        {
            if (EditorApplication.isPlaying) return;
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            new GameObject("GotteshandPrototype").AddComponent<GodHandPrototype>();
            EditorApplication.EnterPlaymode();
        }
    }
}
