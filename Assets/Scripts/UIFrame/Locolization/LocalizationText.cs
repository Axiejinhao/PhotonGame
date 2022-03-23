using System;
using System.Collections.Generic;
using UIFrame;
using UnityEngine;
using UnityEngine.UI;

namespace Locolization
{
    [RequireComponent(typeof(Text))]
    public class LocalizationText : MonoBehaviour
    {
        private Text _text;

        private string[] languageTexts;

        private void Awake()
        {
            _text = GetComponent<Text>();
            languageTexts = JsonDataManager.Instance.FindTextLocalization(this.name);
        }

        private void Start()
        {

        }

        private void OnEnable()
        {
            LocalizationManager.Instance.AddLocalizationListener(SetLanguageText);
        }

        private void OnDisable()
        {
            LocalizationManager.Instance.RemoveLocalizationListener(SetLanguageText);
        }

        /// <summary>
        /// 设置本地化文本
        /// </summary>
        /// <param name="languageID"></param>
        public void SetLanguageText(int languageID)
        {
            _text.text = languageTexts[languageID];
        }
    }
}
