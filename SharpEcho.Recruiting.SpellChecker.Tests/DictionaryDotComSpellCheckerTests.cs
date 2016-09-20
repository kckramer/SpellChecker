using NUnit.Framework;

using SharpEcho.Recruiting.SpellChecker.Contracts;
using SharpEcho.Recruiting.SpellChecker.Core;

namespace SharpEcho.Recruiting.SpellChecker.Tests
{
    [TestFixture]
    class DictionaryDotComSpellCheckerTests
    {
        private ISpellChecker SpellChecker;

        [OneTimeSetUp]
        public void TestFixureSetUp()
        {
            SpellChecker = new DictionaryDotComSpellChecker();
        }

        [Test]
        public void Check_That_SharpEcho_Is_Misspelled()
        {
            var result = SpellChecker.Check("SharpEcho");

            Assert.That(result, Is.False);
        }

        [Test]
        public void Check_That_South_Is_Not_Misspelled()
        {
            var result = SpellChecker.Check("South");

            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_That_Case_Does_Not_Matter()
        {
            var result = SpellChecker.Check("sOuTh");

            Assert.That(result, Is.True);
        }

        [Test]
        public void Check_That_Whitespace_Is_Trimmed()
        {
            var result = SpellChecker.Check("sOu Th");

            Assert.That(result, Is.True);
        }
    }
}
