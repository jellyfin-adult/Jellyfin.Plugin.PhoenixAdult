using Pronium.Helpers;
using Pronium.Helpers.Utils;
using Pronium.Sites;

namespace Pronium.UnitTests;

[TestFixture]
public class SiteFreeUseFantasyTests
{
    [SetUp]
    public void Setup()
    {
        Database.LoadAll();
    }

    private readonly NetworkMylf _site = new();
    private readonly string _testSceneUrl = "https://www.teamskeet.com/movies/freeused-freeloader";

    [Test]
    [TestCase(TestName = "{c}.{m}")]
    public async Task SearchIsWorking()
    {
        var result = await _site.Search(new[] { 24, 58 }, "Freeused Freeloader", null, new CancellationToken());
        Warn.Unless(result.Count, Is.GreaterThan(1));
        if (result.Count >= 1) {
            var id = result[0].ProviderIds.Values.FirstOrDefault();
            Assert.That(id, Is.Not.Empty);
            Assert.That(Helper.Decode(id?.Split('#')[0]), Is.EqualTo(_testSceneUrl));
            Assert.That(result.First().ImageUrl, Does.Contain("breezy_bri_and_penelope_woods"));
        }
    }

    [Test]
    [TestCase(TestName = "{c}.{m}")]
    public async Task UpdateIsWorking()
    {
        var result = await _site.Update(new[] { 24, 58 }, new[] { Helper.Encode(_testSceneUrl), "2024-08-10" }, new CancellationToken());
        Warn.Unless(result.Item, Is.Not.Null);
        if (result.Item != null) {
            Assert.That(result.Item.Name, Is.EqualTo("Freeused Freeloader"));
            Assert.That(result.Item.OriginalTitle, Is.EqualTo("freeusefantasy - 2024-08-10 - Freeused Freeloader"));
            Assert.That(result.Item.Overview, Does.StartWith("Tyler is pissed at Penelope. She’s not contributing to rent"));
            Assert.That(result.Item.Studios.Length, Is.EqualTo(2));
            Assert.That(result.Item.Genres.Length, Is.EqualTo(86));
            Assert.That(result.Item.Genres, Does.Contain("Belly Button Piercings"));
            Assert.That(result.People.Count, Is.EqualTo(2));
            Assert.That(result.People.Select(t => t.Name), Does.Contain("Penelope Woods"));
            Assert.That(result.People.Select(t => t.Name), Does.Contain("Breezy Bri"));
#pragma warning disable NUnit2021 // Incompatible types for EqualTo constraint
            Assert.That(result.Item.PremiereDate, Is.EqualTo(new DateTime(2024, 08, 10)).Within(24).Hours);
#pragma warning restore NUnit2021 // Incompatible types for EqualTo constraint
        }
    }

    [Test]
    [TestCase(TestName = "{c}.{m}")]
    public async Task GetImagesIsWorking()
    {
        var result = (await _site.GetImages(
            new[] { 24, 58 },
            new[] { Helper.Encode(_testSceneUrl), "2024-08-10" },
            null,
            new CancellationToken())).ToList();

        Warn.Unless(result, Has.Count.EqualTo(2));
        Warn.Unless(result.FirstOrDefault()?.Url, Does.Contain("breezy_bri_and_penelope_woods"));
    }
}
