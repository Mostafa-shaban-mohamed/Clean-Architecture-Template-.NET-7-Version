namespace Wuzzfny.Domain.Common
{
    public class EntityList<TEntity>
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public IList<TEntity> PageData { get; set; }
    }
}