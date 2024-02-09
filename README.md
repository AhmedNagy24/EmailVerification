# CSV Email Verification Tool (C# WPF)
This project is a desktop application developed using C# and WPF (Windows Presentation Foundation) that provides functionality for verifying email addresses within CSV files. It allows users to specify a directory containing CSV files, and then loops over each file to verify the validity of email addresses using two methods: format check and DNS check. Users have the flexibility to choose which check method to apply or perform both checks simultaneously.
## Features
- Directory Selection: Users can choose a directory containing CSV files for email verification.
- CSV File Parsing: The application parses each CSV file within the selected directory to extract email addresses for verification.
- Email Verification Methods:
  * Format Check: Verifies email addresses based on their format to ensure they adhere to standard email address conventions.
  * DNS Check: Performs a DNS lookup to verify the existence of the email domain and its mail exchange (MX) records.
- Selectable Verification Method: Users can choose to perform either the format check, DNS check, or both.
- Results Display: Displays the verification results for each email address, indicating whether it passed or failed each verification method.
- Error Handling: Provides error handling and informative messages for cases such as invalid CSV files or directory paths.
## Technologies Used
- C#: Primary programming language for developing the application logic and user interface.
- WPF (Windows Presentation Foundation): Framework for building desktop applications with rich user interfaces.
- CSV Parsing: Utilizes libraries or custom logic for parsing CSV files and extracting email addresses.
- DNS Lookup: Performs DNS lookups programmatically to verify domain existence and MX records.
### Note
The regex used in the format check is moderately strict. You can modify it to match your needs.
```
"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
```
