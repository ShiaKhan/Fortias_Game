using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class HealerAI : IAIStrategy
{
    public void ExecuteTurn(Character self, List<Character> targets)
    {
        var allyWithLowestHP = targets.Where(t => t.Team == self.Team).OrderBy(t => t.Health).First();
        self.UseAbility("Heal", allyWithLowestHP);
    }
}

public class Character : MonoBehaviour
{
    public AggressiveAI AIStrategy;
    public List<Character> Team;
    public CharacterStats Stats;
    public List<Ability> Abilities;
    public Ability CurrentAbility;
    public float Health;
    public Bar_Controller _barCharacter;
    private HeroesDataSO _heroesDataSO;
    public float currenMP;
    public PlayerView _ownerView;
    public List<Character> Targets;
    public float attackRange = 1.5f; // Khoảng cách tấn công
    public float attackCooldown = 1.0f; // Thời gian hồi chiêu giữa các đòn tấn công
    public float lastAttackTime = 0f;
    void Update()
    {

    }
    public void AutoPlayTurn(List<Character> targets)
    {
        AIStrategy?.ExecuteTurn(this, targets);
    }

    public void setHeroesDataSO(HeroesDataSO heroesDataSO) => this._heroesDataSO = heroesDataSO;
    public void UseAbility(string abilityName, Character target)
    {
        foreach (Ability ability in Abilities)
        {
            if (ability.aName == abilityName)
            {
                var abilityObject = Instantiate(ability.abilityObject, target.transform.position, Quaternion.identity, this.transform);
                Destroy(abilityObject, ability.timeSkill);
            }
        }
    }


    public void TakeDamage(float damage)
    {
        Health -= damage;
        updateBar(Health, currenMP, Stats.Hp, Stats.Mp);
        if (Health <= damage)
        {
            isDead(this);
        }
    }

    public void Heal(float health)
    {
        Health += health;
        updateBar(Health, currenMP, Stats.Hp, Stats.Mp);
    }

    public void InitCharacter(HeroesDataSO heroesDataSO, PlayerView ownerView)
    {
        Debug.Log("Init Character: " + heroesDataSO.name);
        this._heroesDataSO = heroesDataSO;
        Stats = new CharacterStats(heroesDataSO.maxHP, heroesDataSO.maxHP, heroesDataSO.attack, heroesDataSO.physicRes, heroesDataSO.magicRes);
        Abilities = heroesDataSO.skills;
        Health = heroesDataSO.maxHP;
        currenMP = heroesDataSO.maxMP;
        _barCharacter = GetComponentInChildren<Bar_Controller>();
        _ownerView = ownerView;
        AIStrategy = new AggressiveAI(); // Hoặc HealerAI tùy vào loại AI bạn muốn sử dụng
        updateBar(Health, currenMP, Stats.Hp, Stats.Mp);
        this.gameObject.name = heroesDataSO.heroName;
    }

    public void updateBar(float hp, float mp, float maxHP, float maxMP)
    {
        _barCharacter.hpBar.fillAmount = hp / maxHP;
        _barCharacter.mpBar.fillAmount = mp / maxMP;
    }

    public List<Character> DetectObjectsAround2D(float radius, LayerMask layerMask)
    {
        List<Character> detectedObjects = new List<Character>();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != this.gameObject)
            {
                var character = hitCollider.GetComponent<Character>();
                if (character != null)
                {
                    detectedObjects.Add(character);
                }
            }
        }
        return detectedObjects;
    }

    private void OnDrawGizmosSelected()
    {
        // Đặt màu cho gizmo
        Gizmos.color = Color.yellow;
        // Vẽ vòng tròn (2D) quanh vị trí nhân vật
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void autoPlay(List<Character> Targets)
    {
        if (_ownerView.isAutoPlay && Targets.Count > 0)
        {
            AIStrategy.AutoTarget(this, Targets);
        }
        else
        {
            Debug.Log("AutoPlay is disabled or no targets available for " + this.name);
            _ownerView.isAutoPlay = false; // Tắt AutoPlay nếu không có mục tiêu
        }
        
    }
    public void isDead(Character character)
    {
        Debug.Log("Character is dead: " + character.name);
        _ownerView.updateTeam(character);
        Destroy(character.gameObject);

    }
    public void getTeams()
    {
        this.Team = _ownerView.getTeam();
    }
    
     
}