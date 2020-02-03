using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace VRUIParts
{
    public class KeyPresenter : MonoBehaviour
    {
        public UI_Keyboard Keyboard;
        public List<GameObject> KeyObjList = new List<GameObject>();
        void Start()
        {

            foreach (GameObject keyObj in KeyObjList)
            {
                keyObj.AddComponent<EventTrigger>();
                EventTrigger trigger = keyObj.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((x) =>
                {
                    Keyboard.ClickKey(keyObj.name.ToLower());
                });

                trigger.triggers.Add(entry);
            }
        }


    }
}
