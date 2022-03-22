using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public interface IText
    {
        void SetTextText(string text);
        string GetTextText();
        void SetTextColor(Color color);
    }
}
