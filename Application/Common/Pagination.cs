namespace Application.Common
{
    public sealed class Pagination(int limit, int offset)
    {
        public int Limit { get; set; } = limit == 0 ? 10 : limit;

        public int Offset { get; set; } = offset;
    }
}
