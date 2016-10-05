#if UNITY_EDITOR
using UnityEditor;
#endif

public class AssetBundlesBuilder
{
#if UNITY_EDITOR
    public static string bundlesFolderName = "StreamingAssets";
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/"+bundlesFolderName, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.WebGL);
    }
#endif
}