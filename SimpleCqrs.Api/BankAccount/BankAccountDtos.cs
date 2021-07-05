namespace SimpleCqrs.Api.BankAccount
{
    public record OpenDto
    {
        public string Name;
    }

    public record FreezeDto;

    public record WithdrawDto(decimal Amount);
}