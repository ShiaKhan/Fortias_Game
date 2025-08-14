using System.Collections.Generic;

public interface ICharacterEffect
{
    void Apply(Character character);
    void Remove(Character character);
}

public class CharacterEffect : ICharacterEffect
{
    public void Apply(Character character) => character.Stats.AddModifier("Poison", -5);
    public void Remove(Character character) => character.Stats.RemoveModifier("Poison");
}

// Composite để quản lý nhiều effect cùng lúc
public class EffectComposite : ICharacterEffect
{
    private List<ICharacterEffect> _effects = new List<ICharacterEffect>();
    public void AddEffect(ICharacterEffect effect) => _effects.Add(effect);
    public void Apply(Character character) => _effects.ForEach(e => e.Apply(character));
    public void Remove(Character character) => _effects.ForEach(e => e.Remove(character));
}