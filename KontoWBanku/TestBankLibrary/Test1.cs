using BankLibrary;

namespace TestBankLibrary
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void Konstruktor_Poprawne_Dane()
        {
            string nazwa = "Adamczyk";
            decimal saldoPoczatkowe = 1000;

            Account konto = new Account(nazwa, saldoPoczatkowe);

            Assert.IsNotNull(konto);
            Assert.AreEqual(nazwa, konto.Nazwa);
            Assert.AreEqual(saldoPoczatkowe, konto.Saldo);
        }
    }
}
