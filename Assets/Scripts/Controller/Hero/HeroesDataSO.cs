using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/HeroesDataSO")]
public class HeroesDataSO : ScriptableObject
{
    public string heroName;
    public string rarity;
    public string role;
    public float attack;
    public float physicRes;
    public float magicRes;
    public float maxHP;
    public float maxMP;
    public List<Ability> skills;
}
