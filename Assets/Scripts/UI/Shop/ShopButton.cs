using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : UIButton {
    public static Action deselected;
    static Action buttonSelected;
    public AudioSettings.ButtonSounds Sound { get { return AudioSettings.ButtonSounds.normal; } }
    public static event Action<Tuple<Shop.types, int>> itemClicked;
    public static event Action<Tuple<Shop.types, int>> showDescription;
    public static event Action hideDescription;
    [HideInInspector] public Shop.types type;
    [HideInInspector] public int id;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image image;
    //[SerializeField] private TextMeshProUGUI priceText;
    protected override void Start() {
        base.Start();
        transform.localScale = Vector3.one;
        buttonSelected += () => { button.interactable = true; };
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
    protected override void Functionality() {
        itemClicked!.Invoke(new Tuple<Shop.types, int>(type, id));
        buttonSelected.Invoke();
        button.interactable = false;
    }

    public void Selected() { }
    public void Deselected() {
        Debug.Log("deselected");
        deselected.Invoke();
    }
}
