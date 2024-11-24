namespace VFHCatalogApi.Models
{
    public class IndexPlant
    {
        public int pageSize { get; set; }
        public int? pageNo { get; set; }
        public string searchString { get; set; }
        public int typeId { get; set; }
        public int groupId { get; set; }
        public int? sectionId { get; set; }
    }
}
