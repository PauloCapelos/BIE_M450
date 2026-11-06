using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace BIE.E2ETests;

[TestClass]
public class LearnerE2ETests : BrowserTestBase
{
    [TestMethod]
    public void TouristAt30DaysRemainsApprovedAfterReload()
    {
        // Arrange
        Fill("Sam Browser", "Tourismus", "29");
        // TODO: Absenden, sichtbares Ergebnis abwarten und prüfen.
        // Act
        Driver.FindElement(By.Id("submit")).Click();
        Wait.Until(d => d.FindElement(By.Id("result")).Displayed);

        StringAssert.Contains(Driver.FindElement(By.Id("result")).Text, "GENEHMIGT");
        // Assert.Fail("TODO: Assertions gemäss Arbeitsauftrag ergänzen.");
        var caseId = Driver.FindElement(By.Id("result")).GetDomAttribute("data-application-id");
        Driver.Navigate().Refresh();
        var row = Wait.Until(d => d.FindElements(By.CssSelector("#application-list tr")).FirstOrDefault(r => r.GetDomAttribute("data-id") == caseId));
        var cells = row.FindElements(By.TagName("td"));
        // TODO: Name, Herkunft, Tage und Entscheidung nach Neuladen prüfen.
        //Assert.Fail("TODO: Assertions gemäss Arbeitsauftrag ergänzen.");
        Assert.AreEqual("Sam Browser", cells[1].Text);
        Assert.AreEqual("Orion", cells[2].Text);
        Assert.AreEqual("29 Tage", cells[3].Text);
        StringAssert.Contains(cells[4].Text, "GENEHMIGT");
    }
}
