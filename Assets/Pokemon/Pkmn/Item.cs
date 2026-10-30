using UnityEngine;

namespace Pkmn
{
    public enum ITEM_TYPE
    {
        CURRENT_HEALTH,
        ATTACK,
        DEFENSE,
        SPEED
    }
    public class Item
    {
        protected int _itemPower;
        protected ITEM_TYPE _itemType;

        public int ItemPower => _itemPower;
        public ITEM_TYPE ItemType => _itemType;

        public Item(int power, ITEM_TYPE type)
        {
            _itemPower = power;
            _itemType = type;
        }
    }

}
