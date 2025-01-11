using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class RemoveUnusedAssets
{
    [MenuItem("Tools/Cleanup/Remove Unused Assets")]
    public static void RemoveAssets()
    {
        // Step 1: Get all assets in the project
        string[] allAssets = AssetDatabase.GetAllAssetPaths();
        HashSet<string> usedAssets = new HashSet<string>();

        // Step 2: Collect dependencies from scenes included in the build
        var scenesInBuild = EditorBuildSettings.scenes;
        foreach (var scene in scenesInBuild)
        {
            if (scene.enabled)
            {
                string[] dependencies = AssetDatabase.GetDependencies(scene.path, true);
                foreach (string dependency in dependencies)
                {
                    usedAssets.Add(dependency);
                }
            }
        }

        // Step 3: Identify unused assets, excluding specific folders/files
        List<string> unusedAssets = new List<string>();
        foreach (string asset in allAssets)
        {
            if (!usedAssets.Contains(asset) &&
                !AssetDatabase.IsValidFolder(asset) &&
                !asset.Contains("/Editor/") && // Exclude editor scripts
                !asset.Contains("RemoveUnusedAssets.cs") // Exclude this script
            )
            {
                unusedAssets.Add(asset);
            }
        }

        // Step 4: Confirm and delete unused assets
        if (unusedAssets.Count > 0)
        {
            Debug.Log($"Found {unusedAssets.Count} unused assets. Preparing to delete...");

            // Create a log file for deleted assets
            string logPath = "Assets/DeletedAssetsLog.txt";
            using (StreamWriter writer = new StreamWriter(logPath))
            {
                writer.WriteLine("Deleted Assets:");
                foreach (string asset in unusedAssets)
                {
                    // Log the deleted asset
                    writer.WriteLine(asset);

                    // Delete the asset
                    bool success = AssetDatabase.DeleteAsset(asset);
                    if (success)
                        Debug.Log($"Deleted: {asset}");
                    else
                        Debug.LogError($"Failed to delete: {asset}");
                }
            }

            Debug.Log($"Unused assets deleted. Log saved to {logPath}");
            AssetDatabase.Refresh(); // Refresh the AssetDatabase
        }
        else
        {
            Debug.Log("No unused assets found.");
        }
    }
}
