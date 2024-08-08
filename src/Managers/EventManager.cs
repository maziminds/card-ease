using UnityEngine.Events;

namespace CardEase
{
    public class EventManager<J, T> where T : CardManager<J> where J : CardModel
    {
        #region CARD PICK-DROP
        /// <summary>
        /// Event fired when a card is picked.
        /// </summary>
        /// <param name="CardManager">the cardManager class of the card that get picked
        public static UnityEvent<T> CARD_PICKED = new UnityEvent<T>();
        /// <summary>
        /// Event fired when a card is dropped.
        /// </summary>
        /// <param name="CardManager">the cardManager class of the card that get dropped
        public static UnityEvent<T> CARD_DROOPED = new UnityEvent<T>();
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

