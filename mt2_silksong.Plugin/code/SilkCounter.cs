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

public abstract class SilkCounter : AbstractTrackedValueHandler
{
    /*private int _silk;

    private void incrementSilk(int increment)
    {
        int sum = _silk + increment; 
        if (sum > 0 | sum <= 14)
        {
            _silk = sum;
        }
    }*/
    //public abstract int GetValue(CardStatistics.StatValueData statValueData);
    //public abstract void IncrementValue(CardState? card, int amount, CardStatistics.EntryDuration entryDuration = CardStatistics.EntryDuration.ThisTurn);
    
    public static CardStatistics.TrackedValueType silkCounter;

    //public delegate int TrackedValueGetter(CardStatistics.StatValueData statValueData, IReadOnlyDictionary<CardState, CardStatsEntry> deckStats, ICoreGameManagers coreGameManagers);
    
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
    
    private void Awake()
    {
        Railend.ConfigurePostAction(
            c =>
            {
                var trackedValueRegister = c.GetInstance<IRegister<CardStatistics.TrackedValueType>>();
                silkCounter = trackedValueRegister.GetValueOrDefault(MyPluginInfo.PLUGIN_GUID.GetId(TemplateConstants.TrackedValueTypeEnum, "mt2ss_silkcounter"));
                silkCounter.SetTrackedValueHandler(new SimpleGlobalTrackedValueHandler());
            });
    }
}