using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour {
    System.Random rn = new System.Random();
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<Stats> statsList;
    [SerializeField] List<Weapon> weaponsList;
    //one time, contiunious spawning, wave, after some die
    float confidance;
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
        for (int i = 0; i < count; i++) {
            map.CalculateMapSpawnSuitability();
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
        //Transform randomTile = map.GetRandomOpenTile();
        Vector3 position = map.ViableSpawnPositionses[rn.Next(map.ViableSpawnPositionses.Count)];
        position.y = 6 + 1;
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    
}
