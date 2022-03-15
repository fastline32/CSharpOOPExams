using System;
using NUnit.Framework;

[TestFixture]
public class HeroRepositoryTests
{
    [Test]
    public void HeroConstructor()
    {
        Hero hero = new Hero("a", 1);
        Assert.AreEqual(hero.Name,"a");
        Assert.AreEqual(hero.Level,1);
    }

    [Test]
    public void CheckHeroRepositoryCreate()
    {
        HeroRepository heroRepository = new HeroRepository();
        Assert.AreEqual(heroRepository.Heroes.Count,0);
    }

    [Test]
    public void CheckHeroCreateWithNull()
    {
        HeroRepository heroRepository = new HeroRepository();
        Assert.Throws<ArgumentNullException>(() => heroRepository.Create(null));
    }

    [Test]
    public void CheckHeroCreateWithSameName()
    {
        HeroRepository heroRepository = new HeroRepository();
        Hero hero = new Hero("pesho", 1);
        heroRepository.Create(hero);
        Hero hero1 = new Hero("pesho", 1);
        Assert.Throws<InvalidOperationException>(() => heroRepository.Create(hero1));
    }

    [Test]
    public void CheckHeroCreate()
    {
        HeroRepository heroRepository = new HeroRepository();
        Hero hero = new Hero("pesho", 1);
        string excepted = "Successfully added hero pesho with level 1";
        Assert.AreEqual(excepted,heroRepository.Create(hero));
    }

    [Test]
    public void CheckRemoveMethod()
    {
        HeroRepository heroRepository = new HeroRepository();
        Hero hero = new Hero("pesho", 1);
        Hero hero1 = new Hero("gosho", 2);
        heroRepository.Create(hero);
        heroRepository.Create(hero1);
        Assert.AreEqual(heroRepository.Remove("pesho"),true);
        Assert.AreEqual(heroRepository.Remove("pesho"), false);
    }

    [Test]
    public void CheckRemoveWithWrongData()
    {
        HeroRepository heroRepository = new HeroRepository();
        Assert.Throws<ArgumentNullException>(() => heroRepository.Remove(null));
        Assert.Throws<ArgumentNullException>(() => heroRepository.Remove(""));
        Assert.Throws<ArgumentNullException>(() => heroRepository.Remove(" "));
    }

    [Test]
    public void CheckForHighLevelHero()
    {
        HeroRepository heroRepository = new HeroRepository();
        Hero hero = new Hero("pesho", 1);
        Hero hero1 = new Hero("gosho", 2);
        heroRepository.Create(hero);
        heroRepository.Create(hero1);
        Assert.AreEqual(heroRepository.GetHeroWithHighestLevel().Name,"gosho");
    }

    [Test]
    public void CheckGetHero()
    {
        HeroRepository heroRepository = new HeroRepository();
        Hero hero = new Hero("pesho", 1);
        heroRepository.Create(hero);
        Assert.AreEqual(heroRepository.GetHero("pesho").Name,"pesho");
    }
}