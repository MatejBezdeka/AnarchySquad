using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour {
    public static Action deselected;
    public static event Action<Tuple<Shop.types, int>> itemClicked;
    public static event Action<Tuple<Shop.types, int>> showDescription;
    public static event Action hideDescription;
    [HideInInspector] public Shop.types type;
    [HideInInspector] public int id;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image image;
    //[SerializeField] private TextMeshProUGUI priceText;
    void Start() {
        GetComponent<Button>().onClick.AddListener(Clicked);
        transform.localScale = Vector3.one;
    }

    public void SetGraphics(string name, int cost, Sprite image) {
        nameText.text = name;
        costText.text = cost.ToString();
        this.image.sprite = image;
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
