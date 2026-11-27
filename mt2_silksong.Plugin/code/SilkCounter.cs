using System.Collections;
using TrainworksReloaded.Core;
using BepInEx;
using Conductor.Extensions;
using Conductor.TrackedValues;
using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core.Interfaces;
using UnityEngine;

namespace mt2_silksong.Plugin.code;

public abstract class SilkCounter
{
    public static CardStatistics.TrackedValueType silkCounter;
    
    //private static int _silk;

    public abstract int GetValue(CardStatistics.TrackedValueType statValueData);

    public abstract void IncrementValue(CardState? card, int amount,
        CardStatistics.EntryDuration entryDuration = CardStatistics.EntryDuration.ThisTurn);

    public abstract void Reset();

    /*public delegate int TrackedValueGetter(CardStatistics.StatValueData statValueData,
        IReadOnlyDictionary<CardState, CardStatsEntry> deckStats, ICoreGameManagers coreGameManagers);*/

    /*static int getSilk(CardStatistics.StatValueData statValueData,
        IReadOnlyDictionary<CardState, CardStatsEntry> deckStats, ICoreGameManagers coreGameManagers)
    {
        return coreGameManagers.GetCardStatistics().GetStatValue(statValueData);
    }*/

    //private TrackedValueGetter test = getSilk;
    
    [HarmonyPatch(typeof(SaveManager), "IncrementRunStat")]
    internal class SilkCounterPatch
    {
        public static void Postfix(RunStat.StatType statType, CardStatistics ___cardStatistics)
        {
            if (statType == RunStat.StatType.DamageDone)
            {
                ___cardStatistics.IncrementStat(null, silkCounter, 1);
                Plugin.Logger.LogMessage("Damage done, did SilkCounter go up?");
            }
        }
    }
    
    static void Main(string[] args)
    {
        Railend.ConfigurePostAction(
            c =>
            {
                var trackedValueRegister = c.GetInstance<IRegister<CardStatistics.TrackedValueType>>();
                silkCounter = trackedValueRegister.GetValueOrDefault(MyPluginInfo.PLUGIN_GUID.GetId(TemplateConstants.TrackedValueTypeEnum, "SilkCounter"));
                silkCounter.SetTrackedValueHandler(new SimpleGlobalTrackedValueHandler());
            });
    }
}