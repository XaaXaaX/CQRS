namespace CQRS.Application.Queries
{
    public interface IOrderHandler
    {
        OrderResult Execute(OrderQuery query);
    }
}