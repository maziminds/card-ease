using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
    public class V2CardManager : CardEase.CardManager<V2CardModel>
    {
        Image cardImage;
        [SerializeField] GameObject selected;


        void Awake()
        {
            cardImage = GetComponent<Image>();
        }


        public void ToggleSelection()
        {
            this.UpdateSelection(!this.isSelected);
        }

        public override void SetData(V2CardModel cardModel)
        {
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
            }
            else
            {
                cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, cardImage.color.a) / 0.8f;
                selected.SetActive(false);
            }
        }
    }
}