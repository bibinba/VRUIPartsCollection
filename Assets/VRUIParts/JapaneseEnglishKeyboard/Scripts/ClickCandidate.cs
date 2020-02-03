using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace VRUIParts
{
    public class ClickCandidate : MonoBehaviour
    {
        private static Subject<Image> _OnClickCandidateButton = new Subject<Image>();
        public static IObservable<Image> OnClickCandidateButton { get { return _OnClickCandidateButton; } }

        public void OnClickCandidate()
        {
            Image candidate_image = this.GetComponent<Image>();
            _OnClickCandidateButton.OnNext(candidate_image);
        }
    }
}
