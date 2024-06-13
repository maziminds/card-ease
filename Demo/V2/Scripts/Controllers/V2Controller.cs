using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo
{
    public class V2Controller : MonoBehaviour
    {
        [Header("--------------Data----------------")]
        [Tooltip("card zone manager to manage cards in the card zone")]
        [SerializeField] V2CardZonManager cardZoneManager;

        [Tooltip("Card model to use for cards")]
        [SerializeField] V2CardModel[] cards;


        // --------------------------HELPER METHODS------------------------
        public void AddCard()
        {
            V2CardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
            List<V2CardModel> cards = new(){
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