using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatState
{
    void Enter();
    void Exit();
    void Update();
}
public class CombatStateMachine : MonoBehaviour
{
    public ICombatState _currentState;

    public void ChangeState(ICombatState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private void Update() => _currentState?.Update();
}

public class StartCombatState : ICombatState
{
    public void Enter()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}

public class PlayerTurnState : ICombatState
{
    public void Enter()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
public class EnemyTurnState : ICombatState
{
    public void Enter()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
public class VictoryState : ICombatState
{
    public void Enter()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
public class DefeatState : ICombatState
{
    public void Enter()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}