using System;
using UnityEngine;

namespace Pkmn
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;

            CurrentHealth = MaxHealth;
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; private set; }
        public TYPE BaseType { get => _baseType;}
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return CurrentEquipment == null ? _baseHealth : _baseHealth + CurrentEquipment.BonusHealth;
            }
        }
        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get
            {
                return CurrentEquipment == null ? _baseAttack : _baseAttack + CurrentEquipment.BonusAttack;
            }
        }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get
            {
                return CurrentEquipment == null ? _baseDefense : _baseDefense + CurrentEquipment.BonusDefense;
            }
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get
            {
                return CurrentEquipment == null ? _baseSpeed : _baseSpeed + CurrentEquipment.BonusSpeed;
            }
        }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive => CurrentHealth > 0;


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - (int)(((float)s.Power - (float)Defense) * TypeResolver.GetFactor(s.Type, _baseType)), 0, MaxHealth);
        }

        public void UseItem(Item item)
        {
            if (item == null) throw new ArgumentNullException();
            switch (item.ItemType)
            {
                case ITEM_TYPE.CURRENT_HEALTH:
                    CurrentHealth = Mathf.Clamp(CurrentHealth + item.ItemPower, 0, MaxHealth);
                    break;
                case ITEM_TYPE.ATTACK:
                    _baseAttack += item.ItemPower;
                    break;
                case ITEM_TYPE.DEFENSE:
                    _baseDefense += item.ItemPower;
                    break;
                case ITEM_TYPE.SPEED:
                    _baseSpeed += item.ItemPower;
                    break;
            }
        }

        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null) throw new ArgumentNullException("new equipment can't be null");
            CurrentEquipment = newEquipment;
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            CurrentEquipment = null;
        }

    }
}
