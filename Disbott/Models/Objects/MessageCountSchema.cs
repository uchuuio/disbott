namespace Disbott.Models.Objects
{
    /// <summary>
    /// Object to hold the user messages, is stored in db
    /// </summary>
    public class MessageCountSchema
    {
        public ulong Id { get; set; }
        public ulong Messages { get; set; }
    }
}
