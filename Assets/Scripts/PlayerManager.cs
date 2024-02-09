using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Light flashLight;

    [SerializeField] private float flashLightEnergy;
    [SerializeField] private float spendingRate;

    [SerializeField] private int playerHealth;
    [SerializeField] private GameObject gameOverScene;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image energyBar;

    public bool isDead,isWin;
    RaycastHit hit;



    private void Start()
    {
        flashLightEnergy = 100;
        playerHealth = 100;
    }

    private void Update()
    {
        if(!isDead)
        {
            healthBar.fillAmount = (float)playerHealth / 100;
            energyBar.fillAmount = flashLightEnergy / 100;


            flashLightEnergy = Mathf.Clamp(flashLightEnergy, 0, 100);
            playerHealth = Mathf.Clamp(playerHealth, 0, 100);

            flashLight.color = new Color32(253, 231, 149, 255);
            flashLight.intensity = 1.25f;

            if (!Input.GetMouseButton(1))
            {
                flashLightEnergy += Time.deltaTime * spendingRate;
            }

            if (flashLightEnergy > 0)
            {
                if (Input.GetMouseButton(1))
                {
                    flashLightEnergy -= Time.deltaTime * spendingRate;

                    flashLight.color = new Color32(191, 115, 255, 255);
                    flashLight.intensity = 3;

                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                    if (Physics.Raycast(ray, out hit, 50))
                    {
                        if (hit.transform.gameObject.CompareTag("Pumpkin"))
                        {
                            hit.transform.GetComponent<PumpkinMovement>().health -= 100 * Time.deltaTime;
                        }
                    }
                }
            }

            if(playerHealth<=0)
            {
                isDead = true;
            }
        }

        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            gameOverScene.SetActive(true);
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PumpkinHand"))
        {
            playerHealth -= 15;
        }

        if(other.gameObject.CompareTag("TheEnd"))
        {
            isWin = true;
        }
    }
}
