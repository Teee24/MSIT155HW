namespace MSIT155Site.Models.DTO
{
    public class searchDTO
    {
        public string? keyword { get; set; }
        //public int? categoryId { get; set; } = 0;//加預設值
        public int? categoryId { get; set; }
        public string? sortBy { get; set; }
        public string? sortType { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
    }
}
