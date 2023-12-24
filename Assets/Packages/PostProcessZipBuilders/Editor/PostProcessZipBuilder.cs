using System;
using UnityEditor;

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

        public static bool       Zip            = true;
        public static bool       Overwrite      = true;
        public static bool       IncludeBaseDir = true;
        public static bool       RemoveBaseDir  = false;
        public static RenameType RenameZip      = RenameType.None;
        public static string     RenameFormat   = "MMddHHmm";

        public static bool RemoveStreamingAssets = false;

        public static bool ShowDialogWhenBuild = true;

        #endregion Field

        #region Method

        public static void SaveSettings()
        {
            const string baseName = nameof(PostProcessZipBuilder);
            EditorUserSettings.SetConfigValue($"{baseName}.{nameof(ShowDialogWhenBuild)}",   ShowDialogWhenBuild  .ToString());
            EditorUserSettings.SetConfigValue($"{baseName}.{nameof(Zip)}",                   Zip                  .ToString());
            EditorUserSettings.SetConfigValue($"{baseName}.{nameof(Overwrite)}",             Overwrite            .ToString());
            EditorUserSettings.SetConfigValue($"{baseName}.{nameof(IncludeBaseDir)}",        IncludeBaseDir       .ToString());
            EditorUserSettings.SetConfigValue($"{baseName}.{nameof(RemoveBaseDir)}",         RemoveBaseDir        .ToString());
            EditorUserSettings.SetConfigValue($"{baseName}.{nameof(RenameZip)}",             RenameZip            .ToString());
            EditorUserSettings.SetConfigValue($"{baseName}.{nameof(RenameFormat)}",          RenameFormat         .ToString());
            EditorUserSettings.SetConfigValue($"{baseName}.{nameof(RemoveStreamingAssets)}", RemoveStreamingAssets.ToString());
        }

        [InitializeOnLoadMethod]
        public static void LoadSettings()
        {
            const string baseName = nameof(PostProcessZipBuilder);
            bool.TryParse (EditorUserSettings.GetConfigValue($"{baseName}.{nameof(ShowDialogWhenBuild)}"),   out ShowDialogWhenBuild);
            bool.TryParse (EditorUserSettings.GetConfigValue($"{baseName}.{nameof(Zip)}"),                   out Zip);
            bool.TryParse (EditorUserSettings.GetConfigValue($"{baseName}.{nameof(Overwrite)}"),             out Overwrite);
            bool.TryParse (EditorUserSettings.GetConfigValue($"{baseName}.{nameof(IncludeBaseDir)}"),        out IncludeBaseDir);
            bool.TryParse (EditorUserSettings.GetConfigValue($"{baseName}.{nameof(RemoveBaseDir)}"),         out RemoveBaseDir);
            Enum.TryParse (EditorUserSettings.GetConfigValue($"{baseName}.{nameof(RenameZip)}"),             out RenameZip);
            RenameFormat = EditorUserSettings.GetConfigValue($"{baseName}.{nameof(RenameFormat)}");
            bool.TryParse (EditorUserSettings.GetConfigValue($"{baseName}.{nameof(RemoveStreamingAssets)}"), out RemoveStreamingAssets); ;
        }

        #endregion Method
    }
}