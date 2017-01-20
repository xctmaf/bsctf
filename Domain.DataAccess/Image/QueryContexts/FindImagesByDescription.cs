namespace Domain.DataAccess.Image.QueryContexts
{
    using ByndyuSoft.Infrastructure.Domain;

    public class FindImagesByDescription : ICriterion
    {
        public FindImagesByDescription(string term)
        {
            Term = term;
        }

        public string Term { get; private set; }
    }
}