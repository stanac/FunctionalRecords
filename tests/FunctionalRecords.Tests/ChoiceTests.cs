using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalRecords.Tests
{
    public class ChoiceTests
    {
        [Fact]
        public void Test()
        {
            Choice<int, string> stringLength = 3;

            Choice<int, string> d = new Choice<int, string>();

            int stringLengthValue = stringLength.Match(i => i, s => s.Length);
        }
    }
}
