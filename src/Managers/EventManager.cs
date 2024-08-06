using UnityEngine.Events;

namespace CardEase
{
    public class EventManager<J, T> where T : CardManager<J> where J : CardModel
    {
        #region CARD PICK-DROP
        public static UnityEvent CARD_PICKED = new UnityEvent();
        public static UnityEvent CARD_DROOPED = new UnityEvent();
        #endregion

        #region CARD ENTER/EXIT(HOVER)
        public static UnityEvent CARD_ENTER_ZONE = new UnityEvent();
        public static UnityEvent CARD_EXIT_ZONE = new UnityEvent();
        #endregion

        #region CARD SELECTION

        /// <summary>
        /// Event fired when a card is selected.
        /// </summary>
        /// <param name="CardManager">the cardManager class of the card that get selected
        public static UnityEvent<T> CARD_SELECTED = new UnityEvent<T>();



        /// <summary>
        /// Event fired when a card is deselected.
        /// </summary>
        /// <param name="CardManager">the cardManager class of the card that get deselected
        public static UnityEvent<T> CARD_DESELECTED = new UnityEvent<T>();
        #endregion
    }
}

