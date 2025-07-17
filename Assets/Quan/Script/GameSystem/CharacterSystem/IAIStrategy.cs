using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IAIStrategy
{
    void ExecuteTurn(Character self, List<Character> targets);
}

public class AggressiveAI : IAIStrategy
{
    public void ExecuteTurn(Character self, List<Character> targets)
    {
        var weakestEnemy = targets.OrderBy(t => t.Health).First();
        self.UseAbility("BasicAttack", weakestEnemy);
    }

    public void AutoTarget(Character self, List<Character> targets)
    {
        foreach (var target in targets)
        {
            if (Vector2.Distance(self.transform.position, target.transform.position) <= self.attackRange)
            {
                if (target != null && Time.time - self.lastAttackTime >= self.attackCooldown)
                {
                    //Add animation attack
                    //self.UseAbility("BasicAttack", target);
                    self.GetComponent<Animator>().SetTrigger("Atk");
                    target.TakeDamage(self.Stats.Atk);
                    self.lastAttackTime = Time.time;
                    Debug.Log($"Attacking {target.name} with {self.Stats.Atk} damage.");
                }
                else
                {
                    Debug.Log($"Target {target.name} is out of range or on cooldown.");
                }

            }
            else
            {
                self.GetComponent<PlayerMovement>()._controller.MoveToPositionSmooth(target.transform.position);
                //Add animation move 
            }
        }
    }
}