using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Rainfall.Tests.Sample;

public class App : AppFixture<Program> { }

public class Tests : TestBase<App>
{
    [Fact]
    public void Sample_Test()
    {
        (1 + 1).Should().Be(2);
    }
}