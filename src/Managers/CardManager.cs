using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardEase
{
    public abstract class CardManager<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler where T : CardModel
    {
        [Header("--------------Prefabs----------------")]
        [Tooltip("Place Holder prefab for dragging shadow")]
        [SerializeField] GameObject cardPlaceHolderPrefab;

        [Header("--------------Data----------------")]
        [Tooltip("Label to differentiate card zone and cards in it")]
        public string label;
        [Tooltip("Boolean to show is card selected or not")]
        public bool isSelected = false;

        [Header("--------------PRIVATE----------------")]
        [Tooltip("Parent group of the card")]
        Transform parentGroup;
        [Tooltip("Placeholder object for dragging shadow")]
        GameObject cardPlaceholder;


        // --------------------------HELPER METHODS------------------------
        #region it's mostly public and can be used by other classes as well

        /// <summary>
        /// Method to setUp any external data and render the card accordingly
        /// </summary>
        /// <param name="cardModel">A generic card model to store and pass custom value across the game</param>
        /// <returns>void</returns>
        /// <remarks>
        /// This method needs to be overridden by the child class to handle how the card should be rendered
        /// </remarks>
        public abstract void SetData(T cardModel);

        /// <summary>
        /// Method to update the card selection(to indicate is card selected or not)
        /// </summary>
        /// <param name="isSelected">a boolean showing if the card is selected or not</param>
        /// <returns>void</returns>
        /// <remarks>
        /// This method needs to be overridden by the child class to handle how the card should be rendered while selected
        /// </remarks>
        public abstract void UpdateSelection(bool isSelected);


        /// <summary>
        /// Method to update that card's parent group while being dragged from 1 group to another
        /// </summary>
        /// <param name="newParent">a <see cref="Transform"/> of the new parent group"</param>
        /// <returns>void</returns>
        /// <remarks>
        /// This method is used to update the card's parent group while being dragged from 1 group to another
        /// </remarks>
        public void UpdateParent(Transform newParent)
        {
            Transform oldParentGroup = parentGroup;
            parentGroup = newParent;
            if (cardPlaceholder)
            {
                cardPlaceholder.transform.SetParent(parentGroup);
            }
            if (oldParentGroup && oldParentGroup.parent)
            {
                oldParentGroup.parent.GetComponent<CardZoneManager<T>>().RefreshCardZone();
            }

        }

        /// <summary>
        /// Method that will be called whenever the card is picked up
        /// </summary>
        /// <returns>void</returns>
        /// <remarks>
        /// This method gets called whenever the card is picked up
        /// </remarks>
        public virtual void CardPicked()
        {
            CardEase.EventManager<T, CardManager<T>>.CARD_PICKED.Invoke(this);
        }

        /// <summary>
        /// Method that will be called whenever the card is dropped
        /// </summary>
        /// <returns>void</returns>
        /// <remarks>
        /// This method gets called whenever the card is dropped
        /// </remarks>
        public virtual void CardDropped()
        {
            CardEase.EventManager<T, CardManager<T>>.CARD_DROOPED.Invoke(this);
        }


        #endregion


        // ------------------------------------DRAG/DROP LOGIC--------------------------------------------------
        #region all LOGIC for drag-drop on group
        public void OnBeginDrag(PointerEventData eventData)
        {
            CardPicked();
            parentGroup = transform.parent;
            cardPlaceholder = Instantiate(cardPlaceHolderPrefab, transform.parent);
            cardPlaceholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
            transform.SetParent(parentGroup.parent.parent, true);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform container = parentGroup.parent.parent as RectTransform;
            Vector2 localPointerPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(container, eventData.position, eventData.pressEventCamera, out localPointerPos))
            {
                GetComponent<RectTransform>().localPosition = localPointerPos;
            }
            int newSiblingIndex = parentGroup.childCount;
            for (int i = 0; i < parentGroup.childCount; i++)
            {
                if (transform.position.x < parentGroup.GetChild(i).position.x)
                {
                    newSiblingIndex = i;
                    if (cardPlaceholder.transform.GetSiblingIndex() < newSiblingIndex)
                    {
                        newSiblingIndex--;
                    }

                    break;
                }
            }
            cardPlaceholder.transform.SetSiblingIndex(newSiblingIndex);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(parentGroup);
            transform.SetSiblingIndex(cardPlaceholder.transform.GetSiblingIndex());
            StartCoroutine(DestroyAndWait());
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            CardDropped();
        }
        #endregion

        //---------------------------ENUMERATORs--------------------------
        #region all enumerators used in this class(could be private, public)
        private IEnumerator DestroyAndWait()
        {
            Destroy(cardPlaceholder);
            while (cardPlaceholder != null)
            {
                yield return null;
            }
            transform.parent.parent.GetComponent<CardZoneManager<T>>().RefreshCardZone();
        }
        #endregion
    }


}