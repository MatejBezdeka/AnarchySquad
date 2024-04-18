using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {
    System.Random rn = new System.Random();
    int maxenemyCount = 16;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<EnemyUnit> enemyUnits;
    [SerializeField] List<Stats> statsList;
    [SerializeField] List<Weapon> weaponsList;
    [SerializeField] GameObject winScreen;
    //one time, contiunious spawning, wave, after some die
    float confidence;
    int difficulty;
    //float waveCooldown = 999;
    //float currentWaveCooldown = 0;
    float calculateMapCooldown = 5;
    float currentMapCooldown = 0;
    int points;
    
    MapGenerator map;
    void Start() {
        difficulty = PlayerPrefs.GetInt("Ms");
        points = (difficulty * 500 + 1000);
        map = GameManager.instance.MapGenerator;
        //StartCoroutine(EnemySpawner());
        map.CalculateMapSpawnSuitability();
        SpawnAllYouCan();
    }

    void Update() {
        /*currentWaveCooldown += Time.deltaTime;
        currentMapCooldown += Time.deltaTime;
        if (currentWaveCooldown > waveCooldown) {
            currentWaveCooldown = 0;
            SpawnWave(0);
        }*/

        if (currentMapCooldown > calculateMapCooldown) {
            calculateMapCooldown = 0;
            map.CalculateMapSpawnSuitability();
        }
    }

    /*void SpawnWave(int count) {
        for (int i = 0; i < count; i++) {
            SpawnEnemy();
        }

        /*string row = "";
        for (int i = 0; i < map.TilesX; i++) {
            for (int j = 0; j < map.TilesY; j++) {
                row += map.MapSpawnSuitabilityValues[i,j] + " ";
            }

            row += "\n";
        }
        Debug.Log(row);*/
    //}
    void SpawnAllYouCan() {
        int enemyCount = rn.Next(6, 9+difficulty);
        int budgetForOne = points / enemyCount;
        while (enemyCount > 0) {
            SpawnEnemy(budgetForOne); 
            enemyCount--;
        }
    }
    void SpawnEnemy(int budget) {
        Vector3 position = map.ViableSpawnPositionses[rn.Next(map.ViableSpawnPositionses.Count)];
        position.y = 6 + 1;
        EnemyUnit enemy = UnitFactory.SpawnEnemy(enemyPrefab, 
            new UnitBlueprint(
                Names.GetRandomName(), 
                statsList[GetStats(budget/2, new List<ShopItem>(statsList))], 
                weaponsList[GetStats(budget/2, new List<ShopItem>(weaponsList))]), 
            position);
        enemyUnits.Add(enemy);
        enemy.died += EnemyDied;
    }

    int GetStats(int budget, List<ShopItem> list) {
        List<int> viableOptions = new List<int>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Cost <= budget) viableOptions.Add(i);
        }
        int closest = viableOptions[0];
        int closestDifference = Math.Abs(list[viableOptions[0]].Cost - budget);
        int index = closest;
        int max = list[closest].Cost;
        for (int i = 1; i < viableOptions.Count; i++) {
            int currentDifference = Math.Abs(list[viableOptions[i]].Cost - budget);
            if (currentDifference < closestDifference ||
                (currentDifference == closestDifference && list[viableOptions[i]].Cost > max)) {
                closestDifference = currentDifference;
                closest = viableOptions[i];
                index = closest;
                max = list[closest].Cost;
            }
        }
        return index;
    }
    void EnemyDied(EnemyUnit deadEnemy) {
        foreach (var enemy in enemyUnits) {
            if (deadEnemy == enemy) {
                enemyUnits.Remove(enemy);
                break;
            }
        }
        if (enemyUnits.Count == 0) {
            GameManager.instance.StopTimeDefinitevely();
            PlayerPrefs.SetInt("CS", rn.Next(int.MinValue+64, int.MaxValue-64));
            winScreen.SetActive(true);
        }
    }
}
