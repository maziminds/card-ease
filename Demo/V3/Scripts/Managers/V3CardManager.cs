using System.Collections;
using System.Collections.Generic;
using CardEase;
using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
    public class V3CardManager : CardEase.CardManager<V3CardModel>
    {
        Image cardImage;

        public V3CardModel cardData;
        [SerializeField] GameObject selected;


        void Awake()
        {
            cardImage = GetComponent<Image>();
        }


        public void ToggleSelection()
        {
            this.UpdateSelection(!this.isSelected);
        }

        public override void SetData(V3CardModel cardModel)
        {
            this.cardData = cardModel;
            if (cardModel.image != null)
            {
                this.cardImage.sprite = cardModel.image;
            }
        }

        public override void UpdateSelection(bool isSelected)
        {
            this.isSelected = isSelected;

            if (this.isSelected)
            {
                cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, cardImage.color.a) * 0.8f;
                selected.SetActive(true);
                CardEase.EventManager<V3CardModel, V3CardManager>.CARD_SELECTED.Invoke(this);
            }
            else
            {
                cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, cardImage.color.a) / 0.8f;
                selected.SetActive(false);
                CardEase.EventManager<V3CardModel, V3CardManager>.CARD_DESELECTED.Invoke(this);
            }
        }

        public override void CardPicked()
        {
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_PICKED.Invoke(this);
        }

        public override void CardDropped()
        {
            CardEase.EventManager<V3CardModel, V3CardManager>.CARD_DROOPED.Invoke(this);
        }
    }
}