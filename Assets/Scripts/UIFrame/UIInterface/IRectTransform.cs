using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public interface IRectTransform
    {
        void SetParent(Transform parent);
        void SetParent(Transform parent, bool stayWorldPos);
    }
}
