<a name="readme-top"></a>

![GitHub tag (with filter)](https://img.shields.io/github/v/tag/KitsuneLab-Development/K4-System-MMRanks?style=for-the-badge&label=Version)
![GitHub Repo stars](https://img.shields.io/github/stars/KitsuneLab-Development/K4-System-MMRanks?style=for-the-badge)
![GitHub issues](https://img.shields.io/github/issues/KitsuneLab-Development/K4-System-MMRanks?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/KitsuneLab-Development/K4-System-MMRanks?style=for-the-badge)
![GitHub all releases](https://img.shields.io/github/downloads/KitsuneLab-Development/K4-System-MMRanks/total?style=for-the-badge)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/KitsuneLab-Development/K4-System-MMRanks/dev?style=for-the-badge)

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/KitsuneLab-Development/K4-System-MMRanks">
    <img src="https://i.imgur.com/sej1ZzD.png" alt="Logo" width="400" height="256">
  </a>
  <h3 align="center">CounterStrike2 | K4-System @ Matchmaking Ranks</h3>
  <p align="center">
    The plugin adds scoreboard ranks for the players based on their <a href="https://github.com/KitsuneLab-Development/K4-System">K4-System</a> ranks. Support premier point display and regular rank displays.
    <br />
    <a href="https://github.com/KitsuneLab-Development/K4-System-MMRanks/releases">Download</a>
    ·
    <a href="https://github.com/KitsuneLab-Development/K4-System-MMRanks/issues/new?assignees=KitsuneLab-Development&labels=bug&template=bug_report.md&title=%5BBUG%5D">Report Bug</a>
    ·
    <a href="https://github.com/KitsuneLab-Development/K4-System-MMRanks/issues/new?assignees=KitsuneLab-Development&labels=enhancement&template=feature_request.md&title=%5BREQ%5D">Request Feature</a>
     ·
    <a href="https://kitsune-lab.com">Website</a>
     ·
    <a href="https://nests.kitsune-lab.com/tickets/create?department_id=2">Hire Us</a>
  </p>
</div>

> [!CAUTION]
> This plugin does not adhere to Steam guidelines. Therefore, I do not recommend using it on your server, as it may risk being token banned if Steam decides to enforce such measures again.

> [!WARNING]
> In order to reveal everyone's rank on scoreboard install this great metamod plugin from our team member @Cruze03 at https://github.com/Cruze03/FakeRanks-RevealAll/releases/latest

### Dependencies

To use this server addon, you'll need the following dependencies installed:

- [**CounterStrikeSharp**](https://github.com/roflmuffin/CounterStrikeSharp/releases): CounterStrikeSharp allows you to write server plugins in C# for Counter-Strike 2/Source2/CS2.
- [**K4-System**](https://github.com/KitsuneLab-Development/K4-System): Standalone plugin to add statistics, ranks, playtime tracker and more to your server.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Supported Modes

- 0 - Disabled
- 1 - Prime Points
- 2 - MM Rank Images
- 3 - Wingman Rank Images
- 4 - DangerZone Rank Images **[Does not work right now]**
- 5 - [Custom Rank Images](https://github.com/KitsuneLab-Development/K4-System-MMRanks?tab=readme-ov-file#custom-ranks)

### Custom Ranks
- Upload files in your addon with the path: `panorama/images/icons/skillgroups/`
- Images should be in SVG file format and should have `skillgroup` prefix. For eg. `skillgroup19000051.svg` (When you compile addon, it should automatically convert file to VSVG)
- Make sure to edit `RankBase` and `RankMax` in the config file. If your custom ranks start with `19000051` and if you have 20 ranks, then `RankBase` will be `19000051` and `RankMax` will be `20`.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->
## Roadmap

- [ ] Rework the addon fully

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- AUTHORS -->
## Authors

- [**K4ryuu**](https://github.com/K4ryuu) - *Initial work*
- [**Cruze**](https://github.com/Cruze03) - *Metamod part of the plugin*

See also the list of [contributors](https://github.com/KitsuneLab-Development/K4-System-MMRanks/graphs/contributors) who participated in this project as an outside contributor.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the GPL-3.0 License. See `LICENSE.md` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

- **KitsuneLab Team** - [contact@kitsune-lab.com](mailto:contact@kitsune-lab.com)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
