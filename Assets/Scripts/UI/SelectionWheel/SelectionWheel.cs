using DNExtensions.Utilities.RangedValues;

namespace UI
{
    using System.Collections.Generic;
    using DNExtensions;
    using UnityEngine;
    using PrimeTween;

    public class SelectionWheel : MonoBehaviour
    {
        [Header("Layout Settings")] 
        [SerializeField] private SelectionWheelItem itemPrefab;
        [SerializeField] private float radiusX = 100f;
        [SerializeField] private float radiusY = 50f;

        [Header("Animation")] 
        [SerializeField] private float transitionDuration = 0.3f;
        [SerializeField] private Ease transitionEase = Ease.OutCubic;

        private readonly List<SelectionWheelItem> wheelItems = new List<SelectionWheelItem>();

        private void OnEnable()
        {
            GameEvents.OnInventoryChanged += OnInventoryChanged;
        }

        private void OnDisable()
        {
            GameEvents.OnInventoryChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(PlayerInventory inventory)
        {
            RebuildWheel(inventory);
        }

        private void RebuildWheel(PlayerInventory inventory)
        {
            foreach (var item in wheelItems)
            {
                if (item) Destroy(item.gameObject);
            }

            wheelItems.Clear();

            if (!inventory)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);

            for (int i = 0; i < inventory.AllItems.Count; i++)
            {
                var wheelItem = Instantiate(itemPrefab, transform);
                wheelItem.Image.sprite = inventory.AllItems[i].Icon;
                wheelItems.Add(wheelItem);
            }

            SetPositionsImmediate();
        }

        private void SetPositionsImmediate()
        {
            for (int i = 0; i < wheelItems.Count; i++)
            {
                var pos = CalculatePosition(i);
                wheelItems[i].RectTransform.anchoredPosition = pos;
                wheelItems[i].RectTransform.localScale = Vector3.one;
                wheelItems[i].SetAlpha(1f);
            }
        }

        private Vector2 CalculatePosition(int itemIndex)
        {
            int count = wheelItems.Count;
            
            if (count == 1)
            {
                return Vector2.zero;
            }
            
            if (count == 2)
            {
                float xOffset = itemIndex == 0 ? -radiusX * 0.5f : radiusX * 0.5f;
                return new Vector2(xOffset, 0);
            }
            
            // 3+ items: circular layout
            float anglePerItem = 360f / count;
            float angle = itemIndex * anglePerItem;
            float angleRad = angle * Mathf.Deg2Rad;

            float x = Mathf.Sin(angleRad) * radiusX;
            float y = -Mathf.Cos(angleRad) * radiusY;

            return new Vector2(x, y);
        }
    }
}