using System.Collections.Generic;
using UnityEngine;

public static class EnemyDatabase
{
    public static EnemyData GetRandomEnemy()
    {
        List<EnemyData> enemies = new List<EnemyData>();

        EnemyData slime = new EnemyData
        {
            enemyName = "Slime",
            maxHP = 30,
            currentHP = 30,
            attackDamage = 9
        };

        EnemyData goblin = new EnemyData
        {
            enemyName = "Goblin",
            maxHP = 25,
            currentHP = 25,
            attackDamage = 15
        };

        EnemyData zombie = new EnemyData
        {
            enemyName = "Zombie",
            maxHP = 50,
            currentHP = 50,
            attackDamage = 10
        };

        EnemyData bandit = new EnemyData
        {
            enemyName = "Bandit",
            maxHP = 40,
            currentHP = 40,
            attackDamage = 13
        };
        enemies.Add(slime);
        enemies.Add(goblin);
        enemies.Add(zombie);
        enemies.Add(bandit);
        return enemies[Random.Range(0, enemies.Count)];
    }
    public static EnemyData GetBossEnemy()
    {
        return new EnemyData
        {
            enemyName = "Dark Lord",
            maxHP = 100,
            currentHP = 100,
            attackDamage = 20
        };
    }
}
