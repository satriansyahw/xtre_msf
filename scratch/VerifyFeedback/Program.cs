using Licensing.Application.DTOs;
using Licensing.Application.Services;
using Licensing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseSqlite("Data Source=src/Licensing.Api/licensing.db")
    .Options;

using var db = new ApplicationDbContext(options);
var service = new ApplicationService(db);

// 1. Get an existing application
var app = await db.Applications.FirstOrDefaultAsync();
if (app == null)
{
    Console.WriteLine("No application found. Please run the app and submit one first.");
    return;
}

Console.WriteLine($"Testing with App: {app.Id} ({app.ReferenceNumber})");

// 2. Add feedback
var feedbackRequest = new ProvideFeedbackRequest
{
    FieldName = "TestField",
    Comment = $"Test Comment {DateTime.UtcNow}"
};

await service.ProvideFeedbackAsync(app.Id, feedbackRequest);
Console.WriteLine("Feedback added.");

// 3. Verify in DB
var feedbacks = await db.Feedbacks.Where(f => f.ApplicationId == app.Id).ToListAsync();
Console.WriteLine($"Feedbacks in DB for this app: {feedbacks.Count}");
foreach(var f in feedbacks) {
    Console.WriteLine($"- {f.FieldName}: {f.Comment} (Resolved: {f.IsResolved})");
}

// 4. Verify via GetDetailsAsync
var details = await service.GetDetailsAsync(app.Id);
Console.WriteLine($"Feedbacks in DetailsResponse: {details?.Feedbacks.Count}");
