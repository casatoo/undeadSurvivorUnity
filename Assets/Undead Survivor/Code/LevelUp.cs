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
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide() {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
        rect.localScale = Vector3.zero;
        GameManager.instance.Remuse();
    }

    public void Select(int index) {
        items[index].OnClick();
    }

    void Next() {
        // 모든 아이템 비활성화
        foreach (Item item in items) {
            item.gameObject.SetActive(false);
        }
        // 그 중에서 랜덤하게 3개 아이템만 활성화
        int[] ran = new int[3];
        while (true) {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if(ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2]){
                break;
            }
        }
        for (int index =0; index < ran.Length; index++){
            Item ranItem = items[ran[index]];
            // 만랩 아이템 경우 소비아이템으로 대체
            if(ranItem.level == ranItem.data.damages.Length){
                items[Random.Range(4, 7)].gameObject.SetActive(true);
            } else {
            ranItem.gameObject.SetActive(true);
            }
        }
        
    }
}
