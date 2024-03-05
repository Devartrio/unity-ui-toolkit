using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

namespace UiToolkitBasic
{
    public class UIController : MonoBehaviour
    {
        // 바텀 시트 그룹
        private VisualElement _bottomContainer;
        private Button _openButton;
        private Button _closeButton;

        // 바텀 시트
        private VisualElement _bottomSheet;
        private VisualElement _scrim;

        // 소년과 소녀
        private VisualElement _boy;
        private VisualElement _girl;

        private Label _message;

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
            // 소년과 소녀 참조
            _boy = root.Q<VisualElement>("Image_Boy");
            _girl = root.Q<VisualElement>("Image_Girl");
            // 소녀 대사용 메시지 참조
            _message = root.Q<Label>("Message");



            // 시작할 때 바텀 시트 그룹 감추기.
            _bottomContainer.style.display = DisplayStyle.None;

            // 버튼이 할 일
            _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClicked);
            _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);

            // 소년 애니메이션
            Invoke(nameof(AnimateBoy), 0.1f);

            // 바텀 시트가 내려온 다음 그룹 끄기
            _bottomSheet.RegisterCallback<TransitionEndEvent>(OnBottomSheetDown);
        }

        void OnOpenButtonClicked(ClickEvent evt)
        {
            // 바텀 시트 그룹 보여주기
            _bottomContainer.style.display = DisplayStyle.Flex;

            // 바텀 시트와 가림막 애니메이션
            _bottomSheet.AddToClassList("bottomsheet--up");
            _scrim.AddToClassList("scrim--fadein");

            AnimateGirl();
        }

        void AnimateGirl()
        {
            // 소녀 클래스리스트에서 Toggle
            _girl.ToggleInClassList("image--girl--iniar");
            // 트랜지션이 끝날 때 클래스 Toggle
            _girl.RegisterCallback<TransitionEndEvent>(evt => _girl.ToggleInClassList("image--girl--iniar"));

            // 대사가 출력되게
            _message.text = string.Empty;
            string m = "\"안녕하세요. 저는 소녀입니다.\"";
            DOTween.To(() => _message.text, x => _message.text = x, m, 3f).SetEase(Ease.Linear);
        }

        void OnCloseButtonClicked(ClickEvent evt)
        {
            

            // 바텀 시트와 가림막 애니메이션
            _bottomSheet.RemoveFromClassList("bottomsheet--up");
            _scrim.RemoveFromClassList("scrim--fadein");
        }

        void AnimateBoy()
        {
            _boy.RemoveFromClassList("image--boy--inair");

        }

        void OnBottomSheetDown(TransitionEndEvent evt)
        {
            if (!_bottomSheet.ClassListContains("bottomsheet--up"))
            {
                // 시작할 때 바텀 시트 그룹 감추기.
                _bottomContainer.style.display = DisplayStyle.None;
            }
        }
    } 
}
