using System.Collections;
using System.Collections.Generic;
using CardEase;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Demo
{
    public class V3Controller : MonoBehaviour
    {
        [Header("--------------Data----------------")]
        [Tooltip("card zone manager to manage cards in the card zone")]
        [SerializeField] V3CardZonManager cardZoneManager;

        [Tooltip("card zone image for black cards")]
        [SerializeField] Image blackCardZoneImage;

        [Tooltip("card zone image for red cards")]
        [SerializeField] Image redCardZoneImage;

        [Tooltip("Card model to use for cards")]
        [SerializeField] V3CardModel[] cards;

        [Tooltip("Selected Card Text")]
        [SerializeField] TextMeshProUGUI selectedCardsNames;


        // --------------------------MONO METHODS------------------------
        void OnEnable()
        {
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_SELECTED.AddListener(CardSelected);
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_DESELECTED.AddListener(CardDeselected);
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_PICKED.AddListener(CardPicked);
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_DROOPED.AddListener(CardDropped);
        }
        void OnDisable()
        {
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_SELECTED.RemoveListener(CardSelected);
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_DESELECTED.RemoveListener(CardDeselected);
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_PICKED.RemoveListener(CardPicked);
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_DROOPED.RemoveListener(CardDropped);
        }

        // --------------------------HELPER METHODS------------------------

        public void CardSelected(V3CardManager card)
        {
            Debug.Log("Card Selected: " + card.cardData.name);
            UpdateSelectedCards();
        }
        public void CardDeselected(V3CardManager card)
        {
            Debug.Log("Card Deselected: " + card.cardData.name);
            UpdateSelectedCards();
        }
        public void CardPicked(V3CardManager card)
        {
            Debug.Log("Card picked: " + card.cardData.name);
            HighLightCardZone(card.cardData);
        }
        public void CardDropped(V3CardManager card)
        {
            Debug.Log("Card dropped: " + card.cardData.name);
            ClearHighLightedCardZone();
        }


        public void AddCard()
        {
            V3CardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
            List<V3CardModel> cards = new(){
                randomCard,
            };
            cardZoneManager.AddGroup(cards);
            cardZoneManager.RefreshCardZone();
        }

        public void MakeGroup()
        {

            cardZoneManager.GroupSelectedCards();
        }

        public void PreviousLevel()
        {
            SceneManager.LoadScene("V1");
        }

        public void UpdateSelectedCards()
        {
            selectedCardsNames.text = "";
            foreach (var cardManager in cardZoneManager.GetAllSelectedCards())
            {
                V3CardManager newCard = cardManager as V3CardManager;
                selectedCardsNames.text = selectedCardsNames.text + "\n" + newCard.cardData.name;
            }
        }

        public void HighLightCardZone(V3CardModel card)
        {
            blackCardZoneImage.color = new Color(blackCardZoneImage.color.r, blackCardZoneImage.color.g, blackCardZoneImage.color.b, 0.2f);
            redCardZoneImage.color = new Color(redCardZoneImage.color.r, redCardZoneImage.color.g, redCardZoneImage.color.b, 0.2f);
            if (card.color == CardColor.BLACK)
            {
                blackCardZoneImage.color = new Color(blackCardZoneImage.color.r, blackCardZoneImage.color.g, blackCardZoneImage.color.b, 1f);
            }
            if (card.color == CardColor.RED)
            {
                redCardZoneImage.color = new Color(redCardZoneImage.color.r, redCardZoneImage.color.g, redCardZoneImage.color.b, 1f);
            }
        }

        public void ClearHighLightedCardZone()
        {
            blackCardZoneImage.color = new Color(blackCardZoneImage.color.r, blackCardZoneImage.color.g, blackCardZoneImage.color.b, 0.2f);
            redCardZoneImage.color = new Color(redCardZoneImage.color.r, redCardZoneImage.color.g, redCardZoneImage.color.b, 0.2f);
        }
    }
}