using System.IO;
using Unity.Build;
using BuildReport = UnityEditor.Build.Reporting.BuildReport;

namespace Unity.Platforms.MacOS.Build
{
    [BuildStep(Description = "Producing macOS Artifacts")]
    sealed class MacOSProduceArtifactStep : BuildStepBase
    {
        public override BuildResult Run(BuildContext context)
        {
            var report = context.GetValue<BuildReport>();
            if (report == null)
            {
                return context.Failure($"Could not retrieve {nameof(BuildReport)} from build context.");
            }

            var artifact = context.GetOrCreateValue<MacOSArtifact>();
            artifact.OutputTargetFile = new FileInfo(report.summary.outputPath);
            return context.Success();
        }
    }
}
