using AnalysisModule.Indicators;
using NUnit.Framework;

namespace AnalysisModule.Tests.Indicators
{
    [TestFixture]
    public sealed class SMAIndicatorTests
    {
        [Test]
        public void TestTheSameData()
        {
            var test = new[] { 5M, 5M, 5M, 5M, 5M, 5M, 5M, 5M, 5M, 5M };
            for (int i = 1; i < test.Length; i++)
            {
                var target = new SMAIndicator((uint)i);
                foreach (var item in test)
                {
                    target.Add(item);
                    if (target.IsInitialized)
                    {
                        Assert.AreEqual(5M, target.PeekLast());
                    }
                    else
                    {
                        Assert.AreEqual(0M, target.PeekLast());
                    }
                }
            }
        }

        [Test]
        public void TestInitialization()
        {
            var target = new SMAIndicator(3);

            target.Add(1);
            Assert.False(target.IsInitialized);
            target.Add(2);
            Assert.False(target.IsInitialized);
            target.Add(3);
            Assert.True(target.IsInitialized);
            target.Add(4);
            Assert.True(target.IsInitialized);
        }

        [Test]
        public void TestAverageValidity()
        {
            var test = new[] { 1M, 2M, 3M, 4M, 5M, 6M, 7M, 8M, 9M, 10M };
            var expected = new[] { 0M, 1.5M, 2.5M, 3.5M, 4.5M, 5.5M, 6.5M, 7.5M, 8.5M, 9.5M };

            var target = new SMAIndicator(2);
            for (int i = 0; i < test.Length; i++)
            {
                target.Add(test[i]);
                Assert.AreEqual(expected[i], target.PeekLast());
            }
        }
    }
}
