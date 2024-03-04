using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UiToolkitBasic
{
    public class UIController : MonoBehaviour
    {
        private VisualElement _bottomContainer;

        private Button _openButton;

        private Button _closeButton;

        // 바텀 시트
        private VisualElement _bottomSheet;

        private VisualElement _scrim;

        private void Start()
        {
            // UI 도큐먼트에 있는 최상위 비주얼엘리멘트를 참조
            var root = GetComponent<UIDocument>().rootVisualElement;

            // _bottomContainer 참조
            _bottomContainer = root.Q<VisualElement>("Container_Bottom");

            // 열기, 닫기 버튼
            _openButton = root.Q<Button>("Button_Open");
            _closeButton = root.Q<Button>("Button_Close");
            // 바텀 시트와 가림막 참조
            _bottomSheet = root.Q<VisualElement>("BottomSheet");
            _scrim = root.Q<VisualElement>("Scrim");

            // 시작할 때 바텀 시트 그룹 감추기.
            _bottomContainer.style.display = DisplayStyle.None;

            // 버튼이 할 일
            _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClicked);
            _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        }

        void OnOpenButtonClicked(ClickEvent evt)
        {
            // 바텀 시트 그룹 보여주기
            _bottomContainer.style.display = DisplayStyle.Flex;

            // 바텀 시트와 가림막 애니메이션
            _bottomSheet.AddToClassList("bottomsheet--up");
            _scrim.AddToClassList("scrim--fadein");
        }

        void OnCloseButtonClicked(ClickEvent evt)
        {
            // 시작할 때 바텀 시트 그룹 감추기.
            _bottomContainer.style.display = DisplayStyle.None;

            // 바텀 시트와 가림막 애니메이션
            _bottomSheet.RemoveFromClassList("bottomsheet--up");
            _scrim.RemoveFromClassList("scrim--fadein");
        }
    } 
}
