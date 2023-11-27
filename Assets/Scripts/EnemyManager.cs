using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] GameObject enemyPrefab;
    //one time, contiunious spawning, wave, after some die
    
    void Start() {
        StartCoroutine(EnemySpawner());
    }

    
    void Update()
    {
        
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
        position.y = 6.2f;
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }
}
