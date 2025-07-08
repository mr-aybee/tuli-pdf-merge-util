using CommandLine;
using TuliPdfMergeUtil;

var arguments = args;

arguments =
[
	"-p", @"C:\Users\malik\OneDrive\Desktop\temp\tuli\pdfmerger\renewal-notice\pdf-renewal-notice-form.pdf",
	"-j", @"C:\Users\malik\OneDrive\Desktop\temp\tuli\pdfmerger\renewal-notice\data.json",
	"-o", @"C:\Users\malik\OneDrive\Desktop\temp\tuli\pdfmerger\output.pdf"
];

Parser.Default
	.ParseArguments<Options>(arguments)
	.WithParsed<Options>(o =>
	{
		Console.WriteLine($"PDF Path: {o.PdfPath}");
		Console.WriteLine($"JSON Path: {o.JsonPath}");
		Console.WriteLine($"Output Path: {o.OutputPath}");

		// Here you can add the logic to merge PDFs based on the provided options.

		new PdfService(o.PdfPath, o.JsonPath, o.OutputPath)
			.MergePdf()
			.GetAwaiter().GetResult();
	});