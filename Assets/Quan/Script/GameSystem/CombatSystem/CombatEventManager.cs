using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEventManager : MonoBehaviour
{
    public static event Action<Character> OnCharacterDied;

    public static event Action<Ability> OnAbilityUsed; 
    public static void TriggerCharacterDeath(Character character) => OnCharacterDied?.Invoke(character);
    public static void TriggerAbilityUsed(Ability ability) => OnAbilityUsed?.Invoke(ability);
}


