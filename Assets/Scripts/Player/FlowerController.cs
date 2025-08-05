using System;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class FlowerController : MonoBehaviour
    {
        [SerializeField] private Transform trunk;

        private void OnEnable()
        {
            trunk.DOScale(Vector3.one, 0.25f).SetEase(Ease.Linear);
        }
    }
}
