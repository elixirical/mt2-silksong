using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Extensions;

namespace mt2_silksong.Plugin
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
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
                        // Clan Definition
                        "json/clan/silksong_clan.json",
                        // Required: Clan Map Banner Node definition and Card Pool
                        "json/clan/silksong_clan_banner.json",
                        // Optional: Custom Card Border (if removed edit clan.json)
                        "json/clan/silksong_clan_card_frame.json",
                        // Optional: Custom Clan Subtypes used by units.
                        "json/clan/silksong_clan_subtypes.json",
                        // Required: A Clan Champion with one Upgrade Path.
                        "json/champions/silksong_champion.json",
                        // Spell Cards
                        "json/pharloom_cards/spells/hornet_silk_heal.json",
                        "json/spells/NotHornBreak.json",
                        "json/spells/GiveEveryoneArmor.json",
                        "json/spells/NotRazorsharpEdge.json"
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

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            // Uncomment if you need Harmony Patch support.
            //var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            //harmony.PatchAll();

        }
    }
}
