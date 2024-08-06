using System.Collections;
using System.Collections.Generic;
using CardEase;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo
{
    public class V3Controller : MonoBehaviour
    {
        [Header("--------------Data----------------")]
        [Tooltip("card zone manager to manage cards in the card zone")]
        [SerializeField] V3CardZonManager cardZoneManager;

        [Tooltip("Card model to use for cards")]
        [SerializeField] V3CardModel[] cards;


        // --------------------------MONO METHODS------------------------
        void OnEnable()
        {
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_SELECTED.AddListener(CardSelected);
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_DESELECTED.AddListener(CardDeselected);
        }
        void OnDisable()
        {
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_SELECTED.RemoveListener(CardSelected);
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_DESELECTED.RemoveListener(CardDeselected);
        }

        // --------------------------HELPER METHODS------------------------

        public void CardSelected(V3CardManager card)
        {
            Debug.Log($"Card Selected {card.label} and {card.cardData.name}");
        }
        public void CardDeselected(V3CardManager card)
        {
            Debug.Log($"Card Deselected {card.label} and {card.cardData.name}");
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
    }
}