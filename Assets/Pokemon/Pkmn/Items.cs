using UnityEngine;

namespace Pkmn
{
    public class Potion : Item
    {
        public Potion() : base(50, ITEM_TYPE.CURRENT_HEALTH) { }
    }

    public class SuperPotion : Item
    {
        public SuperPotion() : base(300, ITEM_TYPE.CURRENT_HEALTH) { }
    }

    public class AttackPlus : Item
    {
        public AttackPlus() : base(25, ITEM_TYPE.ATTACK) { }
    }

    public class DefensePlus : Item
    {
        public DefensePlus() : base(25, ITEM_TYPE.DEFENSE) { }
    }

    public class SpeedPlus : Item
    {
        public SpeedPlus() : base(25, ITEM_TYPE.SPEED) { }
    }
}