using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour {
    [SerializeField] float cooldown;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject messageHolder;
    public void SetErrorMessage(string text) {
        this.text.text = text;
        cooldown = 2;
        messageHolder.SetActive(true);
    }

    void Update() {
        cooldown -= Time.deltaTime;
        if (cooldown < 0) {
            messageHolder.SetActive(false);
        }
    }
}
