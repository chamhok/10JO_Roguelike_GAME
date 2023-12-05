using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeTransparency(0.3f); // 마우스가 올라갔을 때 불투명하게 변경 (알파값을 1로 설정)
    }

    // 버튼에서 마우스가 빠져나갔을 때 호출되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeTransparency(0f); // 마우스가 빠져나갔을 때 투명하게 변경 (알파값을 0.5로 설정)
    }

    // 이미지의 알파값을 변경하는 함수
    void ChangeTransparency(float alphaValue)
    {
        Color tempColor = buttonImage.color;
        tempColor.a = alphaValue; // 알파값 변경
        buttonImage.color = tempColor; // 변경된 알파값을 이미지에 적용
    }
}
