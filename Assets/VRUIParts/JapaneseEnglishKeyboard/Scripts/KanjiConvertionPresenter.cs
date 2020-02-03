using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
namespace VRUIParts
{
    public class KanjiConvertionPresenter : MonoBehaviour
    {
        string _APIURL = "http://www.google.com/transliterate?langpair=ja-Hira|ja&text=";
        string _APIURLEnd = ",";

        KanjiConverter _Converter;

        string _Hiragana;

        public Text ConvertText;

        [SerializeField]
        Transform _Transform_FieldCandidate = null;
        [SerializeField]
        RectTransform _Prefab_ButtonCandidate = null;
        [SerializeField]
        private LanguageSwitch LanguageSwitch;

        private UI_Keyboard _UI_Keyboard;


        void Start()
        {
            _Converter = new KanjiConverter(_APIURL, _APIURLEnd);
            ClickCandidate.OnClickCandidateButton.Subscribe(Item => OnSelectCandidate(Item)).AddTo(this);
            _UI_Keyboard = GetComponent<UI_Keyboard>();
        }

        // 変換ボタン押したときに呼ぶメソッド
        public async void OnClickGetCandidateAsync()
        {
            _Hiragana = ConvertText.text;
            List<string> kanjiCandidates = await _Converter.GetCandidate(_Hiragana);

            kanjiCandidates.Reverse();
            foreach (string kanji in kanjiCandidates)
            {
                var item = Instantiate(_Prefab_ButtonCandidate) as RectTransform;

                var text = item.GetComponentsInChildren<Text>();

                text[0].text = kanji;

                item.SetParent(_Transform_FieldCandidate, false);
            }
        }

        ///変換候補の中から決定したら
        private void OnSelectCandidate(Image Item)
        {

            _UI_Keyboard.InputField_Result.text = _UI_Keyboard.InputField_Result.text.TrimEnd(ConvertText.text.ToCharArray());
            UI_Keyboard._TmpJapText = UI_Keyboard._TmpJapText.TrimEnd(ConvertText.text.ToCharArray());
            UI_Keyboard._TmpKanaText = UI_Keyboard._TmpKanaText.TrimEnd(ConvertText.text.ToCharArray());

            _UI_Keyboard.InputField_Result.text += Item.gameObject.GetComponentInChildren<Text>().text;
            UI_Keyboard._TmpJapText += Item.gameObject.GetComponentInChildren<Text>().text;
            UI_Keyboard._TmpKanaText += Item.gameObject.GetComponentInChildren<Text>().text;


            if (LanguageSwitch.IsEnglish)
            {
                _UI_Keyboard.InputField_Result.text += " ";
            }

            foreach (Transform n in _Transform_FieldCandidate)
            {
                GameObject.Destroy(n.gameObject);
            }
            ConvertText.text = "";

        }
    }
}
