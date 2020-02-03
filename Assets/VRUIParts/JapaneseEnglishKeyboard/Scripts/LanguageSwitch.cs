using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace VRUIParts
{
    public class LanguageSwitch : MonoBehaviour
    {
        public bool IsEnglish = true;
        [SerializeField]
        Sprite _Sprite_English;
        [SerializeField]
        Sprite _Sprite_Japanese;
        public GameObject _JapaneseCharacter;
        Image _Image_Language;

        private static Subject<Unit> m_OnLanguageChange = new Subject<Unit>();
        public static IObservable<Unit> OnLanguageChange { get { return m_OnLanguageChange; } }

        void Start()
        {
            _Image_Language = this.GetComponent<Image>();
        }

        void Update()
        {
            if (!IsEnglish)///かな入力
            {
                _JapaneseCharacter.SetActive(true);
            }
            else ///英字入力
            {
                _JapaneseCharacter.SetActive(false);
            }
        }
        public void onChangeLanguage()
        {
            IsEnglish = !IsEnglish;
            _Image_Language.sprite = IsEnglish ? _Sprite_English : _Sprite_Japanese;
            m_OnLanguageChange.OnNext(Unit.Default);
        }
    }
}
