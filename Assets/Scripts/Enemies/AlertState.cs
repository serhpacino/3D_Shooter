﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyAI
{
    EnemyStates enemy;
    float timer = 0;
    public AlertState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }
    public void UpdateActions()
    {
        Search();
        Watch();
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        { 
            LookAround();
        }
    }
    void Watch()
    {
      if(enemy.EnemySpotted())
        {
            enemy.navMeshAgent.destination = enemy.lastKnownPosition;
            ToChaseState();
        }
    }
    void LookAround()
    {
        timer += Time.deltaTime;
        if(timer >= enemy.stayAlertTime)
        {
            timer = 0;
            ToPatrolState();
        }
    }
    void Search()
    {
        enemy.navMeshAgent.destination = enemy.lastKnownPosition;
        enemy.navMeshAgent.isStopped = false ;
    }
    public void OnTriggerEnter(Collider enemy) { }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }
    public void ToAttackState() 
    {
        Debug.Log("Error");
    }
    public void ToAlertState() 
    {
        Debug.Log("Error");
    }
    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
}
