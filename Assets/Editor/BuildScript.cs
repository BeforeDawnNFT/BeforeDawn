using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
// ReSharper disable once ClassNeverInstantiated.Global
public class BuildScript
{
    [MenuItem("Continues Integration/Build WebGL")]
    public static void BuildWebGL()
    {
        Build();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static void Build()
    {
        PlayerSettings.SplashScreen.show = false;
        PlayerSettings.SplashScreen.backgroundColor = Color.black;

        // ReSharper disable once StringLiteralTypo
        PlayerSettings.companyName = "VRcollab";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.WebGL, "com.VRcollab.BeforeDawn");
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
        PlayerSettings.WebGL.nameFilesAsHashes = true;
        PlayerSettings.WebGL.dataCaching = true;
        PlayerSettings.WebGL.decompressionFallback = true;

        var scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray();
        var buildDir = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "build"));
        BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            locationPathName = buildDir,
            target = BuildTarget.WebGL,
            scenes = scenes,
            targetGroup = BuildTargetGroup.WebGL
        });
    }
}