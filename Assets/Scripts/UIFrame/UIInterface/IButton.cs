using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UIFrame
{
    public interface IButton
    {
        void AddOnClickListener(UnityAction action);
    }
}
