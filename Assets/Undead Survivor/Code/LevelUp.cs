using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show() {
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide() {
        rect.localScale = Vector3.zero;
        GameManager.instance.Remuse();
    }

    public void Select(int index) {
        items[index].OnClick();
    }
}
