using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Core.Capabilities;
using K4SharedApi;

namespace K4SystemMMRanks
{
	public sealed class PluginConfig : BasePluginConfig
	{
		[JsonPropertyName("mode")]
		public int Mode { get; set; } = 1;

		[JsonPropertyName("ConfigVersion")]
		public override int Version { get; set; } = 1;
	}

	[MinimumApiVersion(200)]
	public sealed partial class PluginK4SystemMMRanks : BasePlugin, IPluginConfig<PluginConfig>
	{

		public override string ModuleName => "K4-System Matchmaking Ranks";
		public override string ModuleVersion => "1.0.1";
		public override string ModuleAuthor => "K4ryuu";

		public required PluginConfig Config { get; set; } = new PluginConfig();

		public void OnConfigParsed(PluginConfig config)
		{
			if (config.Version < Config.Version)
			{
				base.Logger.LogWarning("Configuration version mismatch (Expected: {0} | Current: {1})", this.Config.Version, config.Version);
			}

			this.Config = config;
		}

		public static PlayerCapability<IPlayerAPI> Capability_SharedAPI { get; } = new("k4-system:sharedapi-player");

		public override void Load(bool hotReload)
		{
			VirtualFunctions.CCSPlayerPawnBase_PostThinkFunc.Hook(_ =>
			{
				if (Config.Mode == 0)
					return HookResult.Continue;

				Utilities.GetPlayers().Where(p => p?.IsValid == true && p.PlayerPawn?.IsValid == true && !p.IsBot && !p.IsHLTV && p.SteamID.ToString().Length == 17)
					.ToList()
					.ForEach(p =>
					{
						IPlayerAPI? apiHandler = Capability_SharedAPI.Get(p);

						if (apiHandler == null)
							return;

						if (!apiHandler.IsLoaded || !apiHandler.IsValid || !apiHandler.IsPlayer)
							return;

						int rankId = apiHandler.RankID;
						int points = apiHandler.Points;

						p.CompetitiveRankType = (sbyte)(Config.Mode == 1 ? 11 : 12);
						p.CompetitiveRanking = Config.Mode == 1 ? points : rankId >= 19 ? 18 : rankId;
					});

				return HookResult.Continue;
			}, HookMode.Post);
		}
	}
}
