using System;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIFrame
{
    public class UIMono : MonoBehaviour,
        IRectTransform, IText, IInputField,
        IImage, IRawImage, IButton, IGameObject
    {
        #region Component

        private RectTransform _rectTransform;
        private Image _image;
        private RawImage _rawIamge;
        private Button _button;
        private Text _text;
        private InputField _inputField;

        #endregion

        #region Mono Callback

        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            _rawIamge = GetComponent<RawImage>();
            _button = GetComponent<Button>();
            _text = GetComponent<Text>();
            _inputField = GetComponent<InputField>();
        }

        #endregion

        #region IText

        public virtual string GetTextText()
        {
            return _text.text;
        }

        public virtual void SetTextColor(Color color)
        {
            _text.color = color;
        }

        public virtual void SetTextText(string text)
        {
            _text.text = text;
        }

        #endregion

        #region IInputField

        public virtual void AddOnValueChangeListener(UnityAction<string> action)
        {
            _inputField.onValueChanged.AddListener(action);
        }

        public virtual string GetInputFieldText()
        {
            return _inputField.text;
        }

        public virtual void SetInputFieldText(string text)
        {
            _inputField.text = text;
        }

        #endregion

        #region IImage

        public virtual void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public virtual Sprite GetSprite()
        {
            return _image.sprite;
        }

        public virtual void SetImageColor(Color color)
        {
            _image.color = color;
        }

        public virtual Color GetImageColor()
        {
            return _image.color;
        }

        #endregion

        #region IButton

        public virtual void AddOnClickListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        #endregion

        #region IRectTransform

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetParent(Transform parent, bool stayWorldPos)
        {
            transform.SetParent(parent,stayWorldPos);
        }

        #endregion

        #region IGameObject

        public void SetGameObjectActive(bool active)
        {
            gameObject.SetActive(active);
        }

        #endregion

    }
}
