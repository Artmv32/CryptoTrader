using AnalysisModule.Indicators;
using NUnit.Framework;
using System;
using System.IO;

namespace AnalysisModule.Tests.Indicators
{
    [TestFixture]
    public class EMAIndicatorTests
    {
        [Test]
        public void TestEmaData()
        {
            var data = GetEmaData1().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var ema = new EMAIndicator(10);
            int i = 0;
            for (; i < 9; i++)
            {
                var value = decimal.Parse(data[i].Trim());
                ema.Add(value);
                Assert.AreEqual(0M, ema.PeekLast());
                Assert.False(ema.IsInitialized);
            }
            for (; i < data.Length; i++)
            {
                var datas = data[i].Split(new[] { '\t' });
                var value = decimal.Parse(datas[0]);
                var expectedEma = decimal.Parse(datas[3]);
                ema.Add(value);
                Assert.IsTrue(ema.IsInitialized);
                Assert.AreEqual(expectedEma.ToString("C2"), ema.PeekLast().ToString("C2"));
            }
        }


        private static string GetEmaData1()
        {
            return @"22.27			
22.19			
22.08			
22.17			
22.18			
22.13			
22.23			
22.43			
22.24			
22.29	22.22	0	22.22
22.15	22.21	0.1818	22.21
22.39	22.23	0.1818	22.24
22.38	22.26	0.1818	22.27
22.61	22.31	0.1818	22.33
23.36	22.42	0.1818	22.52
24.05	22.61	0.1818	22.80
23.75	22.77	0.1818	22.97
23.83	22.91	0.1818	23.13
23.95	23.08	0.1818	23.28
23.63	23.21	0.1818	23.34
23.82	23.38	0.1818	23.43
23.87	23.53	0.1818	23.51
23.65	23.65	0.1818	23.54
23.19	23.71	0.1818	23.47
23.10	23.69	0.1818	23.40
23.33	23.61	0.1818	23.39
22.68	23.51	0.1818	23.26
23.10	23.43	0.1818	23.23
22.40	23.28	0.1818	23.08
22.17	23.13	0.1818	22.92
";
        }
    }
}
