using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo
{

    public class V1Controller : MonoBehaviour
    {
        [Header("--------------Data----------------")]
        [Tooltip("card zone manager to manage cards in the card zone")]
        [SerializeField] V1CardZoneManager cardZoneManagerRed;
        [SerializeField] V1CardZoneManager cardZoneManagerBlue;

        [Tooltip("Card model to use for cards")]
        [SerializeField] V1CardModel[] cards;


        // --------------------------MONO methods------------------------

        void Start()
        {
            V1CardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
            List<V1CardModel> cards = new()
        {
            randomCard
        };
            cardZoneManagerRed.AddGroup(cards);
            cardZoneManagerRed.RefreshCardZone();
        }



        // --------------------------HELPER METHODS------------------------
        public void AddCardToRed()
        {
            V1CardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
            List<V1CardModel> cards = new(){
                randomCard,
            };
            cardZoneManagerRed.AddGroup(cards);
            cardZoneManagerRed.RefreshCardZone();
        }
        public void AddCardToBlue()
        {
            V1CardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
            List<V1CardModel> cards = new(){
                randomCard,
            };
            cardZoneManagerBlue.AddGroup(cards);
            cardZoneManagerBlue.RefreshCardZone();
        }

        public void NextLevel()
        {
            SceneManager.LoadScene("V2");
        }

    }
}