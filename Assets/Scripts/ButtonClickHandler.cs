using UnityEngine;
using UnityEngine.EventSystems;

namespace KM.Utility
{
    public class ButtonClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsPressed { get; private set; } = false;


        public void OnPointerDown(PointerEventData eventData) => IsPressed = true;

        public void OnPointerUp(PointerEventData eventData) => IsPressed = false;
    }
}