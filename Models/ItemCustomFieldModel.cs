namespace CatalogR.Models
{
    public class ItemCustomFieldModel
    {
        public string Type { get; set; }

        public string Class { get; set; }

        public Dictionary<string, string> Fields { get; set; }

        public ItemCustomFieldModel(Dictionary<string, string> fields, string type = "text", string @class = "form-control")
        {
            Fields = fields;
            Type = type;
            Class = @class;
        }
    }
}
