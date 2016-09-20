using NUnit.Framework;

using SharpEcho.Recruiting.SpellChecker.Contracts;
using SharpEcho.Recruiting.SpellChecker.Core;

namespace SharpEcho.Recruiting.SpellChecker.Tests
{
    [TestFixture]
    class MnemonicSpellCheckerIBeforeETests
    {
        private ISpellChecker SpellChecker;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            SpellChecker = new MnemonicSpellCheckerIBeforeE();
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            var result = SpellChecker.Check("believe");

            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_At_End_Of_Word_Is_Spelled_Correctly()
        {
            var result = SpellChecker.Check("die");

            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_Word_That_Contains_E_Before_I_After_C_Is_Spelled_Correctly()
        {
            var result = SpellChecker.Check("perceive");

            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_Word_That_Contains_E_Before_I_Is_Spelled_Incorrectly()
        {
            var result = SpellChecker.Check("heir");

            Assert.That(result, Is.False);
        } 
        
        [Test]
        public void Check_Word_That_Contains_Multiple_E_Before_I_Is_Spelled_Incorrectly()
        {
            var result = SpellChecker.Check("Meliteieis");

            Assert.That(result, Is.False);
        }
    }
}
