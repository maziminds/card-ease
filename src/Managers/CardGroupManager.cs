using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace CardEase
{
    public abstract class CardGroupManager<T> : MonoBehaviour, IPointerEnterHandler where T : CardModel
    {
        [Header("--------------Prefabs----------------")]
        [Tooltip("Card prefab to add new card in group")]
        [SerializeField] GameObject cardPrefab;

        [Header("--------------Data----------------")]
        [Tooltip("minimum spacing needed between cards")]
        [SerializeField] float minSpacing = -99999f;
        [Tooltip("maximum spacing allowed between cards")]
        [SerializeField] float maxSpacing = 99999f;


        [Header("--------------PRIVATE----------------")]
        [Tooltip("layout group used to adjust spacing")]
        HorizontalLayoutGroup horizontalLayoutGroup;
        [Tooltip("Parent zone of the group")]
        CardZoneManager<T> parentZone;


        // --------------------------MONO methods------------------------
        #region methods related to MonoBehaviour
        protected virtual void Awake()
        {
            horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
            parentZone = transform.parent.GetComponent<CardZoneManager<T>>();
        }
        #endregion



        // --------------------------HELPER METHODS------------------------
        #region it's mostly public and can be used by other classes as well

        /// <summary>
        /// Adds a card to the group.
        /// </summary>
        /// <param name="cardModel">The card model to be added. If null, a default card will be created.</param>
        /// <returns>The <see cref="CardManager{T}"/> component of the newly created card.</returns>
        public virtual CardManager<T> AddCard(T cardModel = null)
        {
            GameObject card = Instantiate(cardPrefab, transform);
            CardManager<T> cardManager = card.GetComponent<CardManager<T>>();
            if (cardModel != null)
            {
                cardManager.SetData(cardModel);
            }
            cardManager.label = transform.parent.GetComponent<CardZoneManager<T>>().label;
            return cardManager;
        }


        /// <summary>
        /// Adjusts the spacing between elements in the card zone.
        /// </summary>
        /// <remarks>
        /// This method is can arrange the elements(cards) in it so it will cover the screen area properly
        /// </remarks>
        /// <returns>
        /// void
        /// </returns>
        public virtual void RefreshCardGroup()
        {
            //checking if there is any card in the group or not
            int cardCount = transform.childCount;
            if (cardCount <= 0)
            {
                return;
            }

            //get the width of card and card zone
            float cardWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
            float cardZoneWidth = transform.parent.GetComponent<RectTransform>().rect.width;

            //get the total groups and total card in the card zone
            int totalGroupCount = transform.parent.childCount;
            int totalCardCount = parentZone.GetAllCards().Count;

            //get the spacing of groups and calculate available zone space
            float groupSpacing = (cardZoneWidth / totalGroupCount) * 0.1f;
            cardZoneWidth = cardZoneWidth - (groupSpacing * (totalGroupCount + 1));

            //update spacing of group by card count
            float desiredSpacing = (cardZoneWidth / totalCardCount) - cardWidth;
            desiredSpacing = Mathf.Clamp(desiredSpacing, minSpacing, maxSpacing);
            horizontalLayoutGroup.spacing = desiredSpacing;

            //update group size based on cards
            Vector2 sizeDelta = transform.GetComponent<RectTransform>().sizeDelta;
            sizeDelta.x = (cardWidth + desiredSpacing) * (transform.childCount - 1) + cardWidth;
            transform.GetComponent<RectTransform>().sizeDelta = sizeDelta;
        }
        #endregion


        // ------------------------------------DRAG/DROP LOGIC--------------------------------------------------
        #region all LOGIC for drag-drop on group
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag)
            {
                CardManager<T> cardManager = eventData.pointerDrag.GetComponent<CardManager<T>>();
                if (cardManager.label == transform.parent.GetComponent<CardZoneManager<T>>().label)
                {
                    eventData.pointerDrag.GetComponent<CardManager<T>>().UpdateParent(this.transform);
                }
            }
        }
        #endregion

    }
}