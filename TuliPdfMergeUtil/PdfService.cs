using System.Reflection.PortableExecutable;
using System.Text.Json;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;

namespace TuliPdfMergeUtil;

public class PdfService(string pdfPath, string jsonPath, string outputPath)
{
	/// <summary>
	/// Merges the PDF files based on the provided JSON configuration.
	/// </summary>
	public async Task MergePdf()
	{
		Console.WriteLine($"Merging PDFs from \r\n⌊PDF Template: {pdfPath} using configuration from \r\n⌊Json: {jsonPath} into \r\n Output: {outputPath}");

		var jsonText = await File.ReadAllTextAsync(jsonPath);
		var jDoc = JsonDocument.Parse(jsonText);
		var dataObject = jDoc.Deserialize<Dictionary<string, object>>();

		if (dataObject == null)
		{
			throw new InvalidDataException("Invalid data");
		}

		// Fill the PDF
		using var reader = new PdfReader(pdfPath);
		await using var writer = new PdfWriter(outputPath);
		using var pdfDoc = new PdfDocument(reader, writer);

		var form = PdfAcroForm.GetAcroForm(pdfDoc, true);
		var fields = form.GetAllFormFields();

		// 5. Iterate over the JSON properties and fill the PDF fields
		foreach (var property in dataObject)
		{
			if (fields.TryGetValue(property.Key, out var field))
			{
				var value = property.Value.ToString();
				field.SetValue(value);
			}
		}

		// Optional: Flatten the form to make fields non-editable
		form.FlattenFields();

		Console.WriteLine("PDF filled successfully!");
	}
}