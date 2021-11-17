using UnityEngine;

namespace KM.Utility
{
    [RequireComponent(typeof(Camera))]
    public class AdjustCameraToSprite : MonoBehaviour
    {
        [SerializeField] SpriteRenderer referenceSprite;


        private void Awake()
        {
            if (referenceSprite != null)
            {
                var bounds = referenceSprite.bounds.extents;
                var height = bounds.x / GetComponent<Camera>().aspect;
                if (height < bounds.y)
                    height = bounds.y;
                GetComponent<Camera>().orthographicSize = height;
            }
        }
    }
}