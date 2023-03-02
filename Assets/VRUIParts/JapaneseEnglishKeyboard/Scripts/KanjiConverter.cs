using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Cysharp.Threading.Tasks;

namespace VRUIParts
{
    public class KanjiConverter
    {
        private string _URL;
        private string _URLend;
        private char[] _Splitter = { ',' };

        public KanjiConverter(string url, string urlend)
        {
            this._URL = url;
            this._URLend = urlend;
        }

        public async UniTask<List<string>> GetCandidate(string hiragana)
        {
            List<string> candidates = new List<string>();

            if (hiragana != string.Empty)
            {
                var uwr = UnityWebRequest.Get(_URL + hiragana + _URLend);

                // SendWebRequestが終わるまでawait 
                await uwr.SendWebRequest();

                if (uwr.isHttpError || uwr.isNetworkError)
                {
                    // 失敗していたらそのまま例外をthrow
                    throw new Exception(uwr.error);
                }
                candidates = ComposeCandidate(uwr.downloadHandler.text);

            }
            return candidates;
        }

        private List<string> ComposeCandidate(string data)
        {
            List<string> candidates = new List<string>(); // empty list
            string value = data.Split(_Splitter, 2)[1];
            value = value.Remove(value.Length - 2);

            if (value != "[]")
            {
                GoogleAutoCompleteAPI respose = JsonUtility.FromJson<GoogleAutoCompleteAPI>("{\"candidate\":" + value + "}");
                foreach (string candidate in respose.candidate)
                {
                    candidates.Add(candidate);
                }
            }

            return candidates;
        }

        public class GoogleAutoCompleteAPI
        {
            public string[] candidate;
        }
    }
}
