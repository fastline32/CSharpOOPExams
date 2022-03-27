using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        
        [Test]
        public void TestItemConstructor()
        {
            Item item = new Item("a", "2");
            Assert.AreEqual("2",item.ItemId);
            Assert.AreEqual("a",item.Owner);
        }

        [Test]
        public void CheckBankVaultConstructor()
        {
            BankVault bankVault = new BankVault();
            Assert.AreEqual(12,bankVault.VaultCells.Count);
        }

        [Test]
        public void CheckAddItemForInvalidCell()
        {
            Item item = new Item("a", "dve");
            BankVault bankVault = new BankVault();
            Assert.Throws<ArgumentException>(() => bankVault.AddItem("z1", item));
        }
        [Test]
        public void CheckAddItemForFullCell()
        {
            Item item = new Item("a", "dve");
            BankVault bankVault = new BankVault();
            bankVault.AddItem("A1", new Item("b", "tri"));
            Assert.Throws<ArgumentException>(() => bankVault.AddItem("A1", item));
        }
        [Test]
        public void CheckAddItemForAlreadyAddedItem()
        {
            Item item = new Item("a", "dve");
            BankVault bankVault = new BankVault();
            bankVault.AddItem("A1", item);
            Assert.Throws<InvalidOperationException>(() => bankVault.AddItem("A2", item));
        }

        [Test]
        public void CheckAddItemForSuccess()
        {
            Item item = new Item("a", "dve");
            BankVault bankVault = new BankVault();
            Assert.AreEqual("Item:dve saved successfully!",bankVault.AddItem("A1", item));
        }

        [Test]
        public void CheckRemoveMethodForWrongCell()
        {
            Item item = new Item("a", "dve");
            BankVault bankVault = new BankVault();
            Assert.Throws<ArgumentException>(() => bankVault.RemoveItem("Z1", item));
        }

        [Test]
        public void CheckRemoveMethodWithWrongItem()
        {
            Item item = new Item("a", "dve");
            BankVault bankVault = new BankVault();
            bankVault.AddItem("A1", item);
            Assert.Throws<ArgumentException>(() => bankVault.RemoveItem("A1", new Item("b", "tri")));
        }

        [Test]
        public void CheckRemoveMethodWithValidData()
        {
            Item item = new Item("a", "dve");
            BankVault bankVault = new BankVault();
            bankVault.AddItem("A1", item);
            Assert.AreEqual("Remove item:dve successfully!",bankVault.RemoveItem("A1",item));
        }
    }
}