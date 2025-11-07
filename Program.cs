using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using Range = SemanticVersioning.Range;

namespace TEP300headset;

public record ModMetadata : AbstractModMetadata
{
    public override List<string>? Incompatibilities { get; init; }
    public override string ModGuid { get; init; } = "com.nem.tep300backport";
    public override string Name { get; init; } = "Peltor TEP-300 Backport + Fixes";
    public override string Author { get; init; } = "nem";
    public override Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~2.0.0") }
    };
    public override string? Url { get; init; } = "https://github.com/Dight67/TEP-300-Backport-Fixes";
    public override List<string>? Contributors { get; init; }
    public override SemanticVersioning.Version Version { get; init; } = new("1.0.2");
    public override Range SptVersion { get; init; } = new("~4.0.3");
    public override string License { get; init; } = "MIT";
    public override bool? IsBundleMod { get; init; } = true;
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class TEP300HeadsetBackport(
    WTTServerCommonLib.WTTServerCommonLib wttCommon,
    ISptLogger<TEP300HeadsetBackport> logger
) : IOnLoad
{
    public async Task OnLoad()
    {
        // Get your current assembly
        var assembly = Assembly.GetExecutingAssembly();

        // Use WTT-CommonLib services
        await wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly, Path.Join("db", "CustomItems"));
        logger.Success("[TEP-300] TEP-300 backport and fixes loaded.");
        await Task.CompletedTask;
    }
}