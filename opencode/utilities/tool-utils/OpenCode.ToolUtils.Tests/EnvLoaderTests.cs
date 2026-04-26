using OpenCode.ToolUtils.Env;

namespace OpenCode.ToolUtils.Tests;

public sealed class EnvLoaderTests
{
    [Fact]
    public void Load_WithEmptySearchPaths_ReturnsEmptyDictionary()
    {
        var result = EnvLoader.Load(new EnvLoaderOptions { SearchPaths = [] });
        Assert.Empty(result);
    }

    [Fact]
    public void Get_MissingKey_ReturnsNull()
    {
        var key = "OPENCODE_TOOLUTILS_MISSING_KEY";
        Environment.SetEnvironmentVariable(key, null);

        var value = EnvLoader.Get(key, new EnvLoaderOptions { SearchPaths = [] });
        Assert.Null(value);
    }
}
