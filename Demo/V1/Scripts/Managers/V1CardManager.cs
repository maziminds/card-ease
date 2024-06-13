using UnityEngine.UI;

namespace Demo
{
    public class V1CardManager : CardEase.CardManager<V1CardModel>
    {
        Image cardImage;

        void Awake()
        {
            cardImage = GetComponent<Image>();
        }

        public override void SetData(V1CardModel cardModel)
        {
            if (cardModel.image != null)
            {
                this.cardImage.sprite = cardModel.image;
            }
        }

        public override void UpdateSelection(bool isSelected)
        {
            this.isSelected = isSelected;
        }
    }
}