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

		[JsonPropertyName("RankBase")]
		public int RankBase { get; set; } = 0;

		[JsonPropertyName("RankMax")]
		public int RankMax { get; set; } = 0;
		
		[JsonPropertyName("RankMargin")]
		public int RankMargin { get; set; } = 0;

		[JsonPropertyName("ConfigVersion")]
		public override int Version { get; set; } = 2;
	}

	[MinimumApiVersion(227)]
	public sealed partial class PluginK4SystemMMRanks : BasePlugin, IPluginConfig<PluginConfig>
	{

		public override string ModuleName => "K4-System Matchmaking Ranks";
		public override string ModuleVersion => "1.0.5";
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
				if (!AllowUpdate || Config.Mode <= 0 || (Config.Mode > 4 && (Config.RankBase == 0 || Config.RankMax == 0)))
					return;

				Utilities.GetPlayers().ForEach(p =>
				{
					IPlayerAPI? h = Capability_SharedAPI.Get(p);

					if (h?.IsLoaded == true && h.IsValid && h.IsPlayer)
					{
						int rankId = h.RankID;
						int points = h.Points;

						p.CompetitiveWins = 10;
						switch(Config.Mode)
						{
							// Premier
							case 1:
							{
								p.CompetitiveRankType = 11;
								p.CompetitiveRanking = points;
								break;
							}
							// Competitive
							case 2:
							{
								p.CompetitiveRankType = 12;
								p.CompetitiveRanking = rankId >= 19 ? 18 : rankId-1;
								break;
							}
							// Wingman
							case 3:
							{
								p.CompetitiveRankType = 7;
								p.CompetitiveRanking = rankId >= 19 ? 18 : rankId-1;
								break;
							}
							// Danger Zone (!! DOES NOT WORK !!)
							case 4:
							{
								p.CompetitiveRankType = 10;
								p.CompetitiveRanking = rankId >= 16 ? 15 : rankId-1;
								break;
							}
							// Custom Rank
							default:
							{
								int rank = rankId > Config.RankMax ? Config.RankBase+Config.RankMax-Config.RankMargin : Config.RankBase+(rankId-Config.RankMargin-1);
								
								p.CompetitiveRankType = 12;
								
								p.CompetitiveRanking = rank;
								break;
							}
						}
						Utilities.SetStateChanged(p, "CCSPlayerController", "m_iCompetitiveRankType");
					}
				});
			});
		}
	}
}
