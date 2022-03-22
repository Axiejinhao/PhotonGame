using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public interface IImage
    {
        void SetSprite(Sprite sprite);
        Sprite GetSprite();
        void SetImageColor(Color color);
        Color GetImageColor();
    }
}
