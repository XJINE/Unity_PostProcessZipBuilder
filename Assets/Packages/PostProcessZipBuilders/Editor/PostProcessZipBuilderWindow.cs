using UnityEditor;
using UnityEngine;
using Settings = PostProcessZipBuilders.PostProcessZipBuilder;

namespace PostProcessZipBuilders
{
    // NOTE:
    // Needs to define EditorWindow class in the same name .cs files.
    // Otherwise, it makes layout error.

    public class PostProcessZipBuilderWindow : EditorWindow
    {
        [MenuItem("Custom/" + nameof(PostProcessZipBuilder))]
        private static void Init()
        {
            GetWindow<PostProcessZipBuilderWindow>(nameof(PostProcessZipBuilder));
        }

        private void OnGUI()
        {
            CommonGUI();
        }

        public static void CommonGUI()
        {
            ShowTitle("Zip");
            Settings.Zip = EditorGUILayout.Toggle("Make Zip", Settings.Zip);

            GUI.enabled             = Settings.Zip;
            Settings.Overwrite      = EditorGUILayout.Toggle("Overwrite Zip",    Settings.Overwrite);
            Settings.IncludeBaseDir = EditorGUILayout.Toggle("Include Base Dir", Settings.IncludeBaseDir);
            Settings.RemoveBaseDir  = EditorGUILayout.Toggle("Remove Base Dir",  Settings.RemoveBaseDir);
            Settings.RenameZip      = (Settings.RenameType)EditorGUILayout.EnumPopup("Rename Zip", Settings.RenameZip);
            Settings.RenameFormat   = EditorGUILayout.TextField("Rename Format", Settings.RenameFormat);
            GUI.enabled             = true;

            EditorGUILayout.Space();

            ShowTitle("StreamingAssets");
            Settings.RemoveStreamingAssets = EditorGUILayout.Toggle("Remove StreamingAssets", Settings.RemoveStreamingAssets);

            EditorGUILayout.Space();

            ShowTitle("General");
            Settings.ShowDialogWhenBuild = EditorGUILayout.Toggle("Show Dialog When Build", Settings.ShowDialogWhenBuild);

            if (GUI.changed)
            {
                Settings.SaveSettings();
            }
        }

        private static void ShowTitle(string title)
        {
            EditorGUILayout.LabelField("\u25A0 " + title, EditorStyles.boldLabel);
        }
    }

    public class PostProcessZipBuilderModalWindow : EditorWindow
    {
        private void OnGUI()
        {
            PostProcessZipBuilderWindow.CommonGUI();
            EditorGUILayout.Space();
            if (GUILayout.Button("OK"))
            {
                Close();
            }
        }
    }
}