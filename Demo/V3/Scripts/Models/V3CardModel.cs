
using UnityEngine;

namespace Demo
{
    [System.Serializable]
    public class V3CardModel : CardEase.CardModel
    {
        public int value;
        public string name;
        public Sprite image;

        public V3CardModel(int num, string str, Sprite img)
        {
            this.value = num;
            this.name = str;
            this.image = img;
        }
    }
}