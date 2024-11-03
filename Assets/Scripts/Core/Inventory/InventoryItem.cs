using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NRPG.Core
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [Header("UI")]
        public Image image;
        public Text countText;
        public Item item; // Eşyanın referansı.
        private InventoryManager inventoryManager;

        [HideInInspector] public int count = 1;
        [HideInInspector] public Transform parentAfterDrag;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            inventoryManager = FindObjectOfType<InventoryManager>();
        }

        public void InitialisedItem(Item newItem)
        {
            if (newItem != null)
            {
                item = newItem;
                image.sprite = newItem.image;
                RefresCount();
            }
        }

        public void RefresCount()
        {
            countText.text = count.ToString();
            countText.gameObject.SetActive(count > 1);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;

            // parentAfterDrag, sürükleme sırasında güncellenir. Bu, yeni slot'ta kalmasını sağlar.
            if (parentAfterDrag != null)
            {
                transform.SetParent(parentAfterDrag); // Sürükleme işlemi tamamlandığında, yeni slot'ta kalır.
                transform.localPosition = Vector3.zero; // Pozisyonu sıfırla.
            }
        }



        // Sol tıklama olayını burada yakalıyoruz
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                // Eşyayı kullanmayı deniyoruz
                if (inventoryManager != null && item != null)
                {
                    inventoryManager.UseItem(item);
                    Debug.Log(item.name + " kullanıldı.");
                }
            }
        }
    }
}
