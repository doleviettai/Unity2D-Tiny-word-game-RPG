using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public static ShopKeeper currentShopKeeper;
    public Animator anim;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;

    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopWealpons;
    [SerializeField] private List<ShopItems> shopArmour;
    [SerializeField] private Camera shopkeeperCam;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -1);

    public static event Action<ShopManager, bool> OnShopStateChanged;
    private bool playerInRange;
    private bool isShopOpen;

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)
                {
                    Time.timeScale = 0;
                    currentShopKeeper = this;
                    isShopOpen = true;
                    OnShopStateChanged?.Invoke(shopManager, true);
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.interactable = true;
                    shopCanvasGroup.blocksRaycasts = true;

                    shopkeeperCam.transform.position = transform.position + cameraOffset;
                    shopkeeperCam.gameObject.SetActive(true);

                    OpenItemShop();
                }
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                Time.timeScale = 1;
                currentShopKeeper = null;
                isShopOpen = false;
                OnShopStateChanged?.Invoke(shopManager, false);
                shopCanvasGroup.alpha = 0;
                shopCanvasGroup.interactable = false;
                shopCanvasGroup.blocksRaycasts = false;

                shopkeeperCam.gameObject.SetActive(false);
            }
                
        }
    }

    public void OpenItemShop()
    {
        shopManager.PopulateShopItem(shopItems);
    }

    public void OpenWeapShop()
    {
        shopManager.PopulateShopItem(shopWealpons);
    }

    public void OpenArmorShop()
    {
        shopManager.PopulateShopItem(shopArmour);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }
}
