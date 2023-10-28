using UnityEngine;

public class PointArrowTimer : MonoBehaviour {
    [SerializeField] float lifeTime;
    float currentTime;
    void FixedUpdate() {
        currentTime += Time.unscaledDeltaTime;
        if (currentTime >= lifeTime) {
            Destroy(gameObject);
        }
    }
}
