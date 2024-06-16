using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAgent : GoapAgent
{


    protected override void SetupBeliefs()
    {
        base.SetupBeliefs();
    }

    protected override void SetupActions()
    {
        base.SetupActions();

        actions.Add(new AgentAction.Builder("AttackEnemy")
            .WithStrategy(new AttackStrategy(1, attackSensor, 10))
            .AddPrecondition(beliefs["EnemyInAttackRange"])
            .AddEffect(beliefs["AttackingEnemy"])
            .Build());
    }

    protected override void SetupGoals()
    {
        base.SetupGoals();

        
    }
}
