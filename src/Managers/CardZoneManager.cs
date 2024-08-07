using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardEase
{
    public abstract class CardZoneManager<T> : MonoBehaviour, IPointerEnterHandler where T : CardModel
    {
        [Header("--------------Prefabs----------------")]
        [Tooltip("Card Group prefab to add new group in zone")]
        [SerializeField] GameObject cardGroupPrefab;

        [Header("--------------Data----------------")]
        [Tooltip("minimum spacing needed between card groups")]
        [SerializeField] float minSpacing = -99999f;
        [Tooltip("maximum spacing allowed between card groups")]
        [SerializeField] float maxSpacing = 99999f;

        [Tooltip("Indicate if empty group should be removed")]
        [SerializeField] bool shouldRemoveEmptyGroup = true;

        [Tooltip("Label to differentiate card zone and cards in it")]
        public string label = "CardZone";


        [Header("--------------PRIVATE----------------")]
        [Tooltip("layout group used to adjust spacing")]
        HorizontalLayoutGroup horizontalLayoutGroup;


        // --------------------------MONO methods------------------------
        #region methods related to MonoBehaviour
        protected virtual void Awake()
        {
            horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        }
        #endregion



        // --------------------------HELPER METHODS------------------------
        #region it's mostly public and can be used by other classes as well

        /// <summary>
        /// Adds a new group to the card zone.
        /// </summary>
        /// <param name="cards">An optional list of cards to be added to the new group.</param>
        /// <returns>A <see cref="CardGroupManager{T}"/> representing the newly added group.</returns>
        /// <remarks>
        /// This method creates a new empty group in the card zone. If a list of cards is provided, those cards will be added to the new group
        /// </remarks>
        public CardGroupManager<T> AddGroup(List<T> cards = null)
        {
            GameObject cardGroup = Instantiate(cardGroupPrefab, transform);

            if (cards != null)
            {
                foreach (T card in cards)
                {
                    cardGroup.GetComponent<CardGroupManager<T>>().AddCard(card);
                }
            }
            return cardGroup.GetComponent<CardGroupManager<T>>();
        }

        /// <summary>
        /// Adjusts the spacing between elements in the card zone.
        /// </summary>
        /// <remarks>
        /// This method can arrange the elements(cardGroups) in it so it will cover the screen area properly
        /// </remarks>
        /// <returns>
        /// void
        /// </returns>
        public void RefreshCardZone()
        {
            //checking if there is any group in the zone or not
            int groupCount = transform.childCount;
            if (groupCount <= 0)
            {
                return;
            }

            //adjust spacing based on group count and zone width
            float desiredSpacing = GetComponent<RectTransform>().rect.width / groupCount * 0.1f;
            desiredSpacing = Mathf.Clamp(desiredSpacing, minSpacing, maxSpacing);
            horizontalLayoutGroup.spacing = desiredSpacing;

            foreach (Transform child in transform)
            {
                if (shouldRemoveEmptyGroup)
                {
                    if (child.childCount <= 0)
                    {
                        Destroy(child.gameObject);
                        continue;
                    }
                }

                CardGroupManager<T> cardGroup = child.GetComponent<CardGroupManager<T>>();
                cardGroup.RefreshCardGroup();
            }
        }


        /// <summary>
        /// Get the list of total cards in the card zone
        /// </summary>
        /// <remarks>
        /// This method will get the list of all cards in the card zone
        /// </remarks>
        /// <returns>
        /// A list of <see cref="CardManager{T}"/> objects representing all the cards in the card zone.
        /// </returns>
        public virtual List<CardManager<T>> GetAllCards()
        {
            List<CardManager<T>> cards = new List<CardManager<T>>();
            foreach (Transform groupTransform in transform)
            {
                foreach (Transform cardTransform in groupTransform)
                {
                    cards.Add(cardTransform.gameObject.GetComponent<CardManager<T>>());
                }
            }
            return cards;
        }


        /// <summary>
        /// Get the list of total SELECTED cards in the card zone
        /// </summary>
        /// <remarks>
        /// This method will get the list of all SELECTED cards in that perticular card zone
        /// </remarks>
        /// <returns>
        /// A list of <see cref="CardManager{T}"/> objects representing all the cards in the card zone.
        /// </returns>
        public virtual List<CardManager<T>> GetAllSelectedCards()
        {
            List<CardManager<T>> selectedCards = new List<CardManager<T>>();
            foreach (Transform groupTransform in transform)
            {
                foreach (Transform cardTransform in groupTransform)
                {
                    CardManager<T> card = cardTransform.gameObject.GetComponent<CardManager<T>>();
                    if (card.isSelected)
                    {
                        selectedCards.Add(card);
                    }

                }
            }
            return selectedCards;
        }


        /// <summary>
        /// This method will make a new group of selected cards and return the group
        /// </summary>
        /// <remarks>
        /// if no card is selected then it will return empty group
        /// </remarks>
        /// <returns>
        /// A list of <see cref="CardGroupManager{T}"/> objects representing all the cards in the card zone.
        /// </returns>
        public CardGroupManager<T> GroupSelectedCards()
        {
            List<CardManager<T>> selectedCards = new List<CardManager<T>>();
            foreach (CardManager<T> card in GetAllCards())
            {
                if (card.isSelected)
                {
                    selectedCards.Add(card);
                }
            }
            GameObject cardGroup = Instantiate(cardGroupPrefab, transform);
            foreach (CardManager<T> card in selectedCards)
            {
                card.transform.SetParent(cardGroup.transform);
                card.UpdateSelection(false);
            }
            RefreshCardZone();
            return cardGroup.GetComponent<CardGroupManager<T>>();
        }

        #endregion


        // ------------------------------------DRAG/DROP LOGIC--------------------------------------------------
        #region all LOGIC for drag-drop on group
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag)
            {
                CardManager<T> cardManager = eventData.pointerDrag.GetComponent<CardManager<T>>();
                if (cardManager && cardManager.label == this.label)
                {
                    if (transform.childCount <= 0)
                    {
                        CardGroupManager<T> groupManager = AddGroup();
                        Vector2 sizeDelta = groupManager.transform.GetComponent<RectTransform>().sizeDelta;
                        sizeDelta.x = GetComponent<RectTransform>().sizeDelta.x;
                        groupManager.transform.GetComponent<RectTransform>().sizeDelta = sizeDelta;
                    }
                }
            }
        }

        #endregion

    }
}