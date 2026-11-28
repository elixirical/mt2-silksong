using Conductor.Extensions;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;

namespace mt2_silksong.Plugin.code;

public class SkarrQueensGuardTrait : CardTraitState
{
    public override PropDescriptions CreateEditorInspectorDescriptions()
    {
        return new PropDescriptions
        {
        };
    }

    public override int OnApplyingDamage(CardTraitState.ApplyingDamageParameters damageParams,
        ICoreGameManagers coreGameManagers)
    {
        if (IsDazed(damageParams.targetCharacter))
        {
            return damageParams.damage * 2;
        }
        else return damageParams.damage;
    }

    public bool IsDazed(CharacterState target)
    {
        return target.HasStatusEffect("dazed") == true;
    }

    public override string GetCardText()
    {
        var text = LocalizeTraitKey("SkarrQueensGuardTrait_CardText");
        var associatedCard = GetCard();
        if (associatedCard == null)
            return string.Empty;
        return string.Format(text);
    }
    
    public override string GetCardTooltipText()
    {
        return string.Format("SkarrQueensGuardTrait_TooltipText".Localize());
    }
}