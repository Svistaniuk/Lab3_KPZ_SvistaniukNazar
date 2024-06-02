using System;

namespace RPGDecoratorExample
{
    // Базовий інтерфейс для героя
    public interface IHero
    {
        string Description { get; }
        int GetPower();
    }

    // Реалізація героїв
    public class Warrior : IHero
    {
        public string Description => "Warrior";
        public int GetPower() => 10;
    }

    public class Mage : IHero
    {
        public string Description => "Mage";
        public int GetPower() => 8;
    }

    public class Paladin : IHero
    {
        public string Description => "Paladin";
        public int GetPower() => 9;
    }

    // Базовий клас для декораторів інвентарю
    public abstract class HeroDecorator : IHero
    {
        protected IHero _hero;

        public HeroDecorator(IHero hero)
        {
            _hero = hero;
        }

        public virtual string Description => _hero.Description;
        public virtual int GetPower() => _hero.GetPower();
    }

    // Конкретні декоратори інвентарю
    public class Armor : HeroDecorator
    {
        public Armor(IHero hero) : base(hero) { }

        public override string Description => _hero.Description + ", Armor";
        public override int GetPower() => _hero.GetPower() + 5;
    }

    public class Sword : HeroDecorator
    {
        public Sword(IHero hero) : base(hero) { }

        public override string Description => _hero.Description + ", Sword";
        public override int GetPower() => _hero.GetPower() + 7;
    }

    public class Amulet : HeroDecorator
    {
        public Amulet(IHero hero) : base(hero) { }

        public override string Description => _hero.Description + ", Amulet";
        public override int GetPower() => _hero.GetPower() + 3;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення героя-воїна
            IHero warrior = new Warrior();
            Console.WriteLine($"Description: {warrior.Description}, Power: {warrior.GetPower()}");

            // Додавання інвентарю герою
            warrior = new Armor(warrior);
            warrior = new Sword(warrior);
            warrior = new Amulet(warrior);

            Console.WriteLine($"Description: {warrior.Description}, Power: {warrior.GetPower()}");

            // Створення героя-мага
            IHero mage = new Mage();
            Console.WriteLine($"Description: {mage.Description}, Power: {mage.GetPower()}");

            // Додавання інвентарю герою
            mage = new Amulet(mage);
            mage = new Armor(mage);

            Console.WriteLine($"Description: {mage.Description}, Power: {mage.GetPower()}");

            // Створення героя-паладина
            IHero paladin = new Paladin();
            Console.WriteLine($"Description: {paladin.Description}, Power: {paladin.GetPower()}");

            // Додавання інвентарю герою
            paladin = new Sword(paladin);
            paladin = new Armor(paladin);

            Console.WriteLine($"Description: {paladin.Description}, Power: {paladin.GetPower()}");
        }
    }
}
