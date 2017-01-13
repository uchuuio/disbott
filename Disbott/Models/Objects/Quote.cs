namespace Disbott.Models.Objects
{
    /// <summary>
    /// Object to store the quotes, is stored in the db
    /// </summary>
    public class QuoteSchema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quote { get; set; }
    }
}
