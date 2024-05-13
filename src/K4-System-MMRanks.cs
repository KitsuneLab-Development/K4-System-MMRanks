using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
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

	[MinimumApiVersion(227)]
	public sealed partial class PluginK4SystemMMRanks : BasePlugin, IPluginConfig<PluginConfig>
	{

		public override string ModuleName => "K4-System Matchmaking Ranks";
		public override string ModuleVersion => "1.0.4";
		public override string ModuleAuthor => "K4ryuu";

		public bool AllowUpdate = false;

		public required PluginConfig Config { get; set; } = new PluginConfig();

		public void OnConfigParsed(PluginConfig config)
		{
			if (config.Version < Config.Version)
				base.Logger.LogWarning("Configuration version mismatch (Expected: {0} | Current: {1})", this.Config.Version, config.Version);

			this.Config = config;
		}

		public static PlayerCapability<IPlayerAPI> Capability_SharedAPI { get; } = new("k4-system:sharedapi-player");

		public override void Load(bool hotReload)
		{
			if (hotReload)
				AllowUpdate = true;

			RegisterEventHandler((EventRoundStart @event, GameEventInfo info) => { AllowUpdate = true; return HookResult.Continue; });
			RegisterEventHandler((EventCsWinPanelMatch @event, GameEventInfo info) => { AllowUpdate = false; return HookResult.Continue; });

			RegisterListener<Listeners.OnTick>(() =>
			{
				if (Config.Mode == 0 || !AllowUpdate)
					return;

				Utilities.GetPlayers().ForEach(p =>
				{
					IPlayerAPI? h = Capability_SharedAPI.Get(p);
					if (h?.IsLoaded == true && h.IsValid && h.IsPlayer)
					{
						int rankId = h.RankID;
						p.CompetitiveWins = 10;
						p.CompetitiveRankType = (sbyte)(Config.Mode == 1 ? 11 : 12);
						p.CompetitiveRanking = Config.Mode == 1 ? h.Points : rankId >= 19 ? 18 : rankId;
						Utilities.SetStateChanged(p, "CCSPlayerController", "m_iCompetitiveRankType");
					}
				});
			});
		}
	}
}
