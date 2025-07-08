using CommandLine;

namespace TuliPdfMergeUtil;

public class Options
{
	[Option('p', "pdf", Required = true, HelpText = "Path to the PDF file or directory containing PDFs.")]
	public required string PdfPath { get; set; }

	[Option('j', "json", Required = true, HelpText = "Path to the JSON configuration file.")]
	public required string JsonPath { get; set; }

	[Option('o', "output", Required = true, HelpText = "Path to the output file.")]
	public required string OutputPath { get; set; }
}