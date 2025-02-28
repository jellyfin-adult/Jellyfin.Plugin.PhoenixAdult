using Pronium.Helpers;
using Pronium.Helpers.Utils;
using Pronium.Sites;

namespace Pronium.UnitTests;

[TestFixture]
public class SiteData18Tests
{
    [SetUp]
    public void Setup()
    {
        Database.LoadAll();
    }

    private readonly SiteData18 _site = new();
    private readonly string _testSceneUrl = "https://www.data18.com/scenes/165843";

    [Test, Explicit("Data18 search is broken")]
    [TestCase(TestName = "{c}.{m}")]
    public async Task SearchIsWorking()
    {
        var result = await _site.Search(new[] { 51, 0 }, "Slut-Ber Party", null, new CancellationToken());
        Assert.That(result.Count, Is.GreaterThan(0));
        var id = result[0].ProviderIds.Values.FirstOrDefault();
        Assert.That(id, Is.Not.Empty);
        Assert.That(Helper.Decode(id?.Split('#')[0]), Is.EqualTo(_testSceneUrl));
    }

    [Test]
    [TestCase(TestName = "{c}.{m}")]
    public async Task UpdateIsWorking()
    {
        var result = await _site.Update(new[] { 51, 0 }, new[] { Helper.Encode(_testSceneUrl), "2010-09-14" }, new CancellationToken());
        Assert.That(result.Item.Name, Is.EqualTo("Slut-Ber Party"));
        Assert.That(result.Item.OriginalTitle, Is.EqualTo("data18 - 2010-09-14 - Slut-Ber Party"));
        Assert.That(result.Item.Overview, Is.Not.Empty);
        Assert.That(result.Item.Studios.Length, Is.EqualTo(3));
        Assert.That(result.Item.Genres.Length, Is.EqualTo(4));
        Assert.That(result.People.Count, Is.EqualTo(2));
        Assert.That(result.People[0].ImageUrl, Does.Contain("Ariella-Ferrera"));
        Assert.That(result.People[1].ImageUrl, Does.Contain("19ef59a8-18ad-4832-83bd-6d102b40525e"));
    }

    [Test]
    [TestCase(TestName = "{c}.{m}")]
    public async Task UpdateIsWorkingBoyGirl()
    {
        var result = await _site.Update(new[] { 51, 0 }, new[] { Helper.Encode("https://www.data18.com/scenes/144862"), "2010-10-18" }, new CancellationToken());
        Assert.That(result.Item.Name, Is.EqualTo("Block Out Your Husband ... Now Fuck Me"));
        Assert.That(result.Item.OriginalTitle, Is.EqualTo("data18 - 2010-10-18 - Block Out Your Husband ... Now Fuck Me"));
        Assert.That(result.Item.Overview, Is.Not.Empty);
        Assert.That(result.Item.Studios.Length, Is.EqualTo(3));
        Assert.That(result.Item.Genres.Length, Is.EqualTo(4));
        Assert.That(result.People.Count, Is.EqualTo(2));
        Assert.That(result.People[0].ImageUrl, Does.Contain("Sophie-Dee"));
        Assert.That(result.People[1].ImageUrl, Does.Contain("Scott-Nails"));
    }

    [Test]
    [TestCase(TestName = "{c}.{m}")]
    public async Task GetImagesIsWorking()
    {
        var result = (await _site.GetImages(
            new[] { 51, 0 },
            new[] { Helper.Encode(_testSceneUrl), "2010-09-14" },
            null,
            new CancellationToken())).ToList();

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(
            result.First().Url,
            Is.EqualTo("https://cdn.dt18.com/media/t/1/scenes/1/3/65843-ariella-ferrera-vanessa-veracruz-hot-and-mean.jpg"));
    }
}
