using Pkmn;
using NUnit.Framework;
using System;
using UnityEditor.VersionControl;

namespace Pkmn.Tests
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer des features et les TU sur le reste du projet

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // - un heal ne régénère pas plus que les HP Max
        // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type
        // - L'utilisation d'objets : Potion, SuperPotion, Vitess+, Attack+ etc.
        // - Gérer les PP (limite du nombre d'utilisation) d'une attaque,
        // si on selectionne une attack qui a 0 PP on inflige 0

        // Choisis ce que tu veux ajouter comme feature et fait en au max.
        // Les nouveaux TU doivent être dans ce fichier.
        // Modifiant des features il est possible que certaines valeurs
        // des TU précédentes ne matchent plus, tu as le droit de réadapter les valeurs
        // de ces anciens TU pour ta nouvelle situation.
        [Test]
        public void ItemsCanModifyStats()
        {
            var a = new Character(100, 10, 10, 10, TYPE.NORMAL);
            var p = new Punch();
            var potion = new Potion();
            var attackPlus = new AttackPlus();
            var defensePlus = new DefensePlus();
            var speedPlus = new SpeedPlus();

            a.ReceiveAttack(p);

            Assert.That(a.CurrentHealth, Is.EqualTo(40));

            a.UseItem(potion);
            a.UseItem(attackPlus);
            a.UseItem(defensePlus);
            a.UseItem(speedPlus);

            Assert.That(a.CurrentHealth, Is.EqualTo(90));
            Assert.That(a.Attack, Is.EqualTo(35));
            Assert.That(a.Defense, Is.EqualTo(35));
            Assert.That(a.Speed, Is.EqualTo(35));
        }

        [Test]
        public void UseItemNullCheck()
        {
            var a = new Character(100, 10, 10, 10, TYPE.NORMAL);
            Assert.Throws<ArgumentNullException>(() =>
                a.UseItem(null)
            );
        }

        [Test]
        public void ItemsInFights()
        {
            var pikachu = new Character(100, 10, 10, 10, TYPE.NORMAL);
            var mewtwo = new Character(100, 10, 10, 20, TYPE.NORMAL);
            var ramoloss = new Character(100, 10, 10, 5, TYPE.NORMAL);
            var mp = new MegaPunch();
            var fight = new Fight(pikachu, mewtwo);

            fight.ExecuteTurn(mp, mp);
            Assert.That(pikachu.IsAlive, Is.EqualTo(false));
            Assert.That(mewtwo.IsAlive, Is.EqualTo(true));

            fight = new Fight(ramoloss, mewtwo);

            var sp = new SpeedPlus();
            ramoloss.UseItem(sp);
            fight.ExecuteTurn(mp, mp);

            Assert.That(ramoloss.IsAlive, Is.EqualTo(true));
            Assert.That(ramoloss.Speed, Is.EqualTo(30));
            Assert.That(mewtwo.IsAlive, Is.EqualTo(false));
        }

        [Test]
        public void CurrentHealthClampsBetween0AndMax()
        {
            var c = new Character(100, 10, 0, 0, TYPE.NORMAL);
            var mp = new MegaPunch();

            c.ReceiveAttack(mp);
            Assert.That(c.CurrentHealth, Is.EqualTo(0));

            //Heal de 300 pv
            var pot = new SuperPotion();

            c.UseItem(pot);

            Assert.That(c.CurrentHealth, Is.EqualTo(100));
        }

        [Test]
        public void StartTurnWithDeadCharacterCheck()
        {
            var pikachu = new Character(100, 10, 10, 10, TYPE.NORMAL);
            var mewtwo = new Character(100, 10, 10, 20, TYPE.NORMAL);
            var mp = new MegaPunch();

            Assert.That(pikachu.IsAlive, Is.EqualTo(true));
            pikachu.ReceiveAttack(mp);
            Assert.That(pikachu.IsAlive, Is.EqualTo(false));

            var f = new Fight(pikachu, mewtwo);
            Assert.Throws<ArgumentException>(() =>
                f.ExecuteTurn(mp, mp)
            );
        }

        [Test]
        public void TypeFactor()
        {
            var bulbizarre = new Character(200, 0, 0, 10, TYPE.WATER);

            var fb = new FireBall();
            var wbb = new WaterBlouBlou();
            var mg = new MagicalGrass();

            bulbizarre.ReceiveAttack(wbb);
            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(180));

            bulbizarre.ReceiveAttack(fb);
            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(80));


        }

    }
}
