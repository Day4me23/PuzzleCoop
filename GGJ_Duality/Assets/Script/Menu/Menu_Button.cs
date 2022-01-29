using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Menu_Button : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Color32 selectedColor;
    [SerializeField] private Color32 notSelectedColor;

    public void OnSelect(BaseEventData eventData) {
        //Debug.Log(this.gameObject.name + "was selected!");


        InceaseSize(0.05f);
    }


    public void OnDeselect(BaseEventData eventData) {
        //Debug.Log(this.gameObject.name + "was deselected!");
        DecreaseSize(0.05f);
    }


    public async void InceaseSize(float duration) {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = selectedColor;
        float timer = 0f;
        while(timer <= duration) {
            timer += Time.deltaTime;
            transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime * 3;
        }
        await Task.Yield();
    }

    public async void DecreaseSize(float duration) {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = notSelectedColor;
        float timer = 0f;
        while (timer <= duration) {
            timer += Time.deltaTime;
            transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime * 3;
        }
        await Task.Yield();
    }
}
