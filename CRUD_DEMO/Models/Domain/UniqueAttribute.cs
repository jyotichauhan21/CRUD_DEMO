namespace CRUD_DEMO.Models.Domain
{
    internal class UniqueAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}