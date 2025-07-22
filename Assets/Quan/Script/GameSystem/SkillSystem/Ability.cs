using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseAbility
{
    float getDame();
    void useAbility(Ability ability,Transform skillPosition, Transform skillParent);
    void destroyAbility(Ability ability);
}
[CreateAssetMenu(menuName = "Ability/Ability")]
public class Ability:ScriptableObject,IBaseAbility
{
    public Ability(float aDamage, string aName, string aDescription, float aCooldown, Sprite aIcon, int aLevel, int aCost, bool isLocked)
    {
        this.aDamage = aDamage;
        this.aName = aName;
        this.aDescription = aDescription;
        this.aCooldown = aCooldown;
        this.aIcon = aIcon;
        this.aLevel = aLevel;
        this.aCost = aCost;
        this.isLocked = isLocked;
    }

    public float aDamage;
    public string aName;
    public string aDescription;
    public float aCooldown;
    public Sprite aIcon;
    public int aLevel;
    public int aCost;
    public bool isLocked;
    public GameObject abilityObject;
    public float timeSkill;
    
    public float getDame()
    {
        return aDamage;
    }

    public void useAbility(Ability ability, Transform skillPosition, Transform skillParent)
    {
        throw new NotImplementedException();
    }

    public void destroyAbility(Ability ability)
    {
        throw new NotImplementedException();
    }
}
