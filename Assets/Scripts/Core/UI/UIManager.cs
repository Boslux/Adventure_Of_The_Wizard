using UnityEngine;
public class UIManager: MonoBehaviour
{
    public InventoryManager inventoryManager;
    private Item selectedItem;

    public void OnUseItemButtonClicked()
    {
        if (selectedItem!=null)
        {
            inventoryManager.UseItem(selectedItem);
            Debug.Log("Eşya kullanıldı");
        }
        else
        {
            Debug.Log("Kullanılacak item seçilmedi");
        }
    }
    public void SetSelectedItem(Item item)
    {
        selectedItem=item;
    }
}