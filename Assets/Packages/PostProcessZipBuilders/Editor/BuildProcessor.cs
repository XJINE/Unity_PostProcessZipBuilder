using System;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using CompressionLevel = System.IO.Compression.CompressionLevel;

namespace PostProcessZipBuilders
{
    public class BuildProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            if (PostProcessZipBuilder.ShowDialogWhenBuild)
            {
                var windowSize = new Vector2(350, 275);
                var window = EditorWindow.GetWindow(typeof(PostProcessZipBuilderModalWindow),
                                                    utility:true,
                                                    nameof(PostProcessZipBuilder),
                                                    focus:true);
                    window.minSize = windowSize;
                    window.maxSize = windowSize;
                    window.ShowModal();
            }

            var exeFilePath = report.summary.outputPath;

            RemoveStreamingAssets(exeFilePath);
            Zip                  (exeFilePath);
            RemoveBaseDir        (exeFilePath);
        }

        private static void RemoveStreamingAssets(string exeFilePath)
        {
            if (!PostProcessZipBuilder.RemoveStreamingAssets)
            {
                return;
            }

            var baseDirPath = Path.GetDirectoryName           (exeFilePath);
            var exeFileName = Path.GetFileNameWithoutExtension(exeFilePath);

            var streamingAssetsDirPath = Path.Combine(baseDirPath, exeFileName + "_Data", "StreamingAssets");
            var streamingAssetsDirInfo = new DirectoryInfo(streamingAssetsDirPath)
            {
                Attributes = FileAttributes.Normal
            };
            
            if (streamingAssetsDirInfo.Exists)
            {
                streamingAssetsDirInfo.Delete(recursive:true);
            }
        }

        private static void Zip(string exeFilePath)
        {
            if (!PostProcessZipBuilder.Zip)
            {
                return;
            }

            var baseDirPath  = Path.GetDirectoryName(exeFilePath);
            var baseDirInfo  = new DirectoryInfo(baseDirPath);
            var dateTimeText = DateTime.Now.ToString(PostProcessZipBuilder.RenameFormat);
            var zipFileName  = PostProcessZipBuilder.RenameZip switch
            {
                PostProcessZipBuilder.RenameType.None    => baseDirInfo.Name,
                PostProcessZipBuilder.RenameType.Prefix  => dateTimeText      + baseDirInfo.Name,
                PostProcessZipBuilder.RenameType.Suffix  => baseDirInfo.Name + dateTimeText,
                PostProcessZipBuilder.RenameType.Replace => dateTimeText,
                                                       _ => baseDirInfo.Name
            } + ".zip";

            var zipFilePath = Path.Combine(baseDirInfo.Parent.ToString(), zipFileName);
            
            if (File.Exists(zipFilePath))
            {
                if (PostProcessZipBuilder.Overwrite)
                {
                    File.Delete(zipFilePath);
                }
                else
                {
                    Debug.LogWarning(zipFilePath + " already exists.");
                    return;
                }
            }

            ZipFile.CreateFromDirectory(baseDirPath,
                                        zipFilePath,
                                        CompressionLevel.Optimal,
                                        PostProcessZipBuilder.IncludeBaseDir);
        }

        private static void RemoveBaseDir(string exeFilePath)
        {
            if (!PostProcessZipBuilder.RemoveBaseDir)
            {
                return;
            }

            var baseDirPath = Path.GetDirectoryName(exeFilePath);
            var baseDirInfo = new DirectoryInfo(baseDirPath)
            {
                Attributes = FileAttributes.Normal
            };
            
            baseDirInfo.Delete(recursive:true);
        }
    }
}