using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour {
    public static event Action<Tuple<Shop.types, int>> itemClicked;
    public static event Action<Tuple<Shop.types, int>> showDescription;
    public static event Action hideDescription;
    [HideInInspector] public Shop.types type;
    [HideInInspector] public int id;
    void Start() {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    public void pointerEnter() {
        showDescription.Invoke(new Tuple<Shop.types, int>(type, id));
    }
    public void pointerExit() {
        hideDescription.Invoke();
    }
    void Clicked() {
        itemClicked.Invoke(new Tuple<Shop.types, int>(type, id));
    }
}
