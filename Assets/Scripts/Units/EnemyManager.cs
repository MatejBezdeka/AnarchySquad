using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour {
    System.Random rn = new System.Random();
    int maxenemyCount = 16;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<EnemyUnit> enemyUnits;
    [SerializeField] List<Stats> statsList;
    [SerializeField] List<Weapon> weaponsList;
    //one time, contiunious spawning, wave, after some die
    float confidence;
    float difficulty;
    float currentWaveCooldown;
    float waveCooldown;
    
    MapGenerator map;
    void Start() {
        map = GameManager.instance.MapGenerator;
        StartCoroutine(EnemySpawner());
        //CalculateMapSpawnSuitability();
        SpawnWave(1);
    }

    void SpawnWave(int count) {
        map.CalculateMapSpawnSuitability();
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
    }
    IEnumerator EnemySpawner() {
        WaitForSeconds waitTime = new WaitForSeconds(10);
        while (true) {
            //SpawnEnemy();
            SpawnWave(1);
            yield return waitTime;
        }
    }
    void SpawnEnemy() {
        Vector3 position = map.ViableSpawnPositionses[rn.Next(map.ViableSpawnPositionses.Count)];
        position.y = 6 + 1;
        enemyUnits.Add(
            UnitFactory.SpawnUnit(
                enemyPrefab,
                new UnitBlueprint(Names.GetRandomName(), statsList[rn.Next(statsList.Count)], weaponsList[rn.Next(weaponsList.Count)]),
                position)
            as EnemyUnit);
    }

    
}
