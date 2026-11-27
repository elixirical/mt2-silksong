using Conductor.Extensions;
using Conductor.TrackedValues;
using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;

namespace mt2_silksong.Plugin.code;

public abstract class RoamDirection
{
    public static CardStatistics.TrackedValueType roamDirection;

    private int _n;

    private int _randn()
    {
        int tempn = RandomManager.Range(0, 1, RngId.BattleTest);
        Plugin.Logger.LogMessage(_n + " to " + tempn);
        return tempn;
    }
    
    public int GetValue(CardStatistics.StatValueData statValueData)
    {
        return _n;
    }

    public void IncrementValue(CardState? card, int amount,
        CardStatistics.EntryDuration entryDuration = CardStatistics.EntryDuration.ThisTurn)
    {
        _n = _randn();
    }

    public void Reset()
    {
        _n = _randn();
    }
    
    [HarmonyPatch(typeof(CharacterState), "RunPostCombatStatusEffects")]
    internal class RoamDirectionPatch
    {
        public static void Prefix(CardState ___spawnerCard, AllGameManagers ___allGameManagers)
        {
            ___allGameManagers.GetCoreManagers().GetCardStatistics().IncrementStat(___spawnerCard, roamDirection, 1);
        }
    }
    
    public void Awake()
    {
        Railend.ConfigurePostAction(
            c =>
            {
                var trackedValueRegister = c.GetInstance<IRegister<CardStatistics.TrackedValueType>>();
                roamDirection = trackedValueRegister.GetValueOrDefault(MyPluginInfo.PLUGIN_GUID.GetId(TemplateConstants.TrackedValueTypeEnum, "RoamDirection"));
                roamDirection.SetTrackedValueHandler(new SimpleGlobalTrackedValueHandler());
            });
    }
    
}