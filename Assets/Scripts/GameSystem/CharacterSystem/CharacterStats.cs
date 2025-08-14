using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public interface IStats
{
    void AddModifier(string effectName, float amount);
    bool RemoveModifier(string effectName);
}

public class CharacterStats : IStats
{
    public string _name;
    private float _HP;
    private float _MP;
    private float _ATK;
    private float _DEF;
    private float _MagicRes;
    public float _currentHP;
    public float _currentMP;
    public float Mp
    {
        get => _MP;
        set => _MP = value;
    }

    public float Atk
    {
        get => _ATK;
        set => _ATK = value;
    }

    public float Def
    {
        get => _DEF;
        set => _DEF = value;
    }

    public float MagicRes
    {
        get => _MagicRes;
        set => _MagicRes = value;
    }


    
    public CharacterStats(float hp, float mp, float atk, float def, float magicRes)
    {
        _HP = hp;
        _MP = mp;
        _ATK = atk;
        _DEF = def;
        _MagicRes = magicRes;
        _currentHP = hp;
        _currentMP = mp;
    }

    public float Hp
    {
        get => _HP;
        set => _HP = value;
    }
    
    public void AddModifier(string effectName, float amount)
    {
        throw new NotImplementedException();
    }

    public bool RemoveModifier(string effectName)
    {
        throw new NotImplementedException();
    }

    public void UpdateHP(float hp)
    {
        _currentHP -= hp;
    }

    public void UpdateMP(float mp)
    {
        _currentMP -= mp;
    }

    public void UpdateStats(float hp, float mp, float atk, float def, float magicRes)
    {
        _HP += hp;
        _MP += mp;
        _ATK += atk;
        _DEF += def;
        _MagicRes += magicRes;
        _currentHP += hp;
        _currentMP += mp;
    }
}
