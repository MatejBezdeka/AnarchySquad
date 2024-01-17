using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PointArrowTimer : MonoBehaviour {
    [SerializeField] float lifeTime;
    private async void Start() {
        await Task.Delay((int)(lifeTime * 1000));
        Destroy(gameObject);
    }
}
