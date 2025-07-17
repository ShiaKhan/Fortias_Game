using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AnimationString
{
    Walk,
    Idle,
    Run,
    Skill1,
    Skill2,
    Skill3,
    Skill4,
}

public class SkillDataSO : ScriptableObject
{
    public float dame;
    public GameObject prefab_Skill;
}

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Data/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    public new string name;
    public int _level;
    public int _power;
    public string _currentMap;
    public string _currentTower;
    public List<HeroesDataSO> _heroes;
}
