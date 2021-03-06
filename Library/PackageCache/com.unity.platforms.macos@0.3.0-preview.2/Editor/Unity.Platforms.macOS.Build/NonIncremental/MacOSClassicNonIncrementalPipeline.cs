using System.Diagnostics;
using System.IO;
using Unity.Build;
using Unity.Build.Classic.Private;
using Unity.Build.Common;
using Unity.BuildSystem.NativeProgramSupport;
using UnityEditor;

namespace Unity.Platforms.MacOS.Build
{
    sealed class MacOSClassicNonIncrementalPipeline : ClassicNonIncrementalPipelineBase
    {
        protected override BuildTarget BuildTarget { get; } = BuildTarget.StandaloneOSX;
        public override Platform Platform => new MacOSXPlatform();

        public override BuildStepCollection BuildSteps { get; } = new[]
        {
            typeof(SaveScenesAndAssetsStep),
            typeof(ApplyUnitySettingsStep),
            typeof(SwitchPlatfomStep),
            typeof(BuildPlayerStep),
            typeof(CopyAdditionallyProvidedFilesStep),
            typeof(MacOSProduceArtifactStep)
        };

        protected override BoolResult OnCanRun(RunContext context)
        {
            var artifact = context.GetLastBuildArtifact<MacOSArtifact>();
            if (artifact == null)
            {
                return BoolResult.False($"Could not retrieve build artifact '{nameof(MacOSArtifact)}'.");
            }

            if (artifact.OutputTargetFile == null)
            {
                return BoolResult.False($"{nameof(MacOSArtifact.OutputTargetFile)} is null.");
            }

            // On macOS, the output target is a .app directory structure
            if (!Directory.Exists(artifact.OutputTargetFile.FullName))
            {
                return BoolResult.False($"Output target file '{artifact.OutputTargetFile.FullName}' not found.");
            }

            return BoolResult.True();
        }

        protected override RunResult OnRun(RunContext context)
        {
            var artifact = context.GetLastBuildArtifact<MacOSArtifact>();
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "open",
                    Arguments = '\"' + artifact.OutputTargetFile.FullName.Trim('\"') + '\"',
                    WorkingDirectory = artifact.OutputTargetFile.Directory?.FullName ?? string.Empty,
                    CreateNoWindow = true,
                    UseShellExecute = true
                }
            };

            if (!process.Start())
            {
                return context.Failure($"Failed to start process at '{process.StartInfo.FileName}'.");
            }

            return context.Success(new MacOSRunInstance(process));
        }

        protected override void PrepareContext(BuildContext context)
        {
            base.PrepareContext(context);
            var classicData = context.GetValue<ClassicSharedData>();
            classicData.StreamingAssetsDirectory = $"{context.GetOutputBuildDirectory()}/{context.GetComponentOrDefault<GeneralSettings>().ProductName}.app/Contents/Resources/Data/StreamingAssets";
        }
    }
}
