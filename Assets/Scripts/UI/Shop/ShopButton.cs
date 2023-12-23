using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour {
    public static event Action<Tuple<Shop.types, int>> itemClicked;
    [HideInInspector] public Shop.types type;
    [HideInInspector] public int id;
    void Start() {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    void Clicked() {
        itemClicked.Invoke(new Tuple<Shop.types, int>(type, id));
    }
}
