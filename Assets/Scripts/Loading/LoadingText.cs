using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]
public class LoadingText : MonoBehaviour {
    [SerializeField] List<string> texts;
    TextMeshProUGUI loadingText;
    int index = 0;
    [SerializeField] float cooldown = 0.75f;
    float currentCooldown = 0;
    void Start() {
        loadingText = gameObject.GetComponent<TextMeshProUGUI>();
        
    }

    void Update() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= cooldown) {
            index++;
            if (index == texts.Count) {
                index = 0;
            }
            loadingText.text = texts[index];
            currentCooldown = 0;
        }
    }
}
