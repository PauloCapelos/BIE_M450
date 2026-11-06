using System.Net.Http.Json;
using Bie.IntegrationTests;
using BIE.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIE.IntegrationTests;

[TestClass]
public class LearnerIntegrationTests
{
    [TestMethod]
    public async Task TouristAt30DaysIsSavedAsApproved()
    {
        using var app = new BieFactory();
        using var client = app.CreateClient();
        var entry = new Entry("Sam Test", "Orion", "Tourismus", 29, false, false, false, true); //Nacher wieder auf 30 ändern
        var response = await client.PostAsJsonAsync("/api/cases", entry);
        var records = await client.GetFromJsonAsync<List<Case>>("/api/cases");
        // TODO: HTTP-Status, gespeicherte Werte und Entscheidung prüfen.

        Assert.AreEqual("Created", response.StatusCode.ToString());
        Assert.IsNotNull(records);
        Assert.AreEqual(1, records.Count());

        Assert.AreEqual("approved", records[0].Decision.Status);
        Assert.IsTrue(records[0].Id > 0);

    }
}
