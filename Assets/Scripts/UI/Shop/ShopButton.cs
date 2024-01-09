using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour {
    public static Action deselected;
    public static event Action<Tuple<Shop.types, int>> itemClicked;
    public static event Action<Tuple<Shop.types, int>> showDescription;
    public static event Action hideDescription;
    [HideInInspector] public Shop.types type;
    [HideInInspector] public int id;
    [SerializeField] TextMeshProUGUI text;
    //[SerializeField] private TextMeshProUGUI priceText;
    void Start() {
        GetComponent<Button>().onClick.AddListener(Clicked);
        transform.localScale = Vector3.one;
    }

    public void SetText(string text) {
        this.text.text = text;
    }
    public void PointerEnter() {
        showDescription!.Invoke(new Tuple<Shop.types, int>(type, id));
    }
    public void PointerExit() {
        hideDescription!.Invoke();
    }
    void Clicked() {
        itemClicked!.Invoke(new Tuple<Shop.types, int>(type, id));
    }

    public void Selected() {
        
    }

    public void Deselected() {
        Debug.Log("deselected");
        deselected.Invoke();
    }
}
