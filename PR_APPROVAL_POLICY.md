# Pull Request Approval Policy for DMSK-dotnet

## Overview

This document clarifies the pull request approval process for the Dragon Medical SpeechKit .NET SDK repository.

## Repository Pull Request Policy

**⚠️ Important Notice: This repository does not accept pull requests.**

As stated in our [CONTRIBUTING.md](CONTRIBUTING.md):
> The code is public for anyone to view but is not meant to be modified and no pull requests will be accepted.

### Why No Pull Requests?

This repository contains sample code and SDK components for Dragon Medical SpeechKit. The code is:
- Provided as reference material and samples
- Maintained directly by Microsoft/Nuance
- Not intended for community contributions

## Getting Support

If you have questions, issues, or need assistance with Dragon Medical SpeechKit:

1. **Documentation**: Visit the official documentation at https://learn.microsoft.com/en-us/industry/healthcare/speechkit/
2. **Support**: Contact official support channels through the Microsoft Healthcare documentation
3. **SDK Updates**: Official updates are published through proper release channels

## Application Document Approval Workflow

If you're looking for information about document approval within the SpeechKit applications themselves, the codebase includes examples of document workflow management:

### Document States

The application uses several document states for workflow management:
- `DocumentStates.Undefined` - Default state
- `DocumentStates.Corrected` - Document has been reviewed and corrected
- `DocumentStates.Uncorrected` - Document needs correction

### Example: Signing and Approving Documents

```csharp
private void SignAndCloseButton_Click(object sender, RoutedEventArgs e)
{
    // Sign report -> set document state to Corrected
    _documentState = DocumentStates.Corrected;
    Close();
}
```

### Closing Documents with State

```csharp
// Close with document ID and state
_vuiController.Close(_documentId.Text, _documentState);
```

## Summary

- **GitHub PRs**: Not accepted for this repository
- **Support**: Use official Microsoft Healthcare documentation and support channels
- **Document Approval**: Implemented within the application using DocumentStates enum
- **Code Reference**: Available for learning and integration guidance only