using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VRUIParts;
public class OutsideInputFieldTest : MonoBehaviour
{
    [SerializeField] private UI_Keyboard _UI_Keyboard;
    [SerializeField] private Button _Button_Input;
    [SerializeField] private InputField _InputField_Name;
    private void Start()
    {
        _Button_Input.onClick.AsObservable()
      .Subscribe(_ => OnClickedButton_Input())
      .AddTo(this);
    }
    private void OnClickedButton_Input()
    {
        _UI_Keyboard.SetInputField(_InputField_Name);
    }
}
