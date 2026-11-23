using TrainworksReloaded.Core;
using BepInEx;
using Conductor.Extensions;
using Conductor.TrackedValues;
using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core.Interfaces;

namespace mt2_silksong.Plugin.code;

public class SilkCounter : BaseUnityPlugin
{
    private static CardStatistics.TrackedValueType _value;

    [HarmonyPatch(typeof(CardEffectDamage), "Apply")]
    internal class SilkCounterPatch
    {
        public static void Postfix()
        {
            Plugin.Logger.LogMessage("Damage registered, silk* increased");
        }
    }
    
    private void Awake()
    {
        Railend.ConfigurePostAction(
            c =>
            {
                var trackedValueRegister = c.GetInstance<IRegister<CardStatistics.TrackedValueType>>();
                _value = trackedValueRegister.GetValueOrDefault(MyPluginInfo.PLUGIN_GUID.GetId(TemplateConstants.TrackedValueTypeEnum, "SilkCounter"));
                _value.SetTrackedValueHandler(new SimpleGlobalTrackedValueHandler());
            });
    }
}