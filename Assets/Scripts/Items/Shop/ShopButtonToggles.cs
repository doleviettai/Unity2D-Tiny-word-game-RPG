using UnityEngine;

public class ShopButtonToggles : MonoBehaviour
{
    public void OpenItemShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenItemShop();
        }
        
    }

    public void OpenWeaponsShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenWeapShop();
        }
    }

    public void OpenArmorShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenArmorShop();
        }
    }
}
