using System.Collections.Generic;
using UnityEngine;

public abstract class aCommand
{
    public abstract void Execute();
}
public class SkillCommand : MonoBehaviour, IBaseAbility
{
    public List<Ability> Abilities;
    public float getDame()
    {
        throw new System.NotImplementedException();
    }

    public void useAbility(Ability ability, Transform skillPosition, Transform skillParent)
    {
        throw new System.NotImplementedException();
    }

    public void destroyAbility(Ability ability)
    {
        throw new System.NotImplementedException();
    }
    
}
