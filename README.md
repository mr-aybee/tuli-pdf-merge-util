# Tuli PDF Merge Util

[![CI](https://github.com/mr-aybee/tuli-pdf-merge-util/actions/workflows/ci.yml/badge.svg)](https://github.com/mr-aybee/tuli-pdf-merge-util/actions/workflows/ci.yml)
![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)
![type](https://img.shields.io/badge/type-CLI-4EAA25)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

This utility fills PDF form fields with data from a JSON file. It uses a PDF form template and a corresponding JSON file to generate a new, filled-out PDF document.

## Prerequisites

- .NET 9.0 Runtime or a higher version.
- A PDF form template with named, fillable fields.
- A JSON file containing the data to be filled into the PDF.

## Usage

You can run the application from the command line, providing paths to the PDF template, the JSON data file, and the desired output file.

```bash
TuliPdfMergeUtil.exe -p <path-to-your-pdf-form.pdf> -j <path-to-your-data.json> -o <path-for-the-output.pdf>
```

### Command-Line Options

-   `-p`, `--pdf` (Required): The absolute path to the source PDF form template.
-   `-j`, `--json` (Required): The absolute path to the JSON file containing the data.
-   `-o`, `--output` (Required): The absolute path where the filled PDF will be saved.

## How It Works

The tool reads the PDF form you provide and identifies all the fillable fields within it. It then reads the JSON data file and matches the keys from the JSON object to the names of the form fields in the PDF.

For each key in the JSON that matches a field name in the PDF, the tool inserts the corresponding value from the JSON into that field. After filling all the matching fields, it "flattens" the PDF, which means the fields become part of the content and are no longer editable. Finally, it saves this new PDF to the specified output path.

## Creating the PDF Template

1.  **Create a PDF:** Start with any PDF document. You can create one from scratch using software like Microsoft Word, Google Docs, or any other document editor and then saving it as a PDF.
2.  **Add Form Fields:** Use a PDF editor that supports form creation to add fillable fields to your PDF. Some popular tools for this are:
    -   Adobe Acrobat Pro
    -   LibreOffice Draw (Free and Open Source)
    -   Online PDF editors like Sejda or PDFescape.
3.  **Name the Fields:** This is the most crucial step. When you add a field (e.g., a text box, checkbox, dropdown), you must assign it a unique **name**. This name is the identifier the tool will use to match it with the data in your JSON file. For example, you could have fields named `FullName`, `Address`, `InvoiceNumber`, etc.

### Example Field Naming

Imagine you are creating a form with a field for a person's name. In your PDF editor, you would create a text field and set its name (often in the field's properties) to `FullName`.

## Creating the JSON Data File

The JSON file should be a simple flat object where keys correspond directly to the field names in your PDF template. The values are the data you want to insert into those fields.

### JSON Structure

```json
{
  "FieldName1": "Value1",
  "FieldName2": "Value2",
  "FieldName3": "Value3"
}
```

### Example JSON

Following the PDF field example from above, if your PDF has fields named `FullName`, `Address`, and `Date`, your `data.json` file would look like this:

```json
{
  "FullName": "John Doe",
  "Address": "123 Main St, Anytown, USA",
  "Date": "2025-07-08"
}
```

When you run the tool with this PDF and JSON file, the field named `FullName` in the PDF will be filled with "John Doe", `Address` with "123 Main St, Anytown, USA", and so on.

## Complete Example

Let's say you have:
-   A PDF template at `C:\Templates\Invoice.pdf` with a field named `InvoiceNumber`.
-   A JSON file at `C:\Data\InvoiceData.json` with the following content:
    ```json
    {
      "InvoiceNumber": "INV-2024-001"
    }
    ```
-   You want to save the output to `C:\Output\FilledInvoice.pdf`.

You would run the following command:

```bash
TuliPdfMergeUtil.exe -p "C:\Templates\Invoice.pdf" -j "C:\Data\InvoiceData.json" -o "C:\Output\FilledInvoice.pdf"
```

The result will be a new PDF file named `FilledInvoice.pdf` where the `InvoiceNumber` field is filled with "INV-2024-001".

## Build from source

Install the .NET 9 SDK, then run from the repository root:

```shell
dotnet build TuliPdfMergeUtil.sln -c Release
```

## Architecture

The `TuliPdfMergeUtil/` project contains the command-line entry point and PDF processing service. It uses CommandLineParser for argument handling and iText for PDF operations.

## Operational considerations

Use a separate output file and retain the original template. Test with synthetic form data before processing confidential documents. Review iText licensing requirements for your intended use. Build and runtime compatibility have not been independently verified.

## Contributing

Contributions are welcome. Read [CONTRIBUTING.md](CONTRIBUTING.md) for the branch, build and pull-request workflow. Please report security issues privately through [GitHub security advisories](https://github.com/mr-aybee/tuli-pdf-merge-util/security/advisories/new), not public issues.

## Code of Conduct

This project follows the [Contributor Covenant](CODE_OF_CONDUCT.md). By participating you agree to uphold it.

## License

Released under the [MIT License](LICENSE). © 2026 Abdullah Shahid.
