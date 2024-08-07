
using UnityEngine;

namespace Demo
{
    [System.Serializable]
    public class V3CardModel : CardEase.CardModel
    {
        public int value;
        public string name;
        public CardColor color;
        public Sprite image;

        public V3CardModel(int num, string str, Sprite img, CardColor col)
        {
            this.value = num;
            this.name = str;
            this.image = img;
            this.color = col;
        }
    }

    public enum CardColor
    {
        RED,
        BLACK
    }
}