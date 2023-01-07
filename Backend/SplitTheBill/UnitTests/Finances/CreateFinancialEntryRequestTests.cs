using Application.Finances.CreateEntry;

namespace UnitTests.Finances;

public sealed class CreateFinancialEntryRequestTests
{
    readonly CreateEntryCommand command = new();

    [Fact]
    public void LinesIsNull_ShouldReturnErrorMessage()
    {
        // Assert:
        command.FinancialLines = null;

        // Act:
        bool isValid = (command as IValidation).IsValid(out string? errorMessage);

        // Assert:
        Assert.False(isValid);
        Assert.Contains(ErrorMessages.Finances.NoExpenseLines, errorMessage);
        Assert.DoesNotContain(ErrorMessages.Finances.LineWithNegativeAmount, errorMessage);
    }

    [Fact]
    public void LinesHaveNegativeAmounts_ShouldReturnErrorMessage()
    {
        // Assert:
        command.FinancialLines = new()
        {
            new() { Amount = -1, DebtorId = Guid.NewGuid() },
        };

        // Act:
        bool isValid = (command as IValidation).IsValid(out string? errorMessage);

        // Assert:
        Assert.False(isValid);
        Assert.Contains(ErrorMessages.Finances.LineWithNegativeAmount, errorMessage);
    }
}