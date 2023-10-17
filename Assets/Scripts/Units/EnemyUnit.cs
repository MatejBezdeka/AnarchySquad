using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SlowUpdate());

    }
    IEnumerator SlowUpdate() {
        WaitForSeconds waitTime = new WaitForSeconds(responseTime);
        while (true)
        {
            foreach (var unit in units) {
                
            }
            yield return waitTime;
        }
    }
}
