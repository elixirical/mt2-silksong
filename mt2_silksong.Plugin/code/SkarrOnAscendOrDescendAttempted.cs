using Conductor.Extensions;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;

namespace mt2_silksong.Plugin.code;

public static class OnAscendOrDescendAttempted
{
    public static bool SkarrBugsnaxPlayedTrigger(TriggerOnCardPlayedParams data, out QueueTriggerParams? triggerQueueData)
    {
        if (data.Card.GetAssetName().ExtractId(MyPluginInfo.PLUGIN_GUID, TemplateConstants.Card) == "SkarrBugSnack")
        {
            triggerQueueData = new QueueTriggerParams
            {
                fireTriggersData = new CharacterState.FireTriggersData
                {
                    paramInt = 1
                }
            };
            return true;
        }
        triggerQueueData = null;
        return false;
    }
}