public interface ICombatCommand
{
    void Execute();
    void Undo();
}
public class CombatCommand : ICombatCommand
{
    private Character _attacker;
    private Character _target;
    private int _damage;
    public CombatCommand(Character attacker, Character target)
    {
        _attacker = attacker;
        _target = target;
    }

    public void Execute()
    {
        _damage = CalculateDamage();
        _target.TakeDamage(_damage);
        CombatEventManager.TriggerAbilityUsed(_attacker.CurrentAbility);
    }

    public void Undo() => _target.Heal(_damage);

    public int CalculateDamage()
    {
        return _damage;
    }
}
