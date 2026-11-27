using BepInEx;
using BepInEx.Logging;
using Conductor.Extensions;
using Conductor.TrackedValues;
using HarmonyLib;
using mt2_silksong.Plugin.code;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Extensions;
using TrainworksReloaded.Core.Interfaces;

namespace mt2_silksong.Plugin
{
    [BepInPlugin("elixirical.mt2_silksong", "MT2: Silksong", "0.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger = new(MyPluginInfo.PLUGIN_GUID);
        
        // Plugin startup logic. This function is automatically called when your plugin initializes
        public void Awake()
        {
            Logger = base.Logger;

            var builder = Railhead.GetBuilder();
            builder.Configure(
                MyPluginInfo.PLUGIN_GUID,
                c =>
                {
                    // Be sure to include any new json files if you add more.
                    c.AddMergedJsonFile(
                        "json/global.json",
                        "json/tracked_values.json",
                        "json/skarr/champions/karmelita.json",
                        "json/skarr/champions/karmelita_starter.json",
                        "json/skarr/skarr_clan.json",
                        "json/skarr/skarr_clan_banner.json",
                        "json/skarr/skarr_clan_card_frame.json",
                        "json/skarr/skarr_clan_subtypes.json",
                        "json/skarr/units/skarr_unit_skarrswarm.json",
                        "json/skarr/skarr_factionwide.json",
                        "json/skarr/spells/skarr_bug_snack.json",
                        "json/skarr/units/skarr_unit_skarrlid.json",
                        "json/skarr/units/skarr_unit_skarrwing.json",
                        "json/skarr/units/skarr_unit_skarr_scout.json",
                        "json/skarr/units/skarr_unit_skarrgard.json",
                        "json/skarr/units/skarr_unit_skarr_spear.json"
                        // Clan Definition
                        //"json/clan/silksong_clan.json",
                        // Required: Clan Map Banner Node definition and Card Pool
                        //"json/clan/silksong_clan_banner.json",
                        // Optional: Custom Card Border (if removed edit clan.json)
                        //"json/clan/silksong_clan_card_frame.json",
                        // Optional: Custom Clan Subtypes used by units.
                        //"json/clan/silksong_clan_subtypes.json",
                        // Required: A Clan Champion with one Upgrade Path.
                        //"json/champions/silksong_champion.json",
                        // Spell Cards
                        //"json/pharloom_cards/scaling_heal.json",
                        //"json/pharloom_cards/spells/hornet_silk_heal.json"
                        //"json/spells/NotHornBreak.json",
                        //"json/spells/GiveEveryoneArmor.json",
                        //"json/spells/NotRazorsharpEdge.json"
                        // Starter (required).
                        // Units
                        // Non Banner Unit, that is draftable as a Battle Reward.
                        // Equipment
                        // Rooms
                        // Shop Upgrades (Enhancer)
                        // Artifacts
                    );
                }
            );
            Roundhouse.WhenReady(MyPluginInfo.PLUGIN_GUID,
                ctx => 
                { 
                    var trigger = ctx.GetCharacterTriggerEnum("SkarrNibble");
                    trigger.SetToTriggerOnCardPlayed(SkarrOnBugsnaxPlayed.SkarrBugsnaxPlayedTrigger);
                }
            );

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            // Uncomment if you need Harmony Patch support.
            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }
    }
}
