using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;
using System;

namespace Licensing.Tests.E2E;

[TestFixture]
public class HappyPathTest : PageTest
{
    private string BaseUrl => Environment.GetEnvironmentVariable("E2E_BASE_URL") ?? "http://localhost:5001";

    [Test]
    public async Task FullApplicationFlow_ShouldSucceed()
    {
        string uniqueName = $"E2E_{DateTime.Now.Ticks % 1000000}";

        // 1. Operator: Submit Application
        await Page.GotoAsync($"{BaseUrl}/operator/submit");
        
        // Step 1
        await Page.FillAsync("input[placeholder='e.g. John Doe']", uniqueName);
        await Page.FillAsync("input[placeholder='e.g. Acme Corp']", "E2E Business");
        await Page.FillAsync("input[placeholder='john@example.com']", "e2e@test.com");
        
        await Page.ClickAsync("h5:has-text('Step 1')"); 
        await Page.ClickAsync("button:has-text('Next')");

        // Step 2
        await Page.FillAsync("textarea", "123 E2E Lane");
        await Page.FillAsync("input[type='number']", "10");
        await Page.ClickAsync("button:has-text('Next')");

        // Step 3
        await Page.ClickAsync("button:has-text('Submit Application')");
        
        // Wait for redirect to dashboard
        await Expect(Page).ToHaveURLAsync($"{BaseUrl}/operator");
        await Expect(Page.Locator($"tr:has-text('{uniqueName}')")).ToBeVisibleAsync(new() { Timeout = 10000 });

        // 2. Switch Persona to Officer
        await Page.ClickAsync("button:has-text('Officer')");

        // 3. Officer: Review Application
        await Page.GotoAsync($"{BaseUrl}/officer");
        await Expect(Page.Locator($"tr:has-text('{uniqueName}')")).ToBeVisibleAsync(new() { Timeout = 10000 });
        await Page.Locator($"tr:has-text('{uniqueName}')").Locator("button:has-text('Open Workspace')").ClickAsync();

        // Add feedback
        await Page.SelectOptionAsync("select", "ApplicantName");
        await Page.FillAsync("input[placeholder='Comment']", "Please clarify name");
        await Page.ClickAsync("button:has-text('Add')");

        // Request Resubmission
        await Page.FillAsync("textarea", "Reviewing initial submission.");
        await Page.ClickAsync("button:has-text('Request Resubmission')");
        
        await Expect(Page).ToHaveURLAsync($"{BaseUrl}/officer");

        // 4. Operator: Resubmit
        await Page.ClickAsync("button:has-text('Operator')");
        await Page.GotoAsync($"{BaseUrl}/operator");
        
        // RELOAD to ensure status update is fetched
        await Page.ReloadAsync();
        
        // Wait for status to update in the list (Operator label)
        await Expect(Page.Locator($"tr:has-text('{uniqueName}')")).ToContainTextAsync("Pending Pre-Site Resubmission", new() { Timeout = 20000 });
        
        // Click the application button 'Details' within the row
        await Page.Locator($"tr:has-text('{uniqueName}')").Locator("button:has-text('Details')").ClickAsync();
        
        // Ensure we are on details page
        await Expect(Page.Locator("span.badge")).ToContainTextAsync("Pending Pre-Site Resubmission", new() { Timeout = 10000 });
        
        await Page.ClickAsync("button:has-text('Resubmit Application')");

        // Fix flagged item - Applicant Name is the first input
        await Page.Locator("input").First.FillAsync($"{uniqueName}_UP");
        await Page.ClickAsync("button:has-text('Submit Updates')");

        // 5. Officer: Final Approval
        await Page.ClickAsync("button:has-text('Officer')");
        await Page.GotoAsync($"{BaseUrl}/officer");
        
        await Page.ReloadAsync();
        // The Officer label for PreSiteResubmitted is "Pre-Site Resubmitted"
        await Expect(Page.Locator($"tr:has-text('{uniqueName}')")).ToContainTextAsync("Pre-Site Resubmitted", new() { Timeout = 20000 });
        await Page.Locator($"tr:has-text('{uniqueName}')").Locator("button:has-text('Open Workspace')").ClickAsync();
        await Page.ClickAsync("button:has-text('Approve Application')");

        // Verify status in dashboard
        await Expect(Page).ToHaveURLAsync($"{BaseUrl}/officer");
        await Page.ClickAsync("button:has-text('Operator')");
        await Page.GotoAsync($"{BaseUrl}/operator");
        
        await Expect(Page.Locator($"tr:has-text('{uniqueName}')")).ToContainTextAsync("Approved", new() { Timeout = 20000 });
    }
}
