namespace VFHCatalogApi.Models
{
    public class IndexSeedSeedling
    {
       public int countryId { get; set; }
       public int regionId { get; set; }
       public int cityId { get; set; }
       public int pageSize { get; set; }
       public int? pageNo { get; set; }
       public bool isCompany { get; set; }
    }
}
