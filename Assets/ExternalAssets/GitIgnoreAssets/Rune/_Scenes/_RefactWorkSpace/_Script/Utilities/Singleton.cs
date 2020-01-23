// Based on https://github.com/UnityCommunity/UnitySingleton/blob/master/Assets/Scripts/Singleton.cs

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoonNight.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Fields
        
        private static T _instance;

        #endregion

        #region Properties

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = (T) FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }

                return _instance;
            }
        }
        
        #endregion

        #region Methods

        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                print("Destroy");
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
