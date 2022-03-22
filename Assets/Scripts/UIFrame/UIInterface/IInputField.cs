using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UIFrame
{
    public interface IInputField
    {
        void AddOnValueChangeListener(UnityAction<string> action);
        string GetInputFieldText();
        void SetInputFieldText(string text);
    }
}
