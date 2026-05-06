using UnityEngine;

public static class GameSettings
{
    public static int difficulty = 0;

    public static int startMoney = 200;
    public static float baseHP = 20f;

    public static float enemyHPMultiplier = 1f;
    public static float enemySpeedMultiplier = 1f;

    public static void ApplyDifficulty()
    {
        switch (difficulty)
        {
            case 0: // Easy
                startMoney = 750;
                baseHP = 25f;

                enemyHPMultiplier = 0.8f;
                enemySpeedMultiplier = 0.9f;
                break;

            case 1: // Medium
                startMoney = 350;
                baseHP = 20f;

                enemyHPMultiplier = 1f;
                enemySpeedMultiplier = 1f;
                break;

            case 2: // Hard
                startMoney = 150;
                baseHP = 15f;

                enemyHPMultiplier = 1.3f;
                enemySpeedMultiplier = 1.2f;
                break;
        }
    }
}