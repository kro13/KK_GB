
using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public interface IMonoExposed
    {
        void AddUpdate(Action onUpdateAction);
        void AddStart(Action<MonoBehaviour> onStartAction);
    }
}
