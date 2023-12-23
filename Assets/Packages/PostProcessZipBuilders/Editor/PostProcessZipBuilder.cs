using UnityEditor;
using UnityEngine;

namespace PostProcessZipBuilders
{
    public static class PostProcessZipBuilder
    {
        #region Field

        public enum RenameType
        {
            None,
            Prefix,
            Suffix,
            Replace
        }

        public static bool ShowDialogWhenBuild = true;

        public static RenameType RenameZip    = RenameType.None;
        public static string     RenameFormat = "MMddHHmm";

        public static bool Zip            = true;
        public static bool Overwrite      = true;
        public static bool IncludeBaseDir = true;
        public static bool RemoveBaseDir  = false;

        public static bool RemoveStreamingAssets = false;

        #endregion Field

        #region Method

        public static void OnGUI()
        {
            ShowTitle("Zip");
            Zip = EditorGUILayout.Toggle("Make Zip", Zip);

            GUI.enabled    = Zip;
            Overwrite      = EditorGUILayout.Toggle("Overwrite Zip",    Overwrite);
            IncludeBaseDir = EditorGUILayout.Toggle("Include Base Dir", IncludeBaseDir);
            RemoveBaseDir  = EditorGUILayout.Toggle("Remove Base Dir",  RemoveBaseDir);
            RenameZip      = (RenameType)EditorGUILayout.EnumPopup("Rename Zip", RenameZip);
            RenameFormat   = EditorGUILayout.TextField("Rename Format",RenameFormat);
            GUI.enabled    = true;

            EditorGUILayout.Space();

            ShowTitle("StreamingAssets");
            RemoveStreamingAssets = EditorGUILayout.Toggle("Remove StreamingAssets", RemoveStreamingAssets);

            EditorGUILayout.Space();

            ShowTitle("General");
            ShowDialogWhenBuild = EditorGUILayout.Toggle("Show Dialog When Build", ShowDialogWhenBuild);
        }

        private static void ShowTitle(string title)
        {
            EditorGUILayout.LabelField("\u25A0 " + title, EditorStyles.boldLabel);
        }
    }

    public class PostProcessZipBuilderEditorWindow : EditorWindow
    {
        [MenuItem("Custom/" + nameof(PostProcessZipBuilder))]
        private static void Init()
        {
            GetWindow<PostProcessZipBuilderEditorWindow>(nameof(PostProcessZipBuilder));
        }

        private void OnGUI()
        {
            PostProcessZipBuilder.OnGUI();
        }
    }

    public class PostProcessZipBuilderModalWindow : EditorWindow
    {
        private void OnGUI()
        {
            PostProcessZipBuilder.OnGUI();
            EditorGUILayout.Space();
            if (GUILayout.Button("OK"))
            {
                Close();
            }
        }
    }

    #endregion Method
}