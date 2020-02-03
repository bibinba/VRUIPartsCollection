//
// This is a modified version of VRTK/UI_Keyboard.
//
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Text;
using System;
namespace VRUIParts
{
    public class UI_Keyboard : MonoBehaviour
{
    public InputField InputField_Result;
    public GameObject Keyboard; 
    public Text ConvertText;
    public static string _TmpJapText = "";
    public static string _TmpKanaText = "";
    string _TmpText = "";
    [SerializeField]
    private LanguageSwitch LanguageSwitch;

    void Start()
    {
        LanguageSwitch.OnLanguageChange.Subscribe(_ => ChangeInputText()).AddTo(this);
    }
    public void SetInputField(InputField outsideInputField)
    {
            InputField_Result = outsideInputField;
    }


    private void ChangeInputText()
    {
        _TmpText = InputField_Result.text;
        _TmpJapText = "";
        _TmpKanaText = "";
        ConvertText.text = "";

    }
    public void ClickKey(string character)
    {

        if (!LanguageSwitch.IsEnglish)///かな入力
        {
            _TmpKanaText = _TmpKanaText + character;
            _TmpJapText = RomajiKanaConverter.RomanToKana(_TmpKanaText);
  
            if (_TmpKanaText == _TmpJapText)
            {
                    InputField_Result.text = InputField_Result.text + character;
               
            }
            else
            {
                    InputField_Result.text = InputField_Result.text + character;
                StringBuilder sb = new StringBuilder(InputField_Result.text);
                sb = sb.Replace(_TmpKanaText, _TmpJapText, InputField_Result.text.Length - _TmpKanaText.Length, _TmpKanaText.Length);
                    InputField_Result.text = sb.ToString();

                _TmpKanaText = _TmpJapText;
              
            }
            ConvertText.text = RomajiKanaConverter.RomanToKana(ConvertText.text + character);
     
        }
        else ///英字入力
        {
                InputField_Result.text = InputField_Result.text + character;

        }

    }

    public void Backspace()
    {
        if (InputField_Result.text.Length > 0)
        {
                InputField_Result.text = InputField_Result.text.Substring(0, InputField_Result.text.Length - 1);

        }
        if (ConvertText.text.Length > 0)
        {
            ConvertText.text = ConvertText.text.Substring(0, ConvertText.text.Length - 1);
        }

        if (_TmpJapText.Length > 0)
        {
            _TmpJapText = _TmpJapText.Substring(0, _TmpJapText.Length - 1);
            
        }
        if (_TmpKanaText.Length > 0)
        {
            _TmpKanaText = _TmpKanaText.Substring(0, _TmpKanaText.Length - 1);
        }
    

    }

    public void Enter()
    {
        _TmpJapText = "";
        _TmpKanaText = "";
        ConvertText.text = "";
        // Keyboard.gameObject.SetActive(false);
    }

    public void InactiveKeyboard()
    {
        Keyboard.gameObject.SetActive(false);
    }
}
}
