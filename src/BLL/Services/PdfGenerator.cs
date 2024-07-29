using BLL.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;

namespace BLL.Services;

public class PdfGenerator
{

    public byte[] GenerateCaseFilePdf(CaseFileModel caseFile)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var document = QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(2, Unit.Centimetre);
                page.Header().Text("Case File").FontSize(20).Bold().AlignCenter();
                page.Content().PaddingVertical(1, Unit.Centimetre).Column(column =>
                {
                    column.Item().Text($"Type: {caseFile.Type}");
                    column.Item().Text($"Initiation Date: {caseFile.InitiationDate}");

                    column.Item().Text("Connected Persons:");
                    foreach (var group in caseFile.ConnectedPersons)
                    {
                        column.Item().Text($"Group: {group.Key}");
                        foreach (var person in group.Value)
                        {
                            column.Item().Text($" - {person.Name}");
                        }
                    }

                    column.Item().Text("Reports:");
                    foreach (var report in caseFile.Reports)
                    {
                        column.Item().Text($" - Report Title: {report.Id} {report.DateOfReport}, Content: {report.Description}");
                    }

                    column.Item().Text("Warrants:");
                    foreach (var warrant in caseFile.Warrants)
                    {
                        column.Item().Text($" - Warrant Type: {warrant.Id} {warrant.IssueDate}, Description: {warrant.Description}");
                    }
                });
            });
        });

        using (var ms = new MemoryStream())
        {
            document.GeneratePdf(ms);
            return ms.ToArray();
        }
    }
}