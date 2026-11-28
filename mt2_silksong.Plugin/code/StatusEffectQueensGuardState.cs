namespace mt2_silksong.Plugin.code;

public sealed class StatusEffectQueensGuardState : StatusEffectState
{
    public override bool TestTrigger(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
    {
        outputTriggerParams.damage = inputTriggerParams.damage;
        CharacterState attacker = inputTriggerParams.attacker;
        CharacterState attacked = inputTriggerParams.attacked;
        if (attacker == null || attacked == null || !attacked.IsAlive)
        {
            return false;
        }
        if (inputTriggerParams.damage > 0 && attacked.HasStatusEffect("dazed"))
        {
            int statusEffectStacks = attacker.GetStatusEffectStacks(GetStatusId());
            outputTriggerParams.damage = inputTriggerParams.damage * GetDamageMultiplier(statusEffectStacks);
        }
        return outputTriggerParams.damage != inputTriggerParams.damage;
    }

    public override int GetEffectMagnitude(int stacks = 1)
    {
        return GetDamageMultiplier(stacks);
    }

    private int GetDamageMultiplier(int stacks)
    {
        return stacks + 1;
    }
}