using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] List<Stats> statsList;
    [SerializeField] List<Weapon> weaponsList;
    //one time, contiunious spawning, wave, after some die
    
    void Start() {
        StartCoroutine(EnemySpawner());
    }

    void SpawnWave(int count) {
        for (int i = 0; i < count; i++) {
            SpawnEnemy();
        }
    }
    IEnumerator EnemySpawner() {
        WaitForSeconds waitTime = new WaitForSeconds(5);
        while (true) {
            SpawnEnemy();
            yield return waitTime;
        }
    }
    void SpawnEnemy() {
        Transform randomTile = GameManager.instance.MapGenerator.GetRandomOpenTile();
        Vector3 position = randomTile.position;
        position.y = 6 + 1;
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }
}
