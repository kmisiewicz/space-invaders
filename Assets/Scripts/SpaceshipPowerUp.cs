using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KM.SpaceInvaders
{
    [RequireComponent(typeof(SpaceshipController))]
    public class SpaceshipPowerUp : MonoBehaviour
    {
        [SerializeField] Slider shotSpeedBoostSlider;
        [SerializeField] Button shotSpeedBoostButton;
        [SerializeField] Image shotSpeedBoostFillImage;
        [SerializeField] Color defaultSliderColor = Color.white;
        [SerializeField] Color activeSliderColor = Color.green;
        [SerializeField] float shotSpeedBoostLoadTime = 40f;
        [SerializeField] float shotSpeedBoostDuration = 5f;


        SpaceshipController _playerSpaceship;


        private void Awake()
        {
            _playerSpaceship = GetComponent<SpaceshipController>();

            StartCoroutine(LoadShotSpeedBoost());
        }

        private IEnumerator LoadShotSpeedBoost()
        {
            float t = 0f;
            while (t < shotSpeedBoostLoadTime)
            {
                shotSpeedBoostSlider.value = Mathf.Lerp(0, 1, t / shotSpeedBoostLoadTime);
                t += Time.deltaTime;
                yield return null;
            }
            shotSpeedBoostSlider.value = 1;
            shotSpeedBoostButton.interactable = true;
            shotSpeedBoostFillImage.color = activeSliderColor;
        }

        public void OnLoadShotSpeedBoostActivated()
        {
            _playerSpaceship.ShootingIntervalMultiplier = 0.5f;
            shotSpeedBoostButton.interactable = false;
            StartCoroutine(ShotSpeedBoost());
        }

        private IEnumerator ShotSpeedBoost()
        {
            float t = shotSpeedBoostDuration;
            while (t > 0f)
            {
                shotSpeedBoostSlider.value = Mathf.Lerp(0, 1, t / shotSpeedBoostDuration);
                t -= Time.deltaTime;
                yield return null;
            }
            shotSpeedBoostSlider.value = 0;
            _playerSpaceship.ShootingIntervalMultiplier = 1f;
            shotSpeedBoostFillImage.color = defaultSliderColor;
            StartCoroutine(LoadShotSpeedBoost());
        }
    }
}